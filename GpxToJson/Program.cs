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
        //Application options
        private static string APPOPTIONS = ConfigurationManager.AppSettings["appcoptions"];

        static void Main(string[] args)
        {

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
    }

}
