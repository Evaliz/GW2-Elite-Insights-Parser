﻿using System.Collections.Generic;
using System.Linq;
using GW2EIParser.EIData;

namespace GW2EIParser.Builders.JsonModels
{
    /// <summary>
    /// Class corresponding to a death recap
    /// </summary>
    public class JsonDeathRecap
    {
        /// <summary>
        /// Elementary death recap item
        /// </summary>
        public class JsonDeathRecapDamageItem
        {
            /// <summary>
            /// Id of the skill
            /// </summary>
            /// <seealso cref="JsonLog.SkillMap"/>
            /// <seealso cref="JsonLog.BuffMap"/>
            public long Id { get; }
            /// <summary>
            /// True if the damage was indirect
            /// </summary>
            public bool IndirectDamage { get; }
            /// <summary>
            /// Source of the damage
            /// </summary>
            public string Src { get; }
            /// <summary>
            /// Damage done
            /// </summary>
            public int Damage { get; }
            /// <summary>
            /// Time value
            /// </summary>
            public int Time { get; }

            public JsonDeathRecapDamageItem(DeathRecap.DeathRecapDamageItem item)
            {
                Id = item.ID;
                IndirectDamage = item.IndirectDamage;
                Src = item.Src;
                Damage = item.Damage;
                Time = item.Time;
            }
        }

        /// <summary>
        /// Time of death
        /// </summary>
        public long DeathTime { get; }
        /// <summary>
        /// List of damaging events to put into downstate
        /// </summary>
        public List<JsonDeathRecapDamageItem> ToDown { get; }
        /// <summary>
        /// List of damaging events to put into deadstate
        /// </summary>
        public List<JsonDeathRecapDamageItem> ToKill { get; }

        public JsonDeathRecap(DeathRecap recap)
        {
            DeathTime = recap.DeathTime;
            ToDown = recap.ToDown?.Select(x => new JsonDeathRecapDamageItem(x)).ToList();
            ToKill = recap.ToKill?.Select(x => new JsonDeathRecapDamageItem(x)).ToList();
        }

    }
}
