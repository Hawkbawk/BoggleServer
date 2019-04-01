using System;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Game
    {
        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember]
        public string GameID;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string GameState;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Board;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int TimeLimit;

        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int TimeLeft;

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember]
        public TimeSpan TimeStarted;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public User Player1;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public User Player2;
    }
}