using Guru.DataProcessor.FieldParser;
using System.Collections.Generic;

namespace Guru.DataProcessor.DataProcessing
{
    /// <summary>
    /// CSV data reader
    /// </summary>
    public class CsvDataReader
    {
        private readonly ICsvFileParser csvFileParserInternal;

        /// <summary>
        /// Initialise object
        /// </summary>
        /// <param name="filePath"></param>
        public CsvDataReader(ICsvFileParser csvFileParser)
        {
            csvFileParserInternal = csvFileParser;
        }


        /// <summary>
        /// Start Processing CSV data
        /// </summary>
        /// <returns>CSV data collection</returns>
        public Dictionary<string, DataFromCsv> Start()
        {
            var command = new Dictionary<string, DataFromCsv>();
            using (csvFileParserInternal)
            {
                var results = csvFileParserInternal.ReadFields();

                foreach (var item in results)
                {
                    var lineNumber = csvFileParserInternal.LineNumber;

                    if (item.Length != 2)
                    {
                        throw new System.ArgumentOutOfRangeException(string.Format("Invalid File Format {0}. Expecting two columns in CSV", csvFileParserInternal.FilePath)); //Ideally fileFormat exception
                    }

                    var roverName = string.Format("R{0}", lineNumber);

                    var csvData = GetInstructionsFromCsv(item);
                    command.Add(roverName, csvData);
                }
               
            }
            return command;
        }


        private static DataFromCsv GetInstructionsFromCsv(string[] fields)
        {
            var firstField = fields[0];
            var initialPositionProcessor = new InitialCompassPositionsProcessor(firstField);
            var initialCommand = initialPositionProcessor.Start();

            var secondField = fields[1];
            var sequenceCommandProcessor = new InitialCommandProcessor(secondField);
            var sequenceCommans = sequenceCommandProcessor.Start();
            var roverCommands = new Queue<char>();
            foreach (var sequenceCommand in sequenceCommans)
            {
                roverCommands.Enqueue(sequenceCommand);
            }

            var csvData = new DataFromCsv()
            {
                LandingPosition = initialCommand,
                NextCommand = roverCommands
            };

            return csvData;
        }
    }
}
