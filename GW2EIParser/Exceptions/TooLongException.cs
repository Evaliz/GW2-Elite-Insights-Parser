﻿using System;

namespace GW2EIParser.Exceptions
{
    public class TooLongException : Exception
    {
        public TooLongException() : base("Fight is took longer than 24h - may be a broken evtc")
        {
        }

    }
}
