using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Guru.Rover.WebApplication.Controllers.Models;
using Guru.DataProcessor;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.SessionState;
using Moq;
using System.Web.Routing;
using System.Reflection;
using System.Web.Mvc;

namespace Guru.Rover.WebApplication.Test.Models
{
    [TestClass]
    public class ModelUtilityTests
    {
        [TestMethod]
        public void Test_InitialRoverPositions_VerifyPositions()
        {
            var data = new Dictionary<string, DataFromCsv>();
            var r1RoverPositionsData = new RoverPositionsData
            {
                CompassPosition = 'N',
                Xaxis = 1,
                Yaxis = 2
            };
            var r1NextCommand = new Queue<char>();
            r1NextCommand.Enqueue('M');
            r1NextCommand.Enqueue('R');
            r1NextCommand.Enqueue('L');
            var r1DataFromCsv = new DataFromCsv
            {
                LandingPosition = r1RoverPositionsData,
                NextCommand = r1NextCommand
            };

            data.Add("R1", r1DataFromCsv);
            var result = data.GetInitialRoverPositions();

            Assert.AreEqual("R1 | NX1Y2 | Landing", result[0].RoverText);
            Assert.AreEqual(1, result[0].CurrentX);
            Assert.AreEqual(2, result[0].CurrentY);
            Assert.AreEqual(0, result[0].PerviousX);
            Assert.AreEqual(0, result[0].PerviousY);
        }


        [TestMethod]
        public void Test_NextMoveCommand_VerifyPositions()
        {
            HttpContext.Current = GetHttpContext();

            var data = new Dictionary<string, DataFromCsv>();
            var r1RoverPositionsData = new RoverPositionsData
            {
                CompassPosition = 'N',
                Xaxis = 1,
                Yaxis = 2
            };
            var r1NextCommand = new Queue<char>();
            r1NextCommand.Enqueue('M');
            r1NextCommand.Enqueue('R');
            r1NextCommand.Enqueue('M');
            r1NextCommand.Enqueue('L');
            r1NextCommand.Enqueue('M');
            var r1DataFromCsv = new DataFromCsv
            {
                LandingPosition = r1RoverPositionsData,
                NextCommand = r1NextCommand
            };

            data.Add("R1", r1DataFromCsv);
            HttpContext.Current.Session.Add("CsvDataSessionId", data);
            var commandOne = data.GetNextMoveCommand();
            var commandTwo = data.GetNextMoveCommand();
            var commandThree = data.GetNextMoveCommand();
            var commandFour = data.GetNextMoveCommand();
            var commandFive = data.GetNextMoveCommand();

            Assert.AreEqual("R1 | NX1Y3 | Move to N", commandOne[0].RoverText);
            Assert.AreEqual(1, commandOne[0].CurrentX);
            Assert.AreEqual(3, commandOne[0].CurrentY);
            Assert.AreEqual(1, commandOne[0].PerviousX);
            Assert.AreEqual(2, commandOne[0].PerviousY);

            Assert.AreEqual("R1 | EX1Y3 | Rotate N to E", commandTwo[0].RoverText);
            Assert.AreEqual(1, commandTwo[0].CurrentX);
            Assert.AreEqual(3, commandTwo[0].CurrentY);
            Assert.AreEqual(1, commandTwo[0].PerviousX);
            Assert.AreEqual(3, commandTwo[0].PerviousY);

            Assert.AreEqual("R1 | EX2Y3 | Move to E", commandThree[0].RoverText);
            Assert.AreEqual(2, commandThree[0].CurrentX);
            Assert.AreEqual(3, commandThree[0].CurrentY);
            Assert.AreEqual(1, commandThree[0].PerviousX);
            Assert.AreEqual(3, commandThree[0].PerviousY);

            Assert.AreEqual("R1 | NX2Y3 | Rotate E to N", commandFour[0].RoverText);
            Assert.AreEqual(2, commandFour[0].CurrentX);
            Assert.AreEqual(3, commandFour[0].CurrentY);
            Assert.AreEqual(2, commandFour[0].PerviousX);
            Assert.AreEqual(3, commandFour[0].PerviousY);

            Assert.AreEqual("R1 | NX2Y4 | Move to N", commandFive[0].RoverText);
            Assert.AreEqual(1, commandFive.Count);
            Assert.AreEqual(2, commandFive[0].CurrentX);
            Assert.AreEqual(4, commandFive[0].CurrentY);
            Assert.AreEqual(2, commandFive[0].PerviousX);
            Assert.AreEqual(3, commandFive[0].PerviousY);

        }

        private static HttpContext GetHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            var httpResponce = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard, new[] { typeof(HttpSessionStateContainer) }, null).Invoke(new object[] { sessionContainer });
            return httpContext;
        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://example.com/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);


            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);



            return httpContext;
        }

    }


}
