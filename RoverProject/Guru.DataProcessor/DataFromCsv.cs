using System.Collections.Generic;

namespace Guru.DataProcessor
{
    /// <summary>
    /// Data from CSV
    /// </summary>
    public class DataFromCsv
    {
        /// <summary>
        /// Current Landing Position
        /// </summary>
        public RoverPositionsData LandingPosition { get; set; }

        /// <summary>
        /// Commands to be sent to rover
        /// </summary>
        public Queue<char> NextCommand { get; set; }
    }
}
