﻿using System.Collections.Generic;
using System.Linq;
using GW2EIParser.EIData;
using GW2EIParser.Parser.ParsedData;

namespace GW2EIParser.Builders.JsonModels
{
    /// <summary>
    /// Class representing damage modifier data
    /// </summary>
    public class JsonDamageModifierData
    {
        /// <summary>
        /// Class corresponding to a buff based damage modifier
        /// </summary>
        public class JsonDamageModifierItem
        {
            /// <summary>
            /// Hits done under the buff
            /// </summary>
            public int HitCount { get; }
            /// <summary>
            /// Total hits
            /// </summary>
            public int TotalHitCount { get; }
            /// <summary>
            /// Gained damage \n
            /// If the corresponding <see cref="JsonLog.DamageModDesc.NonMultiplier"/> is true then this value correspond to the damage done while under the effect. One will have to deduce the gain manualy depending on your gear.
            /// </summary>
            public double DamageGain { get; }
            /// <summary>
            /// Total damage done
            /// </summary>
            public int TotalDamage { get; }

            public JsonDamageModifierItem(DamageModifierStat extraData)
            {
                HitCount = extraData.HitCount;
                TotalHitCount = extraData.TotalHitCount;
                DamageGain = extraData.DamageGain;
                TotalDamage = extraData.TotalDamage;
            }
        }
        /// <summary>
        /// ID of the damage modifier \
        /// </summary>
        /// <seealso cref="JsonLog.DamageModMap"/>
        public int Id { get; }
        /// <summary>
        /// Array of damage modifier data \n
        /// Length == # of phases
        /// </summary>
        /// <seealso cref="JsonDamageModifierItem"/>
        public List<JsonDamageModifierItem> DamageModifiers { get; }


        protected JsonDamageModifierData(int ID, List<JsonDamageModifierItem> data)
        {
            Id = ID;
            DamageModifiers = data;
        }


        public static List<JsonDamageModifierData> GetDamageModifiers(Dictionary<string, List<DamageModifierStat>> damageModDict, ParsedLog log, Dictionary<string, JsonLog.DamageModDesc> damageModDesc)
        {
            var dict = new Dictionary<int, List<JsonDamageModifierItem>>();
            foreach (string key in damageModDict.Keys)
            {
                int iKey = key.GetHashCode();
                string nKey = "d" + iKey;
                if (!damageModDesc.ContainsKey(nKey))
                {
                    damageModDesc[nKey] = new JsonLog.DamageModDesc(log.DamageModifiers.DamageModifiersByName[key]);
                }
                dict[iKey] = damageModDict[key].Select(x => new JsonDamageModifierItem(x)).ToList();
            }
            var res = new List<JsonDamageModifierData>();
            foreach (KeyValuePair<int, List<JsonDamageModifierItem>> pair in dict)
            {
                res.Add(new JsonDamageModifierData(pair.Key, pair.Value));
            }
            return res;
        }

        public static List<JsonDamageModifierData>[] GetDamageModifiersTarget(Player player, ParsedLog log, Dictionary<string, JsonLog.DamageModDesc> damageModDesc)
        {
            var res = new List<JsonDamageModifierData>[log.FightData.Logic.Targets.Count];
            for (int i = 0; i < log.FightData.Logic.Targets.Count; i++)
            {
                NPC tar = log.FightData.Logic.Targets[i];
                res[i] = GetDamageModifiers(player.GetDamageModifierStats(log, tar), log, damageModDesc);
            }
            return res;
        }
    }
}
