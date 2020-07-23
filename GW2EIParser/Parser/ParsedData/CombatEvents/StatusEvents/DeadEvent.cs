﻿namespace GW2EIParser.Parser.ParsedData.CombatEvents
{
    public class DeadEvent : AbstractStatusEvent
    {
        public DeadEvent(CombatItem evtcItem, AgentData agentData) : base(evtcItem, agentData)
        {

        }

        public DeadEvent(AgentItem src, long time) : base(src, time)
        {

        }

    }
}
