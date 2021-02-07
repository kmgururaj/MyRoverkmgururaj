using System.Collections.Generic;

namespace Guru.DataProcessor.PositionHandler
{
    /// <summary>
    /// Rover Rotator
    /// </summary>
    public class RoverRotatorHandler
    {
        /// <summary>
        /// Known compass positions
        /// </summary>
        private readonly List<char> directions;

        /// <summary>
        /// Initialise Rover Rotator
        /// </summary>
        public RoverRotatorHandler()
        {
            directions = new List<char> { 'N', 'E', 'S', 'W' };
        }

        /// <summary>
        /// Get Next Compass Position
        /// </summary>
        /// <param name="currentCompassPosition">Current Compass Position</param>
        /// <param name="nextRotationDirection">Next Rotation Direction</param>
        /// <returns>Next Compass Position</returns>
        public char GetNextCompassPosition(char currentCompassPosition, char nextRotationDirection)
        {
            var directionChanger = new RoverRotatorHandler();
            char newDirection;
            switch (nextRotationDirection)
            {
                case 'L':
                    {
                        newDirection = directionChanger.MoveLeftGetCompassPosition(currentCompassPosition);
                    }
                    break;
                case 'R':
                    {
                        newDirection = directionChanger.MoveRightGetCompassPosition(currentCompassPosition);
                    }
                    break;
                default:
                    {
                        newDirection = currentCompassPosition;
                    }
                    break;
            }

            return newDirection;
        }

        /// <summary>
        /// Move Left Get Compass Position
        /// </summary>
        /// <param name="currentCompassPosition">Current Compass Position</param>
        /// <returns>Next Compass Position</returns>
        private char MoveLeftGetCompassPosition(char currentCompassPosition)
        {
            var currentCompassIndex = directions.IndexOf(currentCompassPosition);
            var nextCompassIndex = currentCompassIndex - 1;

            if (nextCompassIndex < 0)
            {
                nextCompassIndex = directions.Count - 1;
            }

            var nextCompassPosition = directions[nextCompassIndex];

            return nextCompassPosition;
        }

        /// <summary>
        /// Move Right Get Compass Position
        /// </summary>
        /// <param name="currentCompassPosition">Current Compass Position</param>
        /// <returns>Next Compass Position</returns>
        private char MoveRightGetCompassPosition(char currentCompassPosition)
        {
            var currentCompassIndex = directions.IndexOf(currentCompassPosition);
            var nextCompassIndex = currentCompassIndex + 1;

            if (nextCompassIndex > directions.Count - 1)
            {
                nextCompassIndex = 0;
            }

            var nextCompassPosition = directions[nextCompassIndex];

            return nextCompassPosition;
        }
    }
}
