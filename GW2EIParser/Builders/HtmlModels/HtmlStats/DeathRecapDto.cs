﻿using System.Collections.Generic;
using System.Linq;
using GW2EIParser.EIData;
using GW2EIParser.Parser.ParsedData;

namespace GW2EIParser.Builders.HtmlModels
{
    public class DeathRecapDto
    {
        public long Time { get; set; }
        public List<object[]> ToDown { get; set; } = null;
        public List<object[]> ToKill { get; set; } = null;

        private static List<object[]> BuildDeathRecapItemList(List<DeathRecap.DeathRecapDamageItem> list)
        {
            var data = new List<object[]>();
            foreach (DeathRecap.DeathRecapDamageItem item in list)
            {
                data.Add(new object[]
                {
                            item.Time,
                            item.ID,
                            item.Damage,
                            item.Src,
                            item.IndirectDamage
                });
            }
            return data;
        }

        public static List<DeathRecapDto> BuildDeathRecap(ParsedLog log, Player p)
        {
            var res = new List<DeathRecapDto>();
            List<DeathRecap> recaps = p.GetDeathRecaps(log);
            if (!recaps.Any())
            {
                return null;
            }
            foreach (DeathRecap deathRecap in recaps)
            {
                var recap = new DeathRecapDto()
                {
                    Time = deathRecap.DeathTime
                };
                res.Add(recap);
                if (deathRecap.ToKill != null)
                {
                    recap.ToKill = BuildDeathRecapItemList(deathRecap.ToKill);
                }
                if (deathRecap.ToDown != null)
                {
                    recap.ToDown = BuildDeathRecapItemList(deathRecap.ToDown);
                }

            }
            return res;
        }
    }
}
