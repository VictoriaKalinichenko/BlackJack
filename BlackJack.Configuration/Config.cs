﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Configuration
{
    public static class Config
    {
        public static readonly string ConnectionStringForEF = "name=DataBaseContext";
        public static readonly string ConnectionStringForDapper = "Data Source=DESKTOP-6FDL58C;Initial Catalog=BlackJack;Integrated Security=True;";
    }
}
