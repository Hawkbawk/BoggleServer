using System;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class WordAndScore : IEquatable<WordAndScore>
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Word;
        /// <summary>
        /// 
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int Score;

        public bool Equals(WordAndScore other)
        {
            return other.Word.Equals(Word);
        }
    }
}