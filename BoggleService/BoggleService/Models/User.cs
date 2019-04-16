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
        /// The nickname of this user, eg "Hawkbawk".
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Nickname;

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