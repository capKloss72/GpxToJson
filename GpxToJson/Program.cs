using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("Add valid GPX file and a valid raceid");
                Console.ReadLine();
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File: {args[0]} cannot be found...");
                Console.ReadLine();
            }

            if (args[1].Length == 0)
            {
                Console.WriteLine("File name is too short are the file path is invalid");
                Console.ReadLine();
            }
            else
            {
                int hasSuffix = args[1].LastIndexOf('.');
                if (hasSuffix < 0)
                {
                    args[1] = args[1] + ".json";
                }
                else if (string.Compare(args[1].Substring(hasSuffix), "json") < 0)
                {
                    args[1] = args[1].Substring(0, args[1].LastIndexOf('.')) + ".json";
                }
            }

            //CourseInfo courseInfo = GenerateCourseInfo(args[0]);
            //SerializeJson(courseInfo, args[1]);

            var csv = new CsvReader(File.OpenText(@"C:\Dev\TestIt.csv"));

            var details = csv.GetRecords<CourseDetails>();

            foreach (var detail in details)
            {
                Console.WriteLine(detail.File + " " + detail.Name);
            }

            Console.ReadLine();

        }

        private static List<CourseCoordinate> ExtractCoordinates(string gpxfile, Properties property)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(gpxfile);
            XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("trkpt");

            List<double> coordinates = new List<double>();
            List<CourseCoordinate> coordPairs = new List<CourseCoordinate>();


            foreach (XmlNode node in nodes)
            {
                CourseCoordinate coords = new CourseCoordinate();
                coords.lat = Double.Parse(node.Attributes["lat"].Value);
                coords.lon = Double.Parse(node.Attributes["lon"].Value);
                coords.ele = 0;
                coordPairs.Add(coords);
            }
            return coordPairs;
        }

        private static CourseInfo GenerateCourseInfo(string gpxfile)
        {

            Properties property = new Properties();
            property.courseline = true;
            List<CourseCoordinate> coordPairs = ExtractCoordinates(gpxfile, property);

            CourseInfo courseInfo = new CourseInfo();

            courseInfo.type = "FeatureCollection";
            Feature[] features = new Feature[3];

            Feature courseLineFeature = AddFeature(new Properties(true, false, false, false), coordPairs);

            features[0] = AddFeature(new Properties(true, false, false, false), coordPairs);
            features[1] = AddFeature(new Properties(false, true, false, false), coordPairs);
            features[2] = AddFeature(new Properties(false, false, true, false), coordPairs);

            courseInfo.features = features;


            return courseInfo;
        }

        private static Feature AddFeature(Properties properties, List<CourseCoordinate> coordPairs)
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

            if (properties.startline)
            {
                lineGeometry.coordinates = GetFirstLastCoordinates(coordPairs, 0, 1);
            }
            else if (properties.finishline)
            {
                object[,] finishLineGeometryCoordinates = new object[2, 2];
                var first = courseLineGeometryCoordinates.GetLength(0);
                lineGeometry.coordinates = GetFirstLastCoordinates(coordPairs, first - 2, first - 1);
            }
            else
            {
                lineGeometry.coordinates = courseLineGeometryCoordinates;
            }

            feature.properties = properties;

            return feature;
        }

        private static object[,] GetFirstLastCoordinates(List<CourseCoordinate> coordPairs, int first, int last)
        {
            object[,] coordinates = new object[2, 2];
            coordinates[0, 0] = coordPairs[first].lat;
            coordinates[0, 1] = coordPairs[first].lon;
            coordinates[1, 0] = coordPairs[last].lat;
            coordinates[1, 1] = coordPairs[last].lon;
            return coordinates;
        }

        private static double FindAngle(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Atan2(dy, dx) * (180 / Math.PI);
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
