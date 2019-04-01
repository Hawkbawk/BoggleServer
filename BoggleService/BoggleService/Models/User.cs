using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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