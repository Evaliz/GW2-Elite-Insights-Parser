﻿using System;
using System.Collections.Generic;
using GW2EIParser.EIData;
using GW2EIParser.Parser.ParsedData;

namespace GW2EIParser.Builders.HtmlModels
{

    public class PhaseDto
    {
        public string Name { get; set; }
        public long Duration { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public List<int> Targets { get; set; } = new List<int>();

        public List<List<object>> DpsStats { get; set; }
        public List<List<List<object>>> DpsStatsTargets { get; set; }
        public List<List<List<object>>> DmgStatsTargets { get; set; }
        public List<List<object>> DmgStats { get; set; }
        public List<List<object>> DefStats { get; set; }
        public List<List<object>> SupportStats { get; set; }
        // all
        public List<BuffData> BoonStats { get; set; }
        public List<BuffData> BoonGenSelfStats { get; set; }
        public List<BuffData> BoonGenGroupStats { get; set; }
        public List<BuffData> BoonGenOGroupStats { get; set; }
        public List<BuffData> BoonGenSquadStats { get; set; }

        public List<BuffData> OffBuffStats { get; set; }
        public List<BuffData> OffBuffGenSelfStats { get; set; }
        public List<BuffData> OffBuffGenGroupStats { get; set; }
        public List<BuffData> OffBuffGenOGroupStats { get; set; }
        public List<BuffData> OffBuffGenSquadStats { get; set; }

        public List<BuffData> DefBuffStats { get; set; }
        public List<BuffData> DefBuffGenSelfStats { get; set; }
        public List<BuffData> DefBuffGenGroupStats { get; set; }
        public List<BuffData> DefBuffGenOGroupStats { get; set; }
        public List<BuffData> DefBuffGenSquadStats { get; set; }

        public List<BuffData> PersBuffStats { get; set; }

        // active
        public List<BuffData> BoonActiveStats { get; set; }
        public List<BuffData> BoonGenActiveSelfStats { get; set; }
        public List<BuffData> BoonGenActiveGroupStats { get; set; }
        public List<BuffData> BoonGenActiveOGroupStats { get; set; }
        public List<BuffData> BoonGenActiveSquadStats { get; set; }

        public List<BuffData> OffBuffActiveStats { get; set; }
        public List<BuffData> OffBuffGenActiveSelfStats { get; set; }
        public List<BuffData> OffBuffGenActiveGroupStats { get; set; }
        public List<BuffData> OffBuffGenActiveOGroupStats { get; set; }
        public List<BuffData> OffBuffGenActiveSquadStats { get; set; }

        public List<BuffData> DefBuffActiveStats { get; set; }
        public List<BuffData> DefBuffGenActiveSelfStats { get; set; }
        public List<BuffData> DefBuffGenActiveGroupStats { get; set; }
        public List<BuffData> DefBuffGenActiveOGroupStats { get; set; }
        public List<BuffData> DefBuffGenActiveSquadStats { get; set; }

        public List<BuffData> PersBuffActiveStats { get; set; }

        public List<DamageModData> DmgModifiersCommon { get; set; }
        public List<DamageModData> DmgModifiersItem { get; set; }
        public List<DamageModData> DmgModifiersPers { get; set; }

        public List<List<BuffData>> TargetsCondiStats { get; set; }
        public List<BuffData> TargetsCondiTotals { get; set; }
        public List<BuffData> TargetsBoonTotals { get; set; }

        public List<List<int[]>> MechanicStats { get; set; }
        public List<List<int[]>> EnemyMechanicStats { get; set; }
        public List<long> PlayerActiveTimes { get; set; }

        public List<double> MarkupLines { get; set; }
        public List<AreaLabelDto> MarkupAreas { get; set; }
        public List<int> SubPhases { get; set; }

        public PhaseDto(PhaseData phaseData, List<PhaseData> phases, ParsedLog log)
        {
            Name = phaseData.Name;
            Duration = phaseData.DurationInMS;
            Start = phaseData.Start / 1000.0;
            End = phaseData.End / 1000.0;
            foreach (NPC target in phaseData.Targets)
            {
                Targets.Add(log.FightData.Logic.Targets.IndexOf(target));
            }
            PlayerActiveTimes = new List<long>();
            foreach (Player p in log.PlayerList)
            {
                PlayerActiveTimes.Add(phaseData.GetActorActiveDuration(p, log));
            }
            // add phase markup
            MarkupLines = new List<double>();
            MarkupAreas = new List<AreaLabelDto>();
            for (int j = 1; j < phases.Count; j++)
            {
                PhaseData curPhase = phases[j];
                if (curPhase.Start < phaseData.Start || curPhase.End > phaseData.End ||
                    (curPhase.Start == phaseData.Start && curPhase.End == phaseData.End))
                {
                    continue;
                }
                if (SubPhases == null)
                {
                    SubPhases = new List<int>();
                }
                SubPhases.Add(j);
                long start = curPhase.Start - phaseData.Start;
                long end = curPhase.End - phaseData.Start;
                if (curPhase.DrawStart)
                {
                    MarkupLines.Add(start / 1000.0);
                }

                if (curPhase.DrawEnd)
                {
                    MarkupLines.Add(end / 1000.0);
                }

                var phaseArea = new AreaLabelDto
                {
                    Start = start / 1000.0,
                    End = end / 1000.0,
                    Label = curPhase.Name,
                    Highlight = curPhase.DrawArea
                };
                MarkupAreas.Add(phaseArea);
            }
            if (MarkupAreas.Count == 0)
            {
                MarkupAreas = null;
            }

            if (MarkupLines.Count == 0)
            {
                MarkupLines = null;
            }
        }


        // helper methods

        public static List<object> GetDMGStatData(FinalGameplayStatsAll stats)
        {
            List<object> data = GetDMGTargetStatData(stats);
            data.AddRange(new List<object>
                {
                    // commons
                    stats.TimeWasted, // 9
                    stats.Wasted, // 10

                    stats.TimeSaved, // 11
                    stats.Saved, // 12

                    stats.SwapCount, // 13
                    Math.Round(stats.StackDist, 2), // 14
                    Math.Round(stats.DistToCom, 2) // 15
                });
            return data;
        }

        public static List<object> GetDMGTargetStatData(FinalGameplayStats stats)
        {
            var data = new List<object>
                {
                    stats.DirectDamageCount, // 0
                    stats.CritableDirectDamageCount, // 1
                    stats.CriticalCount, // 2
                    stats.CriticalDmg, // 3

                    stats.FlankingCount, // 4

                    stats.GlanceCount, // 5

                    stats.Missed,// 6
                    stats.Interrupts, // 7
                    stats.Invulned // 8
                };
            return data;
        }

        public static List<object> GetDPSStatData(FinalDPS dpsAll)
        {
            var data = new List<object>
                {
                    dpsAll.Damage,
                    dpsAll.PowerDamage,
                    dpsAll.CondiDamage,
                };
            return data;
        }

        public static List<object> GetSupportStatData(FinalPlayerSupport support)
        {
            var data = new List<object>()
                {
                    support.CondiCleanse,
                    support.CondiCleanseTime,
                    support.CondiCleanseSelf,
                    support.CondiCleanseTimeSelf,
                    support.BoonStrips,
                    support.BoonStripsTime,
                    support.Resurrects,
                    support.ResurrectTime
                };
            return data;
        }

        public static List<object> GetDefenseStatData(FinalDefensesAll defenses, PhaseData phase)
        {
            var data = new List<object>
                {
                    defenses.DamageTaken,
                    defenses.DamageBarrier,
                    defenses.BlockedCount,
                    defenses.InvulnedCount,
                    defenses.InterruptedCount,
                    defenses.EvadedCount,
                    defenses.DodgeCount
                };

            if (defenses.DownDuration > 0)
            {
                var downDuration = TimeSpan.FromMilliseconds(defenses.DownDuration);
                data.Add(defenses.DownCount);
                data.Add(downDuration.TotalSeconds + " seconds downed, " + Math.Round((downDuration.TotalMilliseconds / phase.DurationInMS) * 100, 1) + "% Downed");
            }
            else
            {
                data.Add(0);
                data.Add("0% downed");
            }

            if (defenses.DeadDuration > 0)
            {
                var deathDuration = TimeSpan.FromMilliseconds(defenses.DeadDuration);
                data.Add(defenses.DeadCount);
                data.Add(deathDuration.TotalSeconds + " seconds dead, " + (100.0 - Math.Round((deathDuration.TotalMilliseconds / phase.DurationInMS) * 100, 1)) + "% Alive");
            }
            else
            {
                data.Add(0);
                data.Add("100% Alive");
            }
            return data;
        }
    }
}
