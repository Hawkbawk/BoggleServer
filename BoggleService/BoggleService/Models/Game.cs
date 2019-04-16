using System;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// Represents the state of a BoggleGame.
    /// </summary>
    [DataContract]
    public class Game
    {
        /// <summary>
        /// The state of the game. Can be either "pending", "active", or "completed".
        /// </summary>
        [DataMember]
        public string GameState;

        /// <summary>
        /// The board of the Boggle game.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Board;

        /// <summary>
        /// The time limit for this Boggle game.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int TimeLimit;

        /// <summary>
        /// The amount of time left in the game.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int TimeLeft;

        /// <summary>
        /// The first player in the game.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public User Player1;
        /// <summary>
        /// The second player in the game.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public User Player2;
    }
}