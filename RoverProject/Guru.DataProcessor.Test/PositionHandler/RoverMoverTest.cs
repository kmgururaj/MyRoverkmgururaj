using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guru.DataProcessor.PositionHandler;

namespace Guru.DataProcessor.Test.PositionHandler
{
    /// <summary>
    /// Test Movement of rover in all direction. North, East, West and South
    /// </summary>
    [TestClass]
    public class RoverMoverTests
    {
        [TestMethod]
        public void Test_CurrentCompassNorth_TestMove_VerifyYIncriments()
        {
            var roverPositionsData = new RoverPositionsData()
            {
                CompassPosition = 'N',
                Xaxis = 4,
                Yaxis = 4
            };

            var roverMoverHandler = new RoverMoverHandler();
            var result = roverMoverHandler.Move(roverPositionsData);

            //For West move expect current Y to incriment by 1 and other coordinates unchanhed
            Assert.AreEqual(roverPositionsData.Xaxis, result.CurrenX);
            Assert.AreEqual(roverPositionsData.Yaxis + 1, result.CurrenY);
            Assert.AreEqual(roverPositionsData.Xaxis, result.PreviousX);
            Assert.AreEqual(roverPositionsData.Yaxis, result.PreviousY);
        }

        [TestMethod]
        public void Test_CurrentCompassEast_TestMove_VerifyXIncriments()
        {
            var roverPositionsData = new RoverPositionsData()
            {
                CompassPosition = 'E',
                Xaxis = 4,
                Yaxis = 4
            };

            var roverMoverHandler = new RoverMoverHandler();
            var result = roverMoverHandler.Move(roverPositionsData);

            //For West move expect current X to incriment by 1
            Assert.AreEqual(roverPositionsData.Xaxis + 1, result.CurrenX);
            Assert.AreEqual(roverPositionsData.Yaxis, result.CurrenY);
            Assert.AreEqual(roverPositionsData.Xaxis, result.PreviousX);
            Assert.AreEqual(roverPositionsData.Yaxis, result.PreviousY);
        }

        [TestMethod]
        public void Test_CurrentCompassWest_TestMove_VerifyXDecriments()
        {
            var roverPositionsData = new RoverPositionsData()
            {
                CompassPosition = 'W',
                Xaxis = 4,
                Yaxis = 4
            };

            var roverMoverHandler = new RoverMoverHandler();
            var result = roverMoverHandler.Move(roverPositionsData);

            //For West move expect current X to decriment by 1
            Assert.AreEqual(roverPositionsData.Xaxis - 1, result.CurrenX);
            Assert.AreEqual(roverPositionsData.Yaxis, result.CurrenY);
            Assert.AreEqual(roverPositionsData.Xaxis, result.PreviousY);
            Assert.AreEqual(roverPositionsData.Yaxis, result.PreviousY);
        }

        [TestMethod]
        public void Test_CurrentCompassSouth_TestMove_VerifyYDecriments()
        {
            var roverPositionsData = new RoverPositionsData()
            {
                CompassPosition = 'S',
                Xaxis = 4,
                Yaxis = 4
            };

            var roverMoverHandler = new RoverMoverHandler();
            var result = roverMoverHandler.Move(roverPositionsData);

            //For West move expect current Y to decriment by 1
            Assert.AreEqual(result.CurrenX, roverPositionsData.Xaxis);
            Assert.AreEqual(result.CurrenY, roverPositionsData.Yaxis - 1);

            Assert.AreEqual(result.PreviousX, roverPositionsData.Xaxis);
            Assert.AreEqual(result.PreviousY, roverPositionsData.Yaxis);
        }

        [TestMethod]
        public void Test_CurrentCompassSouth_TestMoveOutOfBoundary_VerifyXYNoChange()
        {
            var roverPositionsData = new RoverPositionsData()
            {
                CompassPosition = 'S',
                Xaxis = 0,
                Yaxis = 0
            };

            var roverMoverHandler = new RoverMoverHandler();
            var result = roverMoverHandler.Move(roverPositionsData);

            //For West move expect current Y to decriment by 1
            Assert.AreEqual(result.CurrenX, 0);
            Assert.AreEqual(result.CurrenY, 0);

            Assert.AreEqual(result.PreviousX, 0);
            Assert.AreEqual(result.PreviousY, 0);
        }

        [TestMethod]
        public void Test_CurrentCompassNorth_TestMoveOutOfBoundary_VerifyXYNoChange()
        {
            var roverPositionsData = new RoverPositionsData()
            {
                CompassPosition = 'N',
                Xaxis = ApplicationConstants.NumberOfRows,
                Yaxis = ApplicationConstants.NumberOfRows
            };

            var roverMoverHandler = new RoverMoverHandler();
            var result = roverMoverHandler.Move(roverPositionsData);

            //For West move expect current Y to decriment by 1
            Assert.AreEqual(result.CurrenX, ApplicationConstants.NumberOfRows);
            Assert.AreEqual(result.CurrenY, ApplicationConstants.NumberOfRows);

            Assert.AreEqual(result.PreviousX, ApplicationConstants.NumberOfRows);
            Assert.AreEqual(result.PreviousY, ApplicationConstants.NumberOfRows);
        }

    }
}
