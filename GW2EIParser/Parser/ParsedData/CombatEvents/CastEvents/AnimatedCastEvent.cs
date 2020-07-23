﻿using System;

namespace GW2EIParser.Parser.ParsedData.CombatEvents
{
    public class AnimatedCastEvent : AbstractCastEvent
    {
        private readonly int _scaledActualDuration;
        //private readonly int _effectHappenedDuration;


        private static double _upperLimit = Math.Log(4.0/3.0);
        private static double _lowerLimit = Math.Log(0.5);
        private static double _diffLimit = _upperLimit - _lowerLimit;

        private AnimatedCastEvent(CombatItem startItem, AgentData agentData, SkillData skillData) : base(startItem, agentData, skillData)
        {
            ExpectedDuration = startItem.BuffDmg > 0 ? startItem.BuffDmg : startItem.Value;
            if (startItem.IsActivation == ParseEnum.Activation.Quickness)
            {
                Acceleration = 1;
            }
            //_effectHappenedDuration = startItem.Value;
        }

        public AnimatedCastEvent(CombatItem startItem, CombatItem endItem, AgentData agentData, SkillData skillData) : this(startItem, agentData, skillData)
        {
            ActualDuration = endItem.Value;
            _scaledActualDuration = endItem.BuffDmg;
            if (Skill.ID == SkillItem.DodgeId)
            {
                ExpectedDuration = 750;
                ActualDuration = 750;
            }
            double nonScaledToScaledRatio = 1.0;
            if (_scaledActualDuration > 0)
            {
                nonScaledToScaledRatio = (double)_scaledActualDuration / ActualDuration;
                Acceleration = GeneralHelper.Clamp(2.0 * ((Math.Log(nonScaledToScaledRatio) - _lowerLimit) / _diffLimit) - 1.0, -1.0, 1.0);
            }
            switch (endItem.IsActivation)
            {
                case ParseEnum.Activation.CancelCancel:
                    Status = AnimationStatus.Iterrupted;
                    SavedDuration = -ActualDuration;
                    break;
                case ParseEnum.Activation.Reset:
                    Status = AnimationStatus.Full;
                    break;
                case ParseEnum.Activation.CancelFire:
                    int nonScaledExpectedDuration = (int)Math.Round(ExpectedDuration / nonScaledToScaledRatio);
                    SavedDuration = Math.Max(nonScaledExpectedDuration - ActualDuration, 0);
                    Status = AnimationStatus.Reduced;
                    break;
            }
        }

        public AnimatedCastEvent(CombatItem startItem, AgentData agentData, SkillData skillData, long logEnd) : this(startItem, agentData, skillData)
        {
            if (Skill.ID == SkillItem.DodgeId)
            {
                ExpectedDuration = 750;
            }
            ActualDuration = ExpectedDuration;
            if (ActualDuration + Time > logEnd)
            {
                ActualDuration = (int)(logEnd - Time);
            }
        }


        public AnimatedCastEvent(long time, SkillItem skill, int duration, AgentItem caster) : base(time, skill, caster)
        {
            ActualDuration = duration;
            ExpectedDuration = duration;
        }
    }
}
