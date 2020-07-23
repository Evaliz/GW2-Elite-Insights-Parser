﻿using GW2EIParser.EIData;

namespace GW2EIParser.Parser.ParsedData.CombatEvents
{
    public class MechanicEvent : AbstractTimeCombatEvent
    {
        private readonly Mechanic _mechanic;
        public AbstractSingleActor Actor { get; }
        public string ShortName => _mechanic.ShortName;
        public string Description => _mechanic.Description;

        public MechanicEvent(long time, Mechanic mech, AbstractSingleActor actor) : base(time)
        {
            Actor = actor;
            _mechanic = mech;
        }
    }
}
