using System.Collections.Generic;

namespace BoggleService.Models
{
    public class User
    {
        public string UserToken;
        public string Nickname;
        public int Score;
        public List<WordAndScore> WordsPlayed;
    }
}