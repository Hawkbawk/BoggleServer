using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string UserToken;
        /// <summary>
        /// 
        /// </summary>
        public string Nickname;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int DesiredTimeLimit;
        /// <summary>
        /// 
        /// </summary>
        public int Score;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<WordAndScore> WordsPlayed;
    }
}