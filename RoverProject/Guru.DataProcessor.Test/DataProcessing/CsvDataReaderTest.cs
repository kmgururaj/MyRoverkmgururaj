using Guru.DataProcessor.DataProcessing;
using Guru.DataProcessor.FieldParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Guru.DataProcessor.Test.DataProcessing
{
    [TestClass]
    public class CsvDataReaderTests
    {
        [TestMethod]
        public void Test_ReadCsvWithOneRow_VerifyCommands()
        {
            var mockICsvFileParser = GetMockObjectWithOneRow();

            var csvDataReader = new CsvDataReader(mockICsvFileParser.Object);
            var result = csvDataReader.Start();

            var landingPositionRowOne = result["R1"].LandingPosition;
            var nextCommandRowOne = result["R1"].NextCommand;
            var commands = new StringBuilder();
            while (nextCommandRowOne.Any())
            {
                var command = nextCommandRowOne.Dequeue();
                commands.Append(command);
            }

            //Should rerurn one row collection
            Assert.AreEqual(1, result.Count);

            Assert.AreEqual('N', landingPositionRowOne.CompassPosition);
            Assert.AreEqual(1, landingPositionRowOne.Xaxis);
            Assert.AreEqual(2, landingPositionRowOne.Yaxis);
            Assert.AreEqual(16, commands.Length);
            Assert.AreEqual("LMRMLMRMLMRMLMRM", commands.ToString());
        }

        [TestMethod]
        public void Test_ReadCsvWithTwoRow_VerifyCommands()
        {
            var mockICsvFileParser = GetMockObjectWithTwoRow();

            var csvDataReader = new CsvDataReader(mockICsvFileParser.Object);
            var result = csvDataReader.Start();

            var landingPositionRowOne = result["R1"].LandingPosition;
            var nextCommandRowOne = result["R1"].NextCommand;
            var commandsOne = new StringBuilder();
            while (nextCommandRowOne.Any())
            {
                var command = nextCommandRowOne.Dequeue();
                commandsOne.Append(command);
            }

            var landingPositionRowTwo = result["R2"].LandingPosition;
            var nextCommandRowTwo = result["R2"].NextCommand;
            var commandsTwo = new StringBuilder();
            while (nextCommandRowTwo.Any())
            {
                var command = nextCommandRowTwo.Dequeue();
                commandsTwo.Append(command);
            }

            //Should rerurn two row collection
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual('N', landingPositionRowOne.CompassPosition);
            Assert.AreEqual(1, landingPositionRowOne.Xaxis);
            Assert.AreEqual(2, landingPositionRowOne.Yaxis);
            Assert.AreEqual(16, commandsOne.Length);
            Assert.AreEqual("LMRMLMRMLMRMLMRM", commandsOne.ToString());

            Assert.AreEqual('E', landingPositionRowTwo.CompassPosition);
            Assert.AreEqual(5, landingPositionRowTwo.Xaxis);
            Assert.AreEqual(3, landingPositionRowTwo.Yaxis);
            Assert.AreEqual(12, commandsTwo.Length);
            Assert.AreEqual("LRMMMMMMMMMR", commandsTwo.ToString());
        }

        private static Mock<ICsvFileParser> GetMockObjectWithOneRow()
        {
            var mockICsvFileParser = new Mock<ICsvFileParser>();

            var list = new List<string[]>();
            var rowOne = RowOne();
            list.Add(rowOne);
            mockICsvFileParser.Setup(e => e.ReadFields()).Returns(list);
            mockICsvFileParser.SetupSequence(e => e.LineNumber).Returns(1);
            mockICsvFileParser.Setup(e => e.FilePath).Returns("SomeCsvFile.CSV");
            return mockICsvFileParser;
        }

        private static Mock<ICsvFileParser> GetMockObjectWithTwoRow()
        {
            var mockICsvFileParser = new Mock<ICsvFileParser>();

            var list = new List<string[]>();
            var rowOne = RowOne();
            list.Add(rowOne);

            var rowTwo = RowTwo();
            list.Add(rowTwo);

            mockICsvFileParser.Setup(e => e.ReadFields()).Returns(list);
            mockICsvFileParser.SetupSequence(e => e.LineNumber).Returns(1).Returns(2);
            mockICsvFileParser.Setup(e => e.FilePath).Returns("SomeCsvFile.CSV");
            return mockICsvFileParser;
        }

        private static string[] RowOne()
        {
            var rowOneInit = "1 2 N";
            var rowOneCommand = "LMRMLMRMLMRMLMRM";
            var rowOne = new string[] { rowOneInit, rowOneCommand };
            return rowOne;
        }

        private static string[] RowTwo()
        {
            var rowOneInit = "5 3 E";
            var rowOneCommand = "LRMMMMMMMMMR";
            var rowOne = new string[] { rowOneInit, rowOneCommand };
            return rowOne;
        }
    }
}
