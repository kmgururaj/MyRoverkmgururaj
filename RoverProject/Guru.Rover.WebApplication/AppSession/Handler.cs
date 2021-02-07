using Guru.DataProcessor;
using Guru.DataProcessor.DataProcessing;
using Guru.DataProcessor.FieldParser;
using Guru.Rover.WebApplication.Models;
using System.Collections.Generic;
using System.Web;

namespace Guru.Rover.WebApplication.AppSession
{
    public static class Handler
    {
        private const string CsvDataSessionId = "CsvDataSessionId";

        public static Dictionary<string, DataFromCsv> CsvData
        {
            get
            {
                return (Dictionary<string, DataFromCsv>)HttpContext.Current.Session[CsvDataSessionId];
            }
            set
            {
                HttpContext.Current.Session[CsvDataSessionId] = value;
            }
        }

        public static Dictionary<string, DataFromCsv> InitSession()
        {
            var csvPath = HttpContext.Current.Server.MapPath(@"~/CsvData/Moments.csv");
            var csvFileParser = new CsvFileParser(csvPath);
            var csvDataReader = new CsvDataReader(csvFileParser);
            CsvData = csvDataReader.Start();
            return CsvData;
        }

        public static void StoreNewDirectionToSession(string roverName, char currentDirection)
        {
            CsvData[roverName].LandingPosition.CompassPosition = currentDirection;
        }

        public static void StoreMoveInSession(string roverName, RoverPositionViewModel roverPositionViewModel)
        {
            CsvData[roverName].LandingPosition.Xaxis = roverPositionViewModel.CurrentX;
            CsvData[roverName].LandingPosition.Yaxis = roverPositionViewModel.CurrentY;
        }

    }
}