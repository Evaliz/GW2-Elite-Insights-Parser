﻿using System.Collections.Generic;
using GW2EIParser.Parser.ParsedData;

namespace GW2EIParser.EIData
{
    public abstract class GenericDecorationSerializable
    {
        public string Type { get; protected set; }
        public long Start { get; }
        public long End { get; }

        protected GenericDecorationSerializable(ParsedLog log, GenericDecoration decoration, CombatReplayMap map)
        {
            Start = decoration.Lifespan.start;
            End = decoration.Lifespan.end;
        }
    }
}
