using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoggleService.Models
{
    public class PutWordRequest
    {
        public string GameID;
        public string UserToken;
        public string Word;
    }
}