﻿using System;

namespace HotelSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            DBClient dbc = new DBClient();
            dbc.Start();

        }
    }
}
