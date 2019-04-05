using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoggleService.Models
{
    /// <summary>
    /// Represents a join game request.
    /// </summary>
    public class JoinGameRequest
    {
        /// <summary>
        /// The token of the user who want to join a game.
        /// </summary>
        public string UserToken;
        /// <summary>
        /// The user's desired time limit.
        /// </summary>
        public int TimeLimit;
    }
}