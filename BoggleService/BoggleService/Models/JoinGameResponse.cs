using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoggleService.Models
{
    /// <summary>
    /// Represents a response to a join game request.
    /// </summary>
    public class JoinGameResponse
    {
        /// <summary>
        /// The ID of the game.
        /// </summary>
        public string GameID;
        /// <summary>
        /// The status of the game with the above game ID.
        /// </summary>
        public bool IsPending;
    }
}