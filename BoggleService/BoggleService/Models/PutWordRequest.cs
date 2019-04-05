using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoggleService.Models
{

    /// <summary>
    /// Represents a request to play a word.
    /// </summary>
    public class PutWordRequest
    {
        /// <summary>
        /// The user who's playing the word.
        /// </summary>
        public string UserToken;

        /// <summary>
        /// The word being played.
        /// </summary>
        public string Word;
    }
}