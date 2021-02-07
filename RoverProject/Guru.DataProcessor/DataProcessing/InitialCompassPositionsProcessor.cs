using System;
using System.Linq;

namespace Guru.DataProcessor.DataProcessing
{
    /// <summary>
    /// Class to process initial Compass position from CSV
    /// </summary>
    internal class InitialCompassPositionsProcessor
    {
        /// <summary>
        /// Initial Compass Positions RAW data received from CSV
        /// </summary>
        private readonly string compassPositionRawDataInternal;


        /// <summary>
        /// Initialise object of InitialCompassPositionsProcessor
        /// </summary>
        /// <param name="compassPositionRawData">Compass Position Raw Data</param>
        public InitialCompassPositionsProcessor(string compassPositionRawData)
        {
            compassPositionRawDataInternal = compassPositionRawData;
        }

        /// <summary>
        /// Start Processing RAW data
        /// </summary>
        /// <returns>Positions of Rover</returns>
        public RoverPositionsData Start()
        {
            var initPositions = compassPositionRawDataInternal.Where(e => e != ' ').ToArray();
            if (initPositions.Length != 3)
            {
                throw new ArgumentException(string.Format("Invalid Argument {0}", compassPositionRawDataInternal));
            }

            var firstCharXaxis = initPositions[0];
            var xAxis = ConvertCharToInt(firstCharXaxis);
            var secondCharYaxis = initPositions[1];
            var yAxis = ConvertCharToInt(secondCharYaxis);
            var direction = char.ToUpper(initPositions[2]);

            var command = new RoverPositionsData
            {
                CompassPosition  = direction,
                Xaxis = xAxis,
                Yaxis = yAxis
            };

            return command;
        }

        /// <summary>
        /// Convert char to int for axis
        /// </summary>
        /// <param name="value"></param>
        /// <returns>char to int</returns>
        private int ConvertCharToInt(char value)
        {
            int outX;
            var isSucessX = int.TryParse(value.ToString(), out outX);
            if (!isSucessX)
            {
                throw new System.ArgumentException(string.Format("Invalid Argument {0}", value));
            }
            return outX;
        }
    }
}
