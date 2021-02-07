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
        
        /// <summary>
        /// Is One Rover Completes its Move to Start Another
        /// if true = Rover one completes all its move and rover two completes its move
        /// if false = Rover one done its one move, rover two done its move this action is cyclic
        /// </summary>
        public static readonly bool IsOneRoverCompletesItsMoveToStartAnother = false;
    }
}
