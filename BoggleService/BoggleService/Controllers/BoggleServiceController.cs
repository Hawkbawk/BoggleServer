using Boggle;
using BoggleService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Web.Http;

namespace BoggleService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BoggleServiceController : ApiController
    {
        private static Dictionary<string, User> Users = new Dictionary<string, User>();
        private static Dictionary<string, Game> Games = new Dictionary<string, Game>();
        private static Game PendingGame = new Game();
        private static int CurrentGameNum = 0;
        private static readonly object sync = new object();

        // POST BoggleService/users
        /// <summary>
        /// Create a new user.
        ///
        /// If Nickname is null, or when trimmed is empty or longer than 50 characters, responds with
        /// status 403 (Forbidden). Otherwise, creates a new user with a unique user token and the
        /// trimmed Nickname.The returned user token should be used to identify the user in
        /// subsequent requests. Responds with status 200 (Ok).
        /// </summary>
        /// <param name="nickname">The name of the user to be registered</param>
        [Route("BoggleService/users")]
        public string PostMakeUser([FromBody]string nickname)
        {
            if (nickname == null || nickname.Trim().Length == 0 || nickname.Trim().Length > 50)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            else if (nickname.Trim()[0] == '@')
            {
                Thread.Sleep(10_000);
            }
            else if (nickname.Trim()[0] == '#')
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            string currentUserToken;
            lock (sync)
            {
                currentUserToken = Guid.NewGuid().ToString();

                User currentUser = new User()
                {
                    UserToken = currentUserToken,
                    Nickname = nickname,

                };
                Users.Add(currentUserToken, currentUser);
            }

            return currentUserToken;


        }

        // POST api/values
        /// <summary> Join a game.
        ///
        /// If UserToken is invalid, TimeLimit is less than 5, or TimeLimit is greater than 120, responds with status 403
        /// (Forbidden). Otherwise, if UserToken is already a player in the pending game, responds
        /// with status 409 (Conflict). Otherwise, if there are no players in the pending game, adds
        /// UserToken as the first player of the pending game, and the TimeLimit as the pending
        /// game's requested time limit. Returns an object as illustrated below containing the
        /// pending game's game ID.Responds with status 200 (Ok) Otherwise, adds UserToken as the
        /// second player.The pending game becomes active and a new pending game with no players is
        /// created.The active game's time limit is the integer average of the time limits requested
        /// by the two players. Returns an object as illustrated below containing the new active
        /// game's game ID(which should be the same as the old pending game's game ID). Responds with
        /// status 200 (Ok).
        ///
        /// </summary> <param name="join">A request that contains the user token and the user's desired time limit</param>
        [Route("BoggleService/games")]
        public JoinGameResponse PostJoinGame([FromBody]JoinGameRequest join)
        {
            User currentUser;
            if (join.TimeLimit < 5 || join.TimeLimit > 120)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            lock (sync)
            {
                if (!Users.TryGetValue(join.UserToken, out currentUser))
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }


                if (PendingGame.Player1 == null)
                {
                    PendingGame.Player1 = new User()
                    {
                        UserToken = currentUser.UserToken,
                        Nickname = currentUser.Nickname,
                        DesiredTimeLimit = join.TimeLimit
                    };
                    JoinGameResponse response = new JoinGameResponse()
                    {
                        GameID = "G" + CurrentGameNum,
                        IsPending = true
                    };
                    PendingGame.GameID = response.GameID;
                    return response;

                }
                else
                {
                    // Put the current user into the pending game as player 2.
                    PendingGame.Player2 = new User()
                    {
                        UserToken = currentUser.UserToken,
                        Nickname = currentUser.Nickname,
                        DesiredTimeLimit = join.TimeLimit
                    };
                    // Construct the game as it's now active.
                    PendingGame.GameState = "active";
                    PendingGame.Board = new BoggleBoard().ToString();
                    PendingGame.TimeLimit = (PendingGame.Player1.DesiredTimeLimit + PendingGame.Player2.DesiredTimeLimit) / 2;
                    PendingGame.TimeLeft = PendingGame.TimeLimit;
                    PendingGame.TimeStarted = DateTime.Now.TimeOfDay;

                    // Add the pending game and then replace the PendingGame with an empty object.
                    Games.Add("G" + CurrentGameNum++, PendingGame);
                    PendingGame = new Game();

                    JoinGameResponse response = new JoinGameResponse()
                    {
                        GameID = "G" + CurrentGameNum,
                        IsPending = false
                    };

                    return response;
                }
            }

        }

        /// <summary>
        /// Cancel a pending request to join a game.
        ///
        /// If UserToken is invalid or is not a player in the pending game, responds with status 403
        /// (Forbidden). Otherwise, removes UserToken from the pending game and responds with status
        /// 204 (NoContent).
        /// </summary>
        /// <param name="UserToken">The unique identifying token for the player to be removed from the pending game</param>
        [Route("BoggleService/games")]
        public void PutCancelJoin([FromBody]string UserToken)
        {
            lock (sync)
            {
                if (!Users.TryGetValue(UserToken, out User currentUser))
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
                else if (!PendingGame.Player1.UserToken.Equals(UserToken))
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }

                PendingGame.Player1 = new User();
                return;
            }

        }

        /// <summary>
        /// Play a word in a game. If Word is null or empty or longer than 30 characters when
        /// trimmed, or if gameID or UserToken is invalid, or if UserToken is not a player in the
        /// game identified by gameID, responds with response code 403 (Forbidden). Otherwise, if the
        /// game state is anything other than "active", responds with response code 409 (Conflict)
        /// Otherwise, records the trimmed Word as being played by UserToken in the game identified
        /// by gameID.Returns the score for Word in the context of the game(e.g. if Word has been
        /// played before the score is zero). Responds with status 200 (OK). Note: The word is not
        /// case sensitive.
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="value"></param>
        [Route("BoggleService/games/{gameID}")]
        public void PutPlayWord([FromUri] string gameID, [FromBody]string value)
        {
        }

        /// <summary>
        /// Get game status information. If gameID is invalid, responds with status 403 (Forbidden).
        /// Otherwise, returns information about the game named by gameID as illustrated below.Note
        /// that the information returned depends on whether brief is true or false as well as on the
        /// state of the game. Responds with status code 200 (OK). Note: The Board and Words are not
        /// case sensitive.
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="brief"></param>
        /// <returns></returns>
        [Route("BoggleService/games/{gameID}/{brief}")]
        public Game GetGameStatus( string gameID, bool brief)
        {
            Game currentGame;
            Game response;
            lock (sync)
            {
                if (PendingGame.GameID.Equals(gameID))
                {
                    response = new Game()
                    {
                        GameState = "pending"
                    };
                }
                else if (!Games.TryGetValue(gameID, out currentGame))
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
                // If the game is pending, they will always get the same response.
                // If the game is active and they want a brief response, do the following.
                else if (currentGame.GameState.Equals("active") && brief)
                {
                    // Copy over the state of the game.
                    response = new Game()
                    {
                        GameState = "active",
                        TimeLeft = ComputeTimeLeft(currentGame),
                        Player1 = currentGame.Player1,
                        Player2 = currentGame.Player2
                    };


                    // Set all of the player info except for their score to null, so that it doesn't get serialized
                    response.Player1.UserToken = null;
                    response.Player1.Nickname = null;
                    response.Player1.WordsPlayed = null;

                    response.Player2.UserToken = null;
                    response.Player2.Nickname = null;
                    response.Player2.WordsPlayed = null;

                    // Reset all of the values that we used that we don't want serialized, but only if the game is completed.
                    if (response.TimeLeft < 0)
                    {
                        response.GameState = "completed";
                        response.TimeLeft = 0;
                        response.TimeStarted = default(TimeSpan);
                    }
                }
                // If the game is completed and they want a brief status, do the following.
                else if (currentGame.GameState.Equals("completed") && brief)
                {
                    // Copy over the state of the game.
                    response = new Game
                    {
                        GameState = "completed",
                        Player1 = currentGame.Player1,
                        Player2 = currentGame.Player2

                    };

                    // Change all of the data in the response so when serialized it matches the brief response for a completed game.
                    response.Player1.UserToken = null;
                    response.Player1.Nickname = null;
                    response.Player1.WordsPlayed = null;

                    response.Player2.UserToken = null;
                    response.Player2.Nickname = null;
                    response.Player2.WordsPlayed = null;
                }
                // If the game is active and they want a complete response, do the following.
                else if (currentGame.GameState.Equals("active"))
                {
                    response = new Game();
                }
                else
                {
                    response = new Game();
                }








            return response;
            }
        }

        private int ComputeTimeLeft(Game g)
        {
            return (int)(g.TimeStarted.TotalSeconds + g.TimeLimit)
                      - (int)(g.TimeStarted.TotalSeconds + DateTime.Now.TimeOfDay.TotalSeconds);
        }
    }

}