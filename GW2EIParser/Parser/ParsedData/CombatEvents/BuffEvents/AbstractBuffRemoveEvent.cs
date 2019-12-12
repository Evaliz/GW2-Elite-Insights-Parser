﻿namespace GW2EIParser.Parser.ParsedData.CombatEvents
{
    public abstract class AbstractBuffRemoveEvent : AbstractBuffEvent
    {
        public int RemovedDuration { get; }

        public AbstractBuffRemoveEvent(CombatItem evtcItem, AgentData agentData, SkillData skillData) : base(evtcItem, skillData)
        {
            RemovedDuration = evtcItem.Value;
            By = agentData.GetAgent(evtcItem.DstAgent);
            if (By.Master != null)
            {
                ByMinion = By;
                By = By.Master;
            }
            To = agentData.GetAgent(evtcItem.SrcAgent);
        }

        public AbstractBuffRemoveEvent(AgentItem by, AgentItem to, long time, int removedDuration, SkillItem buffSkill) : base(buffSkill, time)
        {
            RemovedDuration = removedDuration;
            By = by;
            if (By.Master != null)
            {
                ByMinion = By;
                By = By.Master;
            }
            To = to;
        }

        public override void TryFindSrc(ParsedLog log)
        {
        }
    }
}
