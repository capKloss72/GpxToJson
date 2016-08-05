﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackervonfig.Props;
using C = System.Data.SqlClient; // System.Data.dll
using D = System.Data;

namespace Trackervonfig.Utilities
{
    static class TomrUtils
    {
        public static void InsertRider(IEventData riderdata)
        {
            using (var connection = new C.SqlConnection(GetConnectionStringByName("tomr-sql")))
            {
                connection.Open();
                C.SqlParameter parameter;

                RiderDataCSV riderData = riderdata as RiderDataCSV;

                using (var command = new C.SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = D.CommandType.Text;
                    command.CommandText = @"if NOT exists(select * from dbo.RiderDetails where RiderId = @RiderId)
                                                INSERT INTO dbo.RiderDetails
                                                    (RiderId, Name, Team, IsCurrent)
                                                        OUTPUT INSERTED.RiderId
                                                            VALUES (@RiderId, @Name, @Team, @IsCurrent); ";

                    parameter = new C.SqlParameter("@RiderId", D.SqlDbType.NVarChar, 50);
                    parameter.Value = riderData.RiderId;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@Name", D.SqlDbType.NVarChar, 255);
                    parameter.Value = riderData.Name;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@Team", D.SqlDbType.NVarChar, 255);
                    parameter.Value = riderData.Team;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@IsCurrent", D.SqlDbType.Int);
                    parameter.Value = riderData.IsCurrent;
                    command.Parameters.Add(parameter);

                    string riderId = (string)command.ExecuteScalar();
                    Console.WriteLine($"The inserted RiderId is = {riderId}.");
                }
            }
        }

        public static void UpdateRider(RiderDataCSV riderdata)
        {
            using (var connection = new C.SqlConnection(GetConnectionStringByName("tomr-sql")))
            {
                connection.Open();
                C.SqlParameter parameter;

                using (var command = new C.SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = D.CommandType.Text;
                    command.CommandText = @"update dbo.RiderDetails
                                                set RiderId = @RiderId, Name = @Name, Team = @Team, IsCurrent = @IsCurrent
                                                    where RiderId = @RiderId";

                    parameter = new C.SqlParameter("@RiderId", D.SqlDbType.NVarChar, 50);
                    parameter.Value = riderdata.RiderId;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@Name", D.SqlDbType.NVarChar, 255);
                    parameter.Value = riderdata.Name;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@Team", D.SqlDbType.NVarChar, 255);
                    parameter.Value = riderdata.Team;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@IsCurrent", D.SqlDbType.Int);
                    parameter.Value = riderdata.IsCurrent;
                    command.Parameters.Add(parameter);

                    string riderId = (string)command.ExecuteScalar();
                    Console.WriteLine($"The updated RiderId is = {riderId}.");
                }
            }
        }

        private static void InsertRace(RaceDataCSV racedata)
        {
            using (var connection = new C.SqlConnection(GetConnectionStringByName("tomr-sql")))
            {
                connection.Open();
                C.SqlParameter parameter;

                using (var command = new C.SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = D.CommandType.Text;
                    command.CommandText = @"if NOT exists(select * from dbo.RaceDetails where RaceId = @RaceId)
                                                INSERT INTO dbo.RaceDetails
                                                    (RaceId, RaceName, IsCurrent)
                                                        OUTPUT INSERTED.RaceId
                                                            VALUES (@RaceId, @RaceName, @IsCurrent);";

                    parameter = new C.SqlParameter("@RaceId", D.SqlDbType.NVarChar, 50);
                    parameter.Value = racedata.RaceId;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@RaceName", D.SqlDbType.NVarChar, 255);
                    parameter.Value = racedata.RaceName;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@IsCurrent", D.SqlDbType.Int);
                    parameter.Value = racedata.IsCurrent;
                    command.Parameters.Add(parameter);

                    string raceId = (string)command.ExecuteScalar();
                    Console.WriteLine($"The inserted RaceId is = {raceId}.");
                }
            }
        }

        private static void UpdateRace(RaceDataCSV racedata)
        {
            using (var connection = new C.SqlConnection(GetConnectionStringByName("tomr-sql")))
            {
                connection.Open();
                C.SqlParameter parameter;

                using (var command = new C.SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = D.CommandType.Text;
                    command.CommandText = @"update dbo.RaceDetails
                                                set RaceId = @RaceId, RaceName = @RaceName, IsCurrent = @IsCurrent
                                                    where RaceId = @RaceId";

                    parameter = new C.SqlParameter("@RaceId", D.SqlDbType.NVarChar, 50);
                    parameter.Value = racedata.RaceId;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@RaceName", D.SqlDbType.NVarChar, 255);
                    parameter.Value = racedata.RaceName;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@IsCurrent", D.SqlDbType.Int);
                    parameter.Value = racedata.IsCurrent;
                    command.Parameters.Add(parameter);

                    string raceId = (string)command.ExecuteScalar();
                    Console.WriteLine($"The updated RaceId is = {raceId}.");
                }
            }
        }

