﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoggleService.Models
{
    public class Game
    {
        public string GameState;
        public string Board;
        public int TimeLimit;
        public User Player1;
        public User Player2;
    }
}