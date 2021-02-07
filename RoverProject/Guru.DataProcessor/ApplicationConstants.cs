namespace Guru.DataProcessor
{
    /// <summary>
    /// Application Constants
    /// </summary>
    public static class ApplicationConstants
    {
        /// <summary>
        /// Number of Rows
        /// </summary>
        public static readonly int NumberOfRows = 20;

        /// <summary>
        /// Number of Columns
        /// </summary>
        public static readonly int NumberOfColumns = 25;

        /// <summary>
        /// Time differene between communication to / from rover (in application used for timer)
        /// </summary>
        public static readonly int CommunicationLatenceInMilliseconds = 3000;
    }
}
