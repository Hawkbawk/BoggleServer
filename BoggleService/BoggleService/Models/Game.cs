using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 
        /// </summary>
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
        public User Player1;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public User Player2;
    }
}