        private static bool UpdateRace2(IEventData rd)
        {
            using (var connection = new C.SqlConnection(GetConnectionStringByName("tomr-sql")))
            {
                connection.Open();
                C.SqlParameter parameter;

                RaceDataCSV racedata = rd as RaceDataCSV;

                using (var command = new C.SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = D.CommandType.Text;
                    command.CommandText = @"update dbo.RaceDetails
                                                set RaceId = @RaceId, RaceName = @RaceName, IsCurrent = @IsCurrent
                                                    where RaceId = @RaceId";

                    parameter = new C.SqlParameter("@RaceId", D.SqlDbType.NVarChar, 50);
                    parameter.Value = racedata.RaceId;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@RaceName", D.SqlDbType.NVarChar, 255);
                    parameter.Value = racedata.RaceName;
                    command.Parameters.Add(parameter);

                    parameter = new C.SqlParameter("@IsCurrent", D.SqlDbType.Int);
                    parameter.Value = racedata.IsCurrent;
                    command.Parameters.Add(parameter);

                    string raceId = (string)command.ExecuteScalar();
                    Console.WriteLine($"The updated RaceId is = {raceId}.");

                    return String.IsNullOrEmpty(raceId);
                }
            }
        }

        public static void InsertEventDetails2(string eventCSVFile)
        {
            CsvReader raceDataCSV = null;
            var eventData = ConfigurationManager.AppSettings[eventCSVFile];
            try
            {
                raceDataCSV = new CsvReader(File.OpenText(eventData));
                raceDataCSV.Configuration.SkipEmptyRecords = true;

                switch (eventCSVFile)
                {
                    case "rider_data":
                        InsertRiders2(raceDataCSV.GetRecords<RaceDataCSV>(), UpdateRace2);
                        break;
                    case "race_data":
                        InsertRiders2(raceDataCSV.GetRecords<RaceDataCSV>(), UpdateRace2);
                        break;
                    default:
                        break;
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Unable to locate: {eventData}");
                Console.ReadLine();
            }
        }

        public static void InsertEventDetails(string eventCSVFile)
        {
            CsvReader raceDataCSV = null;
            var eventData = ConfigurationManager.AppSettings[eventCSVFile];
            try
            {
                raceDataCSV = new CsvReader(File.OpenText(eventData));
                raceDataCSV.Configuration.SkipEmptyRecords = true;

                switch (eventCSVFile)
                {
                    case "rider_data":
                        InsertRiders(raceDataCSV);
                        break;
                    case "race_data":
                        InsertRaces(raceDataCSV);
                        break;
                    default:
                        break;
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Unable to locate: {eventData}");
                Console.ReadLine();
            }
        }

        public static void UpdateEventDetails(string eventCSVFile)
        {
            CsvReader raceDataCSV = null;
            var eventData = ConfigurationManager.AppSettings[eventCSVFile];
            try
            {
                raceDataCSV = new CsvReader(File.OpenText(eventData));
                raceDataCSV.Configuration.SkipEmptyRecords = true;

                switch (eventCSVFile)
                {
                    case "rider_data":
                        InsertRiders(raceDataCSV);
                        break;
                    case "race_data":
                        InsertRaces(raceDataCSV);
                        break;
                    default:
                        break;
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Unable to locate: {eventData}");
                Console.ReadLine();
            }
        }

        private static void InsertRiders2(IEnumerable<IEventData> details, Func<IEventData, bool> method)
        {
            //var details = csv.GetRecords<IEventData>();
            foreach (var detail in details)
            {
                method(detail);
            }
        }

        private static void InsertRiders(CsvReader csv)
        {
            var details = csv.GetRecords<RiderDataCSV>();
            foreach (var detail in details)
            {
                InsertRider(detail);
            }
        }

        private static void InsertRaces(CsvReader csv)
        {
            var details = csv.GetRecords<RaceDataCSV>();
            foreach (var detail in details)
            {
                InsertRace(detail);
            }
        }

        //public static void ReadInsertRiderDetails()
        //{
        //    CsvReader raceDataCSV = null;
        //    var riderdata = ConfigurationManager.AppSettings["rider_data"];
        //    try
        //    {
        //        raceDataCSV = new CsvReader(File.OpenText(riderdata));
        //        raceDataCSV.Configuration.SkipEmptyRecords = true;
        //        var details = raceDataCSV.GetRecords<RiderDataCSV>();

        //        foreach (var detail in details)
        //        {
        //            InsertRider(detail);
        //        }
        //    }
        //    catch (DirectoryNotFoundException e)
        //    {
        //        Console.WriteLine($"Unable to locate: {riderdata}");
        //        Console.ReadLine();
        //    }
        //}

        //public static void ReadInsertRaceDetails()
        //{
        //    CsvReader raceDataCSV = null;
        //    var riderdata = ConfigurationManager.AppSettings["race_data"];
        //    try
        //    {
        //        raceDataCSV = new CsvReader(File.OpenText(riderdata));
        //        raceDataCSV.Configuration.SkipEmptyRecords = true;
        //        var details = raceDataCSV.GetRecords<RaceDataCSV>();

        //        foreach (var detail in details)
        //        {
        //            InsertRace(detail);
        //        }
        //    }
        //    catch (DirectoryNotFoundException e)
        //    {
        //        Console.WriteLine($"Unable to locate: {riderdata}");
        //        Console.ReadLine();
        //    }
        //}

        public static void SelectRiderDetailsRows(C.SqlConnection connection)
        {
            using (var command = new C.SqlCommand())
            {
                command.Connection = connection;
                command.CommandType = D.CommandType.Text;

                command.CommandText = @"select * from dbo.RiderDetails";
                C.SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}",
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2));
                }
            }
        }

        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
