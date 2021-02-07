namespace Guru.DataProcessor.PositionHandler
{
    /// <summary>
    /// Class to find cell points (X and Y co-ordinates)
    /// </summary>
    public class RoverMoverHandler
    {
        /// <summary>
        /// Number of grid cells to move
        /// </summary>
        private const int NumberOfPointsToMove = 1;

        /// <summary>
        /// Min Coordinate Value
        /// </summary>
        private const int MinCoordinateValue = 0;

        /// <summary>
        /// Move Points to get current and previous points
        /// </summary>
        /// <returns>New Move Points</returns>
        public RoverAxisCurrentPrevious Move(RoverPositionsData currentPositions)
        {
            char compassPosition = currentPositions.CompassPosition;

            RoverAxisCurrentPrevious newMovePoints = null;
            switch (compassPosition)
            {
                case 'N':
                    {
                        newMovePoints = MoveNorth(currentPositions);
                    }
                    break;
                case 'E':
                    {
                        newMovePoints = MoveEast(currentPositions);
                    }
                    break;
                case 'S':
                    {
                        newMovePoints = MoveSouth(currentPositions);
                    }
                    break;
                case 'W':
                    {
                        newMovePoints = MoveWest(currentPositions);
                    }
                    break;
                default:
                    newMovePoints = UnknownDirection(currentPositions);
                    break;
            }

            return newMovePoints;
        }

        /// <summary>
        /// Get New Move Points If Unknown Direction
        /// </summary>
        /// <returns>New Move Points </returns>
        private RoverAxisCurrentPrevious UnknownDirection(RoverPositionsData currentPositions)
        {
            var newMovePoints = new RoverAxisCurrentPrevious
            {
                CurrenX = currentPositions.Xaxis,
                PreviousX = currentPositions.Xaxis,
                CurrenY = currentPositions.Yaxis,
                PreviousY = currentPositions.Yaxis
            };

            return newMovePoints;
        }

        /// <summary>
        /// Get New Move Points by moving north
        /// </summary>
        /// <returns>New Move Points </returns>
        private RoverAxisCurrentPrevious MoveNorth(RoverPositionsData currentPositions)
        {
            var newY = Incriment(currentPositions.Yaxis);
            if (newY > ApplicationConstants.NumberOfRows)
            {
                newY = ApplicationConstants.NumberOfRows;
            }
            var newMovePoints = new RoverAxisCurrentPrevious
            {
                CurrenX = currentPositions.Xaxis,
                PreviousX = currentPositions.Xaxis,
                CurrenY = newY,
                PreviousY = currentPositions.Yaxis
            };

            return newMovePoints;
        }

        /// <summary>
        /// Get New Move Points by moving south
        /// </summary>
        /// <returns>New Move Points </returns>
        private RoverAxisCurrentPrevious MoveSouth(RoverPositionsData currentPositions)
        {
            var newY = Decriment(currentPositions.Yaxis);
            if (newY < MinCoordinateValue)
            {
                newY = MinCoordinateValue;
            }
            var newMovePoints = new RoverAxisCurrentPrevious
            {
                CurrenX = currentPositions.Xaxis,
                PreviousX = currentPositions.Xaxis,
                PreviousY = currentPositions.Yaxis,
                CurrenY = newY
            };

            return newMovePoints;
        }

        /// <summary>
        /// Get New Move Points by moving east
        /// </summary>
        /// <returns>New Move Points </returns>
        private RoverAxisCurrentPrevious MoveEast(RoverPositionsData currentPositions)
        {
            var newX = Incriment(currentPositions.Xaxis);
            if (newX > ApplicationConstants.NumberOfColumns)
            {
                newX = ApplicationConstants.NumberOfColumns;
            }
            var newMovePoints = new RoverAxisCurrentPrevious
            {
                CurrenX = newX,
                PreviousX = currentPositions.Xaxis,
                PreviousY = currentPositions.Yaxis,
                CurrenY = currentPositions.Yaxis
            };

            return newMovePoints;
        }

        /// <summary>
        /// Get New Move Points by moving West
        /// </summary>
        /// <returns>New Move Points </returns>
        private RoverAxisCurrentPrevious MoveWest(RoverPositionsData currentPositions)
        {
            var newX = Decriment(currentPositions.Xaxis);
            if (newX < MinCoordinateValue)
            {
                newX = MinCoordinateValue;
            }
            var newMovePoints = new RoverAxisCurrentPrevious
            {
                CurrenX = newX,
                PreviousX = currentPositions.Xaxis,
                PreviousY = currentPositions.Yaxis,
                CurrenY = currentPositions.Yaxis
            };

            return newMovePoints;
        }

        /// <summary>
        /// Incriment coordinates
        /// </summary>
        /// <param name="coordiname"></param>
        /// <returns>Incrimentd coordinates</returns>
        private int Incriment(int coordiname)
        {
            var newCoordinate = coordiname + NumberOfPointsToMove;
            return newCoordinate;
        }

        /// <summary>
        /// Decriment Coordinate
        /// </summary>
        /// <param name="coordiname"></param>
        /// <returns>Decrimented Coordinate</returns>
        private int Decriment(int coordiname)
        {
            var newCoordinate = coordiname - NumberOfPointsToMove;
            return newCoordinate;
        }

    }
}
