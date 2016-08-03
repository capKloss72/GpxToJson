using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace GpxToJson
{
    class Program
    {

        private static CsvReader courseDataCSV = new CsvReader(File.OpenText(ConfigurationManager.AppSettings["course_data"]));
        private static CsvReader raceDataCSV = new CsvReader(File.OpenText(ConfigurationManager.AppSettings["race_data"]));

        static void Main(string[] args)
        {

            var details = courseDataCSV.GetRecords<CourseDataCSV>();

            CourseInfo courseInfo = null;

            foreach (var detail in details)
            {
                Console.WriteLine($"GPX Location: {detail.GPXLocation} Output Json Location: {detail.OutputJsonLocation} Race ID: {detail.RaceId}");
                courseInfo = GenerateCourseInfo(detail);
                SerializeJson(courseInfo, detail.RaceId);
            }

            Console.ReadLine();

        }


        private static CourseInfo GenerateCourseInfo(CourseDataCSV courseDataCSV)
        {

            Properties courseLineProperty = new Properties(true);
            Properties startLineProperty = new Properties(false, true, false, false);
            Properties endLineProperty = new Properties(false, false, true, false);

            List<LineCourseCoordinate> coordPairs = ExtractCourseLineCoordinates(courseDataCSV.GPXLocation, courseLineProperty);

            CourseInfo courseInfo = new CourseInfo();

            courseInfo.type = "FeatureCollection";
            Feature[] features = new Feature[3];

            features[0] = AddCourseLineFeature(courseLineProperty, coordPairs);
            features[1] = AddFirstLastLineFeature(startLineProperty, courseDataCSV);
            features[2] = AddFirstLastLineFeature(endLineProperty, courseDataCSV);

            courseInfo.features = features;

            return courseInfo;
        }

        private static List<LineCourseCoordinate> ExtractCourseLineCoordinates(string gpxfile, Properties property)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(gpxfile);
            XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("trkpt");

            List<double> coordinates = new List<double>();
            List<LineCourseCoordinate> coordPairs = new List<LineCourseCoordinate>();


            foreach (XmlNode node in nodes)
            {
                LineCourseCoordinate coords = new LineCourseCoordinate();
                coords.lat = Double.Parse(node.Attributes["lat"].Value);
                coords.lon = Double.Parse(node.Attributes["lon"].Value);
                coords.ele = 0;
                coordPairs.Add(coords);
            }
            return coordPairs;
        }

        private static List<LineCourseCoordinate> ExtractEdgeCoordinates(Properties property)
        {
            List<LineCourseCoordinate> edgeLineCoordinates = new List<LineCourseCoordinate>();

            var details = courseDataCSV.GetRecords<CourseDataCSV>();
            foreach (var detail in details)
            {
                if (property.startline)
                {
                    edgeLineCoordinates.Add(new LineCourseCoordinate(detail.StartLatRight, detail.StartLonRight));
                    edgeLineCoordinates.Add(new LineCourseCoordinate(detail.StartLonRight, detail.StartLonLeft));
                }
                else if (property.finishline)
                {
                    edgeLineCoordinates.Add(new LineCourseCoordinate(detail.FinishLatRight, detail.FinishLonRight));
                    edgeLineCoordinates.Add(new LineCourseCoordinate(detail.FinishLonRight, detail.FinishLonLeft));
                }
            }

            return edgeLineCoordinates;
        }

        private static Feature AddCourseLineFeature(Properties properties, List<LineCourseCoordinate> coordPairs)
        {
            Feature feature = new Feature();
            feature.type = "Feature";

            Geometry lineGeometry = new Geometry();
            feature.geometry = lineGeometry;
            lineGeometry.type = "LineString";

            object[,] courseLineGeometryCoordinates = new object[coordPairs.Count, 3];

            for (int i = 0; i < coordPairs.Count; i++)
            {
                courseLineGeometryCoordinates[i, 0] = coordPairs[i].lat;
                courseLineGeometryCoordinates[i, 1] = coordPairs[i].lon;
                courseLineGeometryCoordinates[i, 2] = coordPairs[i].ele;
            }
            lineGeometry.coordinates = courseLineGeometryCoordinates;
            feature.properties = properties;

            return feature;
        }

        private static Feature AddFirstLastLineFeature(Properties properties, CourseDataCSV courseDataCSV)
        {
            Feature feature = new Feature();
            feature.type = "Feature";

            Geometry lineGeometry = new Geometry();
            feature.geometry = lineGeometry;
            lineGeometry.type = "LineString";

            object[,] coordinates = new object[2, 2];

            if (properties.startline)
            {
                coordinates[0, 0] = courseDataCSV.StartLatRight;
                coordinates[0, 1] = courseDataCSV.StartLonRight;
                coordinates[1, 0] = courseDataCSV.StartLatLeft;
                coordinates[1, 1] = courseDataCSV.StartLonLeft;
            }
            else if (properties.finishline)
            {
                coordinates[0, 0] = courseDataCSV.FinishLatRight;
                coordinates[0, 1] = courseDataCSV.FinishLonRight;
                coordinates[1, 0] = courseDataCSV.FinishLatLeft;
                coordinates[1, 1] = courseDataCSV.FinishLonLeft;
            }

            lineGeometry.coordinates = coordinates;
            feature.properties = properties;

            return feature;
        }

        private static void SerializeJson(CourseInfo courseInfo, string raceid)
        {
            string CourseInfoJson = JsonConvert.SerializeObject(courseInfo, Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            File.WriteAllText(raceid, CourseInfoJson);
            //Console.WriteLine(CourseInfoJson);
        }
    }

}
