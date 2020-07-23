﻿using System;
using System.Collections.Generic;
using System.Linq;
using GW2EIParser.EIData;
using GW2EIParser.Parser;
using GW2EIParser.Parser.ParsedData;
using GW2EIParser.Parser.ParsedData.CombatEvents;
using static GW2EIParser.Parser.ParseEnum.TrashIDS;

namespace GW2EIParser.Logic
{
    public class River : RaidLogic
    {
        public River(int triggerID) : base(triggerID)
        {
            MechanicList.AddRange(new List<Mechanic>
            {
                new HitOnPlayerMechanic(48272, "Bombshell", new MechanicPlotlySetting("circle","rgb(255,125,0)"),"Bomb Hit", "Hit by Hollowed Bomber Exlosion", "Hit by Bomb", 0 ),
                new HitOnPlayerMechanic(47258, "Timed Bomb", new MechanicPlotlySetting("square","rgb(255,125,0)"),"Stun Bomb", "Stunned by Mini Bomb", "Stun Bomb", 0, (de, log) => !de.To.HasBuff(log, 1122, de.Time)),
            }
            );
            GenericFallBackMethod = FallBackMethod.None;
            Extension = "river";
            Icon = "https://wiki.guildwars2.com/images/thumb/7/7b/Gold_River_of_Souls_Trophy.jpg/220px-Gold_River_of_Souls_Trophy.jpg";
        }

        protected override CombatReplayMap GetCombatMapInternal(ParsedLog log)
        {
            return new CombatReplayMap("https://i.imgur.com/YBtiFnH.png",
                            (4145, 1603),
                            (-12201, -4866, 7742, 2851),
                            (-21504, -12288, 24576, 12288),
                            (19072, 15484, 20992, 16508));
        }

        protected override List<ParseEnum.TrashIDS> GetTrashMobsIDS()
        {
            return new List<ParseEnum.TrashIDS>
            {
                Enervator,
                HollowedBomber,
                RiverOfSouls,
                SpiritHorde1,
                SpiritHorde2,
                SpiritHorde3
            };
        }

        public override void CheckSuccess(CombatData combatData, AgentData agentData, FightData fightData, HashSet<AgentItem> playerAgents)
        {
            base.CheckSuccess(combatData, agentData, fightData, playerAgents);
            if (!fightData.Success)
            {
                NPC desmina = Targets.Find(x => x.ID == (int)ParseEnum.TargetIDS.Desmina);
                if (desmina == null)
                {
                    throw new InvalidOperationException("Desmina not found");
                }
                ExitCombatEvent ooc = combatData.GetExitCombatEvents(desmina.AgentItem).LastOrDefault();
                if (ooc != null)
                {
                    long time = 0;
                    foreach (NPC mob in TrashMobs.Where(x => x.ID == (int)SpiritHorde3))
                    {
                        DespawnEvent dspwnHorde = combatData.GetDespawnEvents(mob.AgentItem).LastOrDefault();
                        if (dspwnHorde != null)
                        {
                            time = Math.Max(dspwnHorde.Time, time);
                        }
                    }
                    DespawnEvent dspwn = combatData.GetDespawnEvents(desmina.AgentItem).LastOrDefault();
                    if (time != 0 && dspwn == null && time <= desmina.LastAware)
                    {
                        fightData.SetSuccess(true, time);
                    }
                }
            }
        }

        public override void EIEvtcParse(FightData fightData, AgentData agentData, List<CombatItem> combatData, List<Player> playerList)
        {
            // The walls spawn at the start of the encounter, we fix it by overriding their first aware to the first velocity change event
            List<AgentItem> riverOfSouls = agentData.GetNPCsByID((int)RiverOfSouls);
            bool sortCombatList = false;
            foreach (AgentItem riverOfSoul in riverOfSouls)
            {
                CombatItem firstMovement = combatData.FirstOrDefault(x => x.IsStateChange == ParseEnum.StateChange.Velocity && x.SrcAgent == riverOfSoul.Agent && x.DstAgent != 0);
                if (firstMovement != null)
                {
                    // update start
                    riverOfSoul.OverrideAwareTimes(firstMovement.Time - GeneralHelper.ServerDelayConstant, riverOfSoul.LastAware);
                    foreach (CombatItem c in combatData)
                    {
                        if (c.SrcAgent == riverOfSoul.Agent && (c.IsStateChange == ParseEnum.StateChange.Position || c.IsStateChange == ParseEnum.StateChange.Rotation) && c.Time <= riverOfSoul.FirstAware)
                        {
                            sortCombatList = true;
                            c.OverrideTime(riverOfSoul.FirstAware);
                        }
                    }
                }
            }
            // make sure the list is still sorted by time after overrides
            if (sortCombatList)
            {
                combatData.Sort((x, y) => x.Time.CompareTo(y.Time));
            }
            ComputeFightTargets(agentData, combatData);
        }

        public override void ComputePlayerCombatReplayActors(Player p, ParsedLog log, CombatReplay replay)
        {
            // TODO bombs dual following circle actor (one growing, other static) + dual static circle actor (one growing with min radius the final radius of the previous, other static). Missing buff id
        }

        public override void ComputeNPCCombatReplayActors(NPC target, ParsedLog log, CombatReplay replay)
        {
            NPC desmina = Targets.Find(x => x.ID == (int)ParseEnum.TargetIDS.Desmina);
            if (desmina == null)
            {
                throw new InvalidOperationException("Desmina not found");
            }
            int start = (int)replay.TimeOffsets.start;
            int end = (int)replay.TimeOffsets.end;
            switch (target.ID)
            {
                case (int)HollowedBomber:
                    var bomberman = target.GetCastLogs(log, 0, log.FightData.FightEnd).Where(x => x.SkillId == 48272).ToList();
                    foreach (AbstractCastEvent bomb in bomberman)
                    {
                        int startCast = (int)bomb.Time;
                        int endCast = startCast + bomb.ActualDuration;
                        int expectedEnd = Math.Max(startCast + bomb.ExpectedDuration, endCast);
                        replay.Decorations.Add(new CircleDecoration(true, 0, 480, (startCast, endCast), "rgba(180,250,0,0.3)", new AgentConnector(target)));
                        replay.Decorations.Add(new CircleDecoration(true, expectedEnd, 480, (startCast, endCast), "rgba(180,250,0,0.3)", new AgentConnector(target)));
                    }
                    break;
                case (int)RiverOfSouls:
                    if (replay.Rotations.Count > 0)
                    {
                        replay.Decorations.Add(new FacingRectangleDecoration((start, end), new AgentConnector(target), replay.PolledRotations, 160, 390, "rgba(255,100,0,0.5)"));
                    }
                    break;
                case (int)Enervator:
                // TODO Line actor between desmina and enervator. Missing skillID
                case (int)SpiritHorde1:
                case (int)SpiritHorde2:
                case (int)SpiritHorde3:
                    break;
                default:
                    break;
            }

        }

        public override string GetLogicName(ParsedLog log)
        {
            return "River of Souls";
        }
    }
}
