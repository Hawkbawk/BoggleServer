using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Dynamic;
using static System.Net.HttpStatusCode;

namespace BoggleTests
{
    /// <summary>
    /// Provides a way to start and stop the IIS web server from within the test
    /// cases.  If something prevents the test cases from stopping the web server,
    /// subsequent tests may not work properly until the stray process is killed
    /// manually.
    /// 
    /// Starting the service this way allows code coverage statistics for the service
    /// to be collected.
    /// </summary>
    public static class IISAgent
    {
        // Reference to the running process
        private static Process process = null;

        // Full path to IIS_EXPRESS executable
        private const string IIS_EXECUTABLE = @"C:\Program Files (x86)\IIS Express\iisexpress.exe";

        // Command line arguments to IIS_EXPRESS
        private const string ARGUMENTS = @"/site:""BoggleService"" /apppool:""Clr4IntegratedAppPool"" /config:""..\..\..\.vs\config\applicationhost.config""";

        /// <summary>
        /// Starts IIS
        /// </summary>
        public static void Start()
        {
            if (process == null)
            {
                ProcessStartInfo info = new ProcessStartInfo(IIS_EXECUTABLE, ARGUMENTS);
                info.WindowStyle = ProcessWindowStyle.Minimized;
                info.UseShellExecute = false;
                process = Process.Start(info);
            }
        }

