using Guru.DataProcessor;
using Guru.DataProcessor.PositionHandler;
using Guru.Rover.WebApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace Guru.Rover.WebApplication.Controllers.Models
{
    public static class ModelUtility
    {
        public static List<RoverPositionViewModel> GetInitialRoverPositions(this Dictionary<string, DataFromCsv> data)
        {
            var roverPositions = new List<RoverPositionViewModel>();

            foreach (var item in data)
            {
                var roverName = item.Key;
                var rovercurrentPosition = item.Value.LandingPosition;
                var roverPosition = GetIndividualRoverIniitPositions(roverName, rovercurrentPosition);
                roverPositions.Add(roverPosition);
            }

            return roverPositions;
        }

        public static List<RoverPositionViewModel> GetNextMoveCommand(this Dictionary<string, DataFromCsv> data)
        {
            var viweModel = GetNewRoverPosition(data);

            return viweModel;
        }

        private static RoverPositionViewModel GetIndividualRoverIniitPositions(string roverName, RoverPositionsData item)
        {
            var initPositions = item;
            var roverPositionViewModel = new RoverPositionViewModel();
            roverPositionViewModel.CurrentX = initPositions.Xaxis;
            roverPositionViewModel.CurrentY = initPositions.Yaxis;
            roverPositionViewModel.RoverText = RoverMessage(roverName, item.CompassPosition , initPositions.Xaxis, initPositions.Yaxis, "Landing");
            return roverPositionViewModel;
        }

        private static List<RoverPositionViewModel> GetNewRoverPosition(Dictionary<string, DataFromCsv> data)
        {
            var viweModel = new List<RoverPositionViewModel>();
            foreach (var item in data)
            {
                var roverName = item.Key;
                RoverPositionViewModel roverPositionViewModel;
                var nextCommand = item.Value.NextCommand;
                if (nextCommand.Any())
                {
                    var nextAction = item.Value.NextCommand.Dequeue();
                    var initPositions = item.Value.LandingPosition;
                    if (nextAction == 'M')
                    {
                        roverPositionViewModel = GetIndividualRoverPositionsAfterMove(item);
                        AppSession.Handler.StoreMoveInSession(roverName, roverPositionViewModel);
                    }
                    else
                    {
                        roverPositionViewModel = GetIndividualRoverPositionsAfterRotate(roverName, initPositions, nextAction);
                    }
                    viweModel.Add(roverPositionViewModel);
                    //Note: Break after finding one rover details, if more than one rovers move together break condition will change
                    break;
                }
            }
            return viweModel;
        }

        private static RoverPositionViewModel GetIndividualRoverPositionsAfterRotate(string roverName, RoverPositionsData currentPositions, char nextAction)
        {
            var currentDirection = currentPositions.CompassPosition ;
            var directionChanger = new RoverRotatorHandler();
            var nextDirection = directionChanger.GetNextCompassPosition(currentDirection, nextAction);

            

            var roverPositionViewModel = new RoverPositionViewModel
            {
                CurrentX = currentPositions.Xaxis,
                CurrentY = currentPositions.Yaxis,
                PerviousX = currentPositions.Xaxis,
                PerviousY = currentPositions.Yaxis,
                RoverText = RoverMessage(roverName, nextDirection, currentPositions.Xaxis, currentPositions.Yaxis, "Rotate " + currentPositions.CompassPosition   + " to " + nextDirection)
            };
            AppSession.Handler.StoreNewDirectionToSession(roverName, nextDirection);
            return roverPositionViewModel;
        }

        private static RoverPositionViewModel GetIndividualRoverPositionsAfterMove(KeyValuePair<string, DataFromCsv> item)
        {
            var mover = new RoverMoverHandler();
            var initPositions = item.Value.LandingPosition;
            var nextMove = mover.Move(item.Value.LandingPosition);
            var roverPositionViewModel = new RoverPositionViewModel
            {
                CurrentX = nextMove.CurrenX,
                PerviousX = nextMove.PreviousX,
                CurrentY = nextMove.CurrenY,
                PerviousY = nextMove.PreviousY,
                RoverText = RoverMessage(item.Key, item.Value.LandingPosition.CompassPosition , nextMove.CurrenX, nextMove.CurrenY, "Move to " + initPositions.CompassPosition )
            };
            return roverPositionViewModel;
        }

        /// <summary>
        ///  Message shown on each rover
        /// </summary>
        /// <param name="roverName"></param>
        /// <param name="currentDirection"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="currentAction"></param>
        /// <returns></returns>
        private static string RoverMessage(string roverName, char currentDirection, int x, int y, string currentAction)
        {
            return string.Format("{0} | {1}X{2}Y{3} | {4}", roverName, currentDirection, x, y, currentAction);
        }
    }
}