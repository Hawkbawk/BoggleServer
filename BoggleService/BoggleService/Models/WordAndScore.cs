using System;
using System.Runtime.Serialization;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class WordAndScore : IComparable
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

        public int CompareTo(object obj)
        {
            WordAndScore cmp = obj as WordAndScore;
            return Word.CompareTo(cmp.Word);
            
        }
    }
}