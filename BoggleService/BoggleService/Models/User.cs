using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// Represents a user of the BoggleService.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// The unique user token that identifies this user. Is never serialized and is only used by
        /// the server.
        /// </summary>
        [IgnoreDataMember]
        public string UserToken;

        /// <summary>
        /// The nickname of this user, eg "Hawkbawk".
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Nickname;

        /// <summary>
        /// The user's desired time limit for a game. Is never serialized and is only used by the server.
        /// </summary>
        [IgnoreDataMember]
        public int DesiredTimeLimit;

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Score;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<WordAndScore> WordsPlayed;
    }
}