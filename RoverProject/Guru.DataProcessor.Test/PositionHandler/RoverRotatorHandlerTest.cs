using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guru.DataProcessor.PositionHandler;

namespace Guru.DataProcessor.Test.PositionHandler
{
    [TestClass]
    public class RoverRotatorHandlerTest
    {
        [TestMethod]
        public void Test_CurrentCompassNorth_RotateRight_VerifyNewCompassIsEast()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('N', 'R');

            //CurrentCompass = North and Rorate = Right. Expected CurrentCompass = East
            Assert.AreEqual('E', result);
        }

        [TestMethod]
        public void Test_CurrentCompassNorth_RotateRight_VerifyNewCompassIsWest()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('N', 'L');

            //CurrentCompass = North and Rorate = Left. Expected CurrentCompass = West
            Assert.AreEqual('W', result);
        }

        [TestMethod]
        public void Test_CurrentCompassEast_RotateRight_VerifyNewCompassIsSouth()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('E', 'R');

            //CurrentCompass = East and Rorate = Right. Expected CurrentCompass = South
            Assert.AreEqual('S', result);
        }

        [TestMethod]
        public void Test_CurrentCompassEast_RotateLeft_VerifyNewCompassIsNorth()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('E', 'L');

            //CurrentCompass = East and Rorate = Left. Expected CurrentCompass = North
            Assert.AreEqual('N', result);
        }

        [TestMethod]
        public void Test_CurrentCompassSouth_RotateRight_VerifyNewCompassIsWest()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('S', 'R');

            //CurrentCompass = South and Rorate = Right. Expected CurrentCompass = West
            Assert.AreEqual('W', result);
        }

        [TestMethod]
        public void Test_CurrentCompassSouth_RotateLeft_VerifyNewCompassIsEast()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('S', 'L');

            //CurrentCompass = South and Rorate = Left. Expected CurrentCompass = East
            Assert.AreEqual('E', result);
        }

        [TestMethod]
        public void Test_CurrentCompassWest_RotateRight_VerifyNewCompassIsNorth()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('W', 'R');

            //CurrentCompass = West and Rorate = Right. Expected CurrentCompass = North
            Assert.AreEqual('N', result);
        }

        [TestMethod]
        public void Test_CurrentCompassWest_RotateLeft_VerifyNewCompassIsSouth()
        {
            var asasa = new RoverRotatorHandler();
            var result = asasa.GetNextCompassPosition('W', 'L');

            //CurrentCompass = West and Rorate = Left. Expected CurrentCompass = South
            Assert.AreEqual('S', result);
        }
    }
}
