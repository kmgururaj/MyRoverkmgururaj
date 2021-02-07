namespace Guru.DataProcessor
{
    /// <summary>
    /// Positions
    /// </summary>
    public class RoverPositionsData
    {
        /// <summary>
        /// X-Axis
        /// </summary>
        public int Xaxis { get; set; }

        /// <summary>
        /// Y-Axis
        /// </summary>
        public int Yaxis { get; set; }

        /// <summary>
        /// Compass Position
        /// N - North (Top)
        /// E - East (Right)
        /// S - South (Bottom)
        /// W - WEST (Left)
        /// </summary>
        public char CompassPosition  { get; set; }
    }
}
