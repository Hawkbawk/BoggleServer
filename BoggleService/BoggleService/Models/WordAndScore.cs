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
        [DataMember]
        public string Word;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Score;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(WordAndScore other)
        {
            return other.Word.Equals(Word);
        }
    }
}