        /// <summary>
        ///  Stops IIS
        /// </summary>
        public static void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }
    }

    [TestClass]
    public class ToDoTests
    {
        /// <summary>
        /// This is automatically run prior to all the tests to start the server.
        /// </summary>
        // Remove the comment before the annotation if you want to make it work
        [ClassInitialize]
        public static void StartIIS(TestContext testContext)
        {
            IISAgent.Start();
        }

        /// <summary>
        /// This is automatically run when all tests have completed to stop the server
        /// </summary>
        // Remove the comment before the annotation if you want to make it work
        [ClassCleanup]
        public static void StopIIS()
        {
            IISAgent.Stop();
        }

        private RestClient client = new RestClient("http://localhost:60000/BoggleService/");


        [TestMethod]
        public void TestRegisterOnePlayer()
        {
            //Registering Player 1
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
        }

        [TestMethod]
        public void TestRegisterTwoPlayers()
        {
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);

            string P2Nickname = "Joe2";
            Response r2 = client.DoMethodAsync("POST", "users", P2Nickname).Result;
            Assert.AreEqual(OK, r2.Status);
            Assert.AreEqual(36, r2.Data.Length);
        }

        [TestMethod]
        public void TestJoiningTwoPlayers()
        {
            //Registering Player 1 as "Joe"
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);


            //Registering Player 2 as "Joe2"
            string P2Nickname = "Joe2";
            Response r2 = client.DoMethodAsync("POST", "users", P2Nickname).Result;
            Assert.AreEqual(OK, r2.Status);
            Assert.AreEqual(36, r2.Data.Length);

            dynamic P1 = new ExpandoObject();
            P1.UserToken = r1.Data;
            P1.TimeLimit = 60;
            //P1 and P2 are joining the game
            Response j1 = client.DoMethodAsync("POST", "games", P1).Result;
            Assert.AreEqual(OK, j1.Status);
            Assert.AreEqual("True", j1.Data.IsPending.ToString());


            dynamic P2 = new ExpandoObject();
            P2.UserToken = r2.Data;
            P2.TimeLimit = 60;
            Response j2 = client.DoMethodAsync("POST", "games", P2).Result;
            Assert.AreEqual(OK, j2.Status);
            Assert.AreEqual("False", j2.Data.IsPending.ToString());

            Assert.AreEqual(j1.Data.GameID.ToString(), j2.Data.GameID.ToString());
        }


        [TestMethod]
        public void TestPlayWordNegativeValue()
        {
            //Registering Player 1 as "Joe"
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);


            //Registering Player 2 as "Joe2"
            string P2Nickname = "Joe2";
            Response r2 = client.DoMethodAsync("POST", "users", P2Nickname).Result;
            Assert.AreEqual(OK, r2.Status);
            Assert.AreEqual(36, r2.Data.Length);

            dynamic P1 = new ExpandoObject();
            P1.UserToken = r1.Data;
            P1.TimeLimit = 60;
            //P1 and P2 are joining the game
            Response j1 = client.DoMethodAsync("POST", "games", P1).Result;
            Assert.AreEqual(OK, j1.Status);
            Assert.AreEqual("True", j1.Data.IsPending.ToString());


            dynamic P2 = new ExpandoObject();
            P2.UserToken = r2.Data;
            P2.TimeLimit = 60;
            Response j2 = client.DoMethodAsync("POST", "games", P2).Result;
            Assert.AreEqual(OK, j2.Status);
            Assert.AreEqual("False", j2.Data.IsPending.ToString());

            Assert.AreEqual(j1.Data.GameID.ToString(), j2.Data.GameID.ToString());


            dynamic PlayWord = new ExpandoObject();
            PlayWord.UserToken = r2.Data;
            PlayWord.Word = "jjj";
            Response playWordResponse = client.DoMethodAsync("PUT", "games/" + j1.Data.GameID.ToString(), PlayWord).Result;


            Assert.AreEqual(OK, playWordResponse.Status);
            Assert.AreEqual(-1, playWordResponse.Data);

        }

        [TestMethod]
        public void TestPlayAlreadyPlayedWord()
        {
            //Registering Player 1 as "Joe"
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);


            //Registering Player 2 as "Joe2"
            string P2Nickname = "Joe2";
            Response r2 = client.DoMethodAsync("POST", "users", P2Nickname).Result;
            Assert.AreEqual(OK, r2.Status);
            Assert.AreEqual(36, r2.Data.Length);

            dynamic P1 = new ExpandoObject();
            P1.UserToken = r1.Data;
            P1.TimeLimit = 60;
            //P1 and P2 are joining the game
            Response j1 = client.DoMethodAsync("POST", "games", P1).Result;
            Assert.AreEqual(OK, j1.Status);
            Assert.AreEqual("True", j1.Data.IsPending.ToString());


            dynamic P2 = new ExpandoObject();
            P2.UserToken = r2.Data;
            P2.TimeLimit = 60;
            Response j2 = client.DoMethodAsync("POST", "games", P2).Result;
            Assert.AreEqual(OK, j2.Status);
            Assert.AreEqual("False", j2.Data.IsPending.ToString());

            Assert.AreEqual(j1.Data.GameID.ToString(), j2.Data.GameID.ToString());


            dynamic PlayWord = new ExpandoObject();
            PlayWord.UserToken = r2.Data;
            PlayWord.Word = "jjj";
            Response playWordResponse = client.DoMethodAsync("PUT", "games/" + j1.Data.GameID.ToString(), PlayWord).Result;


            Assert.AreEqual(OK, playWordResponse.Status);
            Assert.AreEqual(-1, playWordResponse.Data);

            PlayWord = new ExpandoObject();
            PlayWord.UserToken = r2.Data;
            PlayWord.Word = "jjj";
            playWordResponse = client.DoMethodAsync("PUT", "games/" + j1.Data.GameID.ToString(), PlayWord).Result;


            Assert.AreEqual(OK, playWordResponse.Status);
            Assert.AreEqual(0, playWordResponse.Data);

        }


        [TestMethod]
        public void TestGameStatusPending()
        {
            //Registering Player 1 as "Joe"
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);


            //Registering Player 2 as "Joe2"
            string P2Nickname = "Joe2";
            Response r2 = client.DoMethodAsync("POST", "users", P2Nickname).Result;
            Assert.AreEqual(OK, r2.Status);
            Assert.AreEqual(36, r2.Data.Length);

            dynamic P1 = new ExpandoObject();
            P1.UserToken = r1.Data;
            P1.TimeLimit = 60;
            //P1 and P2 are joining the game
            Response j1 = client.DoMethodAsync("POST", "games", P1).Result;
            Assert.AreEqual(OK, j1.Status);
            Assert.AreEqual("True", j1.Data.IsPending.ToString());

            Response gameStatus = client.DoMethodAsync("GET", "games/" + j1.Data.GameID.ToString() + "/true").Result;
            Assert.AreEqual(OK, gameStatus.Status);
            Assert.AreEqual("pending", gameStatus.Data.GameState.ToString());


            dynamic P2 = new ExpandoObject();
            P2.UserToken = r2.Data;
            P2.TimeLimit = 60;
            Response j2 = client.DoMethodAsync("POST", "games", P2).Result;
        }

        [TestMethod]
        public void TestCancelPendingGame()
        {
            //Registering Player 1 as "Joe"
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);

            dynamic P1 = new ExpandoObject();
            P1.UserToken = r1.Data;
            P1.TimeLimit = 60;
            //P1 and P2 are joining the game
            Response j1 = client.DoMethodAsync("POST", "games", P1).Result;
            Assert.AreEqual(OK, j1.Status);
            Assert.AreEqual("True", j1.Data.IsPending.ToString());

            Response cancel = client.DoMethodAsync("PUT", "games", P1.UserToken).Result;
            Assert.AreEqual(NoContent, cancel.Status);
        }


        [TestMethod]
        public void TestJoinGameNotRegistered()
        {

            dynamic request = new ExpandoObject();
            request.UserToken = "suckas";
            request.TimeLimit = 20;
            Response response = client.DoMethodAsync("POST", "games", request).Result;
            Assert.AreEqual(Forbidden, response.Status);
        }


        [TestMethod]
        public void TestJoinGameInvalidTimeLimit()
        {
            // Register the user
            string P1Nickname = "Joe";
            Response r1 = client.DoMethodAsync("POST", "users", P1Nickname).Result;
            Assert.AreEqual(OK, r1.Status);
            Assert.AreEqual(36, r1.Data.Length);



            dynamic request = new ExpandoObject();
            request.UserToken = r1.Data;
            request.TimeLimit = -1;
            Response response = client.DoMethodAsync("POST", "games", request).Result;
            Assert.AreEqual(Forbidden, response.Status);
        }




    }
}

