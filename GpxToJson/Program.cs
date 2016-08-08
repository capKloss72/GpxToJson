using CsvHelper;
using Microsoft.WindowsAzure.Storage.Blob;
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
using Trackerconfig.Json;
using Trackerconfig.Props;
using Trackerconfig.Utilities;
using System;  // C# , ADO.NET
using C = System.Data.SqlClient; // System.Data.dll
using D = System.Data;
using Trackervonfig.Props;
using Trackervonfig.Utilities;

namespace Trackerconfig
{
    class Program
    {

        private static string APPOPTIONS = ConfigurationManager.AppSettings["appcoptions"];

        static void Main(string[] args)
        {

            //TomrUtils.InsertEventDetails("rider_data");

            //TomrUtils.UpdateEventDetails("rider_data", true);
            //Console.WriteLine("Inserted");
            //Console.ReadLine();

            switch (APPOPTIONS)
            {
                case "generateevent":
                    JsonGPXUtils.MigrateToJson(true);
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["race_data"], true);
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["rider_data"], true);
                    break;
                case "gpxconvert":
                    JsonGPXUtils.MigrateToJson(false);
                    break;
                case "gpxconvertandupladblob":
                    JsonGPXUtils.MigrateToJson(true);
                    break;
                case "upladblob":
                    JsonGPXUtils.UploadJson();
                    break;
                case "eventsetup":
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["race_data"], true);
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["rider_data"], true);
                    break;
                case "racesetup":
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["race_data"], true);
                    break;
                case "ridersetup":
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["rider_data"], true);
                    break;
                case "eventupdate":
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["race_data"], false);
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["rider_data"], false);
                    break;
                case "raceupdate":
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["race_data"], false);
                    break;
                case "riderupdate":
                    TomrUtils.UpdateEventDetails(ConfigurationManager.AppSettings["rider_data"], false);
                    break;
                default:
                    Console.WriteLine("No applications options selected, please update App.config");
                    break;
            }

            Console.ReadLine();
        }

        //private static void MigrateToJson(bool upload)
        //{
        //    CsvReader courseDataCSV = null;
        //    var coursedata = ConfigurationManager.AppSettings["course_data"];
        //    try
        //    {
        //        courseDataCSV = new CsvReader(File.OpenText(coursedata));
        //        var details = courseDataCSV.GetRecords<CourseDataCSV>();

        //        CourseInfo courseInfo = null;

        //        foreach (var detail in details)
        //        {
        //            courseInfo = GenerateCourseInfo(detail);
        //            if (upload)
        //            {
        //                using (MemoryStream jsonStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(SerializeJson(courseInfo, detail.OutputJsonLocation))))
        //                {
        //                    BlobUtils.UploadRaceBlob(ConfigurationManager.AppSettings["BlobContainerName"], jsonStream, detail.RaceId);
        //                }
        //            } else
        //            {
        //                SerializeAndWriteJson(courseInfo, detail.OutputJsonLocation);
        //            }
        //        }
        //    }
        //    catch (DirectoryNotFoundException e)
        //    {
        //        Console.WriteLine($"Unable to locate: {coursedata}");
        //        Console.ReadLine();
        //    }
        //}

        //private static void UploadJson()
        //{
        //    CsvReader courseDataCSV = null;
        //    var coursedata = ConfigurationManager.AppSettings["course_data"];
        //    try
        //    {
        //        courseDataCSV = new CsvReader(File.OpenText(coursedata));
        //        var details = courseDataCSV.GetRecords<CourseDataCSV>();

        //        foreach (var detail in details)
        //        {
        //            using (FileStream courseJason = File.Create(@detail.OutputJsonLocation))
        //            {
        //                BlobUtils.UploadRaceBlob(ConfigurationManager.AppSettings["BlobContainerName"], courseJason, detail.RaceId);
        //            }
        //        }
        //    }
        //    catch (DirectoryNotFoundException e)
        //    {
        //        Console.WriteLine($"Unable to locate: {coursedata}");
        //        Console.ReadLine();
        //    }
        //}

        //private static CourseInfo GenerateCourseInfo(CourseDataCSV courseDataCSV)
        //{

        //    Properties courseLineProperty = new Properties(true);
        //    Properties startLineProperty = new Properties(false, true, false, false);
        //    Properties endLineProperty = new Properties(false, false, true, false);

        //    List<LineCourseCoordinate> coordPairs = ExtractCourseLineCoordinates(courseDataCSV.GPXLocation, courseLineProperty);

        //    CourseInfo courseInfo = new CourseInfo();

        //    courseInfo.type = "FeatureCollection";
        //    Feature[] features = new Feature[3];

        //    features[0] = AddCourseLineFeature(courseLineProperty, coordPairs);
        //    features[1] = AddFirstLastLineFeature(startLineProperty, courseDataCSV);
        //    features[2] = AddFirstLastLineFeature(endLineProperty, courseDataCSV);

        //    courseInfo.features = features;

        //    Console.WriteLine($"Course Info generated successfully: {courseDataCSV.RaceId}");

        //    return courseInfo;
        //}

        //private static List<LineCourseCoordinate> ExtractCourseLineCoordinates(string gpxfile, Properties property)
        //{

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(gpxfile);
        //    XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("trkpt");

        //    List<double> coordinates = new List<double>();
        //    List<LineCourseCoordinate> coordPairs = new List<LineCourseCoordinate>();


        //    foreach (XmlNode node in nodes)
        //    {
        //        LineCourseCoordinate coords = new LineCourseCoordinate();
        //        coords.lat = Double.Parse(node.Attributes["lat"].Value);
        //        coords.lon = Double.Parse(node.Attributes["lon"].Value);
        //        coords.ele = 0;
        //        coordPairs.Add(coords);
        //    }
        //    return coordPairs;
        //}

        //private static List<LineCourseCoordinate> ExtractEdgeCoordinates(Properties property)
        //{
        //    List<LineCourseCoordinate> edgeLineCoordinates = new List<LineCourseCoordinate>();

        //    var details = courseDataCSV.GetRecords<CourseDataCSV>();
        //    foreach (var detail in details)
        //    {
        //        if (property.startline)
        //        {
        //            edgeLineCoordinates.Add(new LineCourseCoordinate(detail.StartLatRight, detail.StartLonRight));
        //            edgeLineCoordinates.Add(new LineCourseCoordinate(detail.StartLonRight, detail.StartLonLeft));
        //        }
        //        else if (property.finishline)
        //        {
        //            edgeLineCoordinates.Add(new LineCourseCoordinate(detail.FinishLatRight, detail.FinishLonRight));
        //            edgeLineCoordinates.Add(new LineCourseCoordinate(detail.FinishLonRight, detail.FinishLonLeft));
        //        }
        //    }

        //    return edgeLineCoordinates;
        //}

        //private static Feature AddCourseLineFeature(Properties properties, List<LineCourseCoordinate> coordPairs)
        //{
        //    Feature feature = new Feature();
        //    feature.type = "Feature";

        //    Geometry lineGeometry = new Geometry();
        //    feature.geometry = lineGeometry;
        //    lineGeometry.type = "LineString";

        //    object[,] courseLineGeometryCoordinates = new object[coordPairs.Count, 3];

        //    for (int i = 0; i < coordPairs.Count; i++)
        //    {
        //        courseLineGeometryCoordinates[i, 0] = coordPairs[i].lat;
        //        courseLineGeometryCoordinates[i, 1] = coordPairs[i].lon;
        //        courseLineGeometryCoordinates[i, 2] = coordPairs[i].ele;
        //    }
        //    lineGeometry.coordinates = courseLineGeometryCoordinates;
        //    feature.properties = properties;

        //    return feature;
        //}

        //private static Feature AddFirstLastLineFeature(Properties properties, CourseDataCSV courseDataCSV)
        //{
        //    Feature feature = new Feature();
        //    feature.type = "Feature";

        //    Geometry lineGeometry = new Geometry();
        //    feature.geometry = lineGeometry;
        //    lineGeometry.type = "LineString";

        //    object[,] coordinates = new object[2, 2];

        //    if (properties.startline)
        //    {
        //        coordinates[0, 0] = courseDataCSV.StartLatRight;
        //        coordinates[0, 1] = courseDataCSV.StartLonRight;
        //        coordinates[1, 0] = courseDataCSV.StartLatLeft;
        //        coordinates[1, 1] = courseDataCSV.StartLonLeft;
        //    }
        //    else if (properties.finishline)
        //    {
        //        coordinates[0, 0] = courseDataCSV.FinishLatRight;
        //        coordinates[0, 1] = courseDataCSV.FinishLonRight;
        //        coordinates[1, 0] = courseDataCSV.FinishLatLeft;
        //        coordinates[1, 1] = courseDataCSV.FinishLonLeft;
        //    }

        //    lineGeometry.coordinates = coordinates;
        //    feature.properties = properties;

        //    return feature;
        //}

        //private static void SerializeAndWriteJson(CourseInfo courseInfo, string raceid)
        //{
        //    string CourseInfoJson = JsonConvert.SerializeObject(courseInfo, Newtonsoft.Json.Formatting.Indented,
        //        new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
        //    File.WriteAllText(raceid, CourseInfoJson);
        //}

        //private static string SerializeJson(CourseInfo courseInfo, string raceid)
        //{
        //    string CourseInfoJson = JsonConvert.SerializeObject(courseInfo, Newtonsoft.Json.Formatting.Indented,
        //        new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
        //    Console.WriteLine($"Serialization successful: {raceid}");
        //    return CourseInfoJson;
        //}
    }

}
