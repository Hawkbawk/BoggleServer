using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember]
        public string UserToken;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Nickname;
        /// <summary>
        /// 
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