﻿/*jshint esversion: 6 */
const Trans_base = {
    Warrior: '战士',
    Berserker: '战士',
    Spellbreaker: '战士',
    Revenant: "魂武",
    Herald: "魂武",
    Renegade: "魂武",
    Guardian: "守护",
    Dragonhunter: "守护",
    Firebrand: "守护",
    Ranger: "游侠",
    Druid: "游侠",
    Soulbeast: "游侠",
    Engineer: "工程师",
    Scrapper: "工程师",
    Holosmith: "工程师",
    Thief: "盗贼",
    Daredevil: "盗贼",
    Deadeye: "盗贼",
    Mesmer: "幻术",
    Chronomancer: "幻术",
    Mirage: "幻术",
    Necromancer: "死灵",
    Reaper: "死灵",
    Scourge: "死灵",
    Elementalist: "元素",
    Tempest: "元素",
    Weaver: "元素"
};

const Trans_spec = {
    Warrior: '战士',
    Berserker: '狂战士',
    Spellbreaker: '破法',
    Revenant: "魂武",
    Herald: "预告",
    Renegade: "龙魂",
    Guardian: "守护",
    Dragonhunter: "猎龙",
    Firebrand: "燃火",
    Ranger: "游侠",
    Druid: "德鲁伊",
    Soulbeast: "魂兽",
    Engineer: "工程师",
    Scrapper: "机械师",
    Holosmith: "全息",
    Thief: "盗贼",
    Daredevil: "独行侠",
    Deadeye: "神枪",
    Mesmer: "幻术",
    Chronomancer: "时空术士",
    Mirage: "蜃楼",
    Necromancer: "死灵",
    Reaper: "夺魂",
    Scourge: "灾厄",
    Elementalist: "元素",
    Tempest: "暴风使",
    Weaver: "编织"
};

var compileDamageModifiers = function () {
    Vue.component("dmgmodifier-stats-component", {
        props: ['phaseindex', 'playerindex', 'activetargets',
        ],
        template: `${tmplDamageModifierStats}`,
        data: function () {
            return {
                mode: 1,
                displayMode: logData.dmgModifiersItem.length > 0 ? 0 : logData.dmgModifiersCommon.length > 0 ? 1 : 2
            };
        },
        computed: {
            phase: function () {
                return logData.phases[this.phaseindex];
            },
            commonModifiers: function () {
                var modifiers = [];
                for (var i = 0; i < logData.dmgModifiersCommon.length; i++) {
                    modifiers.push(logData.damageModMap['d' + logData.dmgModifiersCommon[i]]);
                }
                return modifiers;
            },
            itemModifiers: function () {
                var modifiers = [];
                for (var i = 0; i < logData.dmgModifiersItem.length; i++) {
                    modifiers.push(logData.damageModMap['d' + logData.dmgModifiersItem[i]]);
                }
                return modifiers;
            }
        }
    });

    Vue.component("dmgmodifier-persstats-component", {
        props: ['phaseindex', 'playerindex', 'activetargets', 'mode'
        ],
        template: `${tmplDamageModifierPersStats}`,
        data: function () {
            return {
                bases: [],
                basesCH: [],
                specmode: "Warrior",
                specToBase: specToBase
            };
        },
        computed: {
            phase: function () {
                return logData.phases[this.phaseindex];
            },
            orderedSpecs: function () {
                var res = [];
                var aux = new Set();
                for (var i = 0; i < specs.length; i++) {
                    var spec = specs[i];
                    var pBySpec = [];
                    for (var j = 0; j < logData.players.length; j++) {
                        if (logData.players[j].profession === spec && logData.phases[0].dmgModifiersPers[j].data.length > 0) {
                            pBySpec.push(j);
                        }
                    }
                    var tmp = "Warrior";
                    if (pBySpec.length) {
                        aux.add(specToBase[spec]);
                        res.push({
                            ids: pBySpec,
                            name: spec,
                            nameCH: Trans_spec[spec],
                        });
                    }
                }
                this.bases = [];
                var _this = this;
                aux.forEach(function (value, value2, set) {
                    _this.bases.push(value);
                    _this.basesCH.push(Trans_base[value]);
                });
                this.specmode = this.bases[0];
                return res;
            },
            personalModifiers: function () {
                var res = [];
                for (var i = 0; i < this.orderedSpecs.length; i++) {
                    var spec = this.orderedSpecs[i];
                    var data = [];
                    for (var j = 0; j < logData.dmgModifiersPers[spec.name].length; j++) {
                        data.push(logData.damageModMap['d' + logData.dmgModifiersPers[spec.name][j]]);
                    }
                    res.push(data);
                }
                return res;
            }
        }
    });

    Vue.component("dmgmodifier-table-component", {
        props: ['phaseindex', 'id', 'playerindex', 'playerindices', 'activetargets', 'modifiers', 'modifiersdata', 'mode', 'sum'
        ],
        mixins: [roundingComponent],
        template: `${tmplDamageModifierTable}`,
        data: function () {
            return {
                cache: new Map(),
                cacheTarget: new Map()
            };
        },
        computed: {
            phase: function () {
                return logData.phases[this.phaseindex];
            },
            indicesToUse: function () {
                var res = [];
                if (this.playerindices !== null) {
                    for (var i = 0; i < this.playerindices.length; i++) {
                        res.push(this.playerindices[i]);
                    }
                    return res;
                }
                for (var i = 0; i < logData.players.length; i++) {
                    res.push(i);
                }
                return res;
            },
            tableData: function () {
                if (this.cache.has(this.phaseindex)) {
                    return this.cache.get(this.phaseindex);
                }
                var rows = [];
                var sums = [];
                var groups = [];
                var total = {
                    name: "全部",
                    data: []
                };
                var j;
                for (var i = 0; i < this.indicesToUse.length; i++) {
                    var index = this.indicesToUse[i];
                    var player = logData.players[index];
                    if (player.isConjure) {
                        continue;
                    }
                    if (!groups[player.group]) {
                        groups[player.group] = {
                            name: "队伍" + player.group,
                            data: []
                        };
                    }
                    var dmgModifier = this.modifiersdata[index].data;
                    var data = [];
                    for (j = 0; j < this.modifiers.length; j++) {
                        data[j] = dmgModifier[j];
                        if (!groups[player.group].data[j]) {
                            groups[player.group].data[j] = [0, 0, 0, 0];
                        }
                        if (!total.data[j]) {
                            total.data[j] = [0, 0, 0, 0];
                        }
                        for (var k = 0; k < data[j].length; k++) {
                            groups[player.group].data[j][k] += data[j][k];
                            total.data[j][k] += data[j][k];
                        }
                    }
                    rows.push({
                        player: player,
                        data: data
                    });
                }
                for (var i = 0; i < groups.length; i++) {
                    if (groups[i]) {
                        sums.push(groups[i]);
                    }
                }
                sums.push(total);
                var res = {
                    rows: rows,
                    sums: sums
                };
                this.cache.set(this.phaseindex, res);
                return res;
            },
            tableDataTarget: function () {
                var cacheID = this.phaseindex + '-';
                cacheID += getTargetCacheID(this.activetargets);
                if (this.cacheTarget.has(cacheID)) {
                    return this.cacheTarget.get(cacheID);
                }
                var rows = [];
                var sums = [];
                var groups = [];
                var total = {
                    name: "全部",
                    data: []
                };
                var j;
                for (var i = 0; i < this.indicesToUse.length; i++) {
                    var index = this.indicesToUse[i];
                    var player = logData.players[index];
                    if (player.isConjure) {
                        continue;
                    }
                    if (!groups[player.group]) {
                        groups[player.group] = {
                            name: "队伍" + player.group,
                            data: []
                        };
                    }
                    var data = [];
                    for (j = 0; j < this.modifiers.length; j++) {
                        data[j] = [0, 0, 0, 0];
                        if (!groups[player.group].data[j]) {
                            groups[player.group].data[j] = [0, 0, 0, 0];
                        }
                        if (!total.data[j]) {
                            total.data[j] = [0, 0, 0, 0];
                        }
                    }
                    var dmgModifier = this.modifiersdata[index].dataTarget;
                    for (j = 0; j < this.activetargets.length; j++) {
                        var modifier = dmgModifier[this.activetargets[j]];
                        for (var k = 0; k < this.modifiers.length; k++) {
                            var targetData = modifier[k];
                            var curData = data[k];
                            for (var l = 0; l < targetData.length; l++) {
                                curData[l] += targetData[l];
                            }
                        }
                    }
                    for (j = 0; j < this.modifiers.length; j++) {
                        for (var k = 0; k < data[j].length; k++) {
                            groups[player.group].data[j][k] += data[j][k];
                            total.data[j][k] += data[j][k];
                        }
                    }
                    rows.push({
                        player: player,
                        data: data
                    });
                }
                for (var i = 0; i < groups.length; i++) {
                    if (groups[i]) {
                        sums.push(groups[i]);
                    }
                }
                sums.push(total);
                var res = {
                    rows: rows,
                    sums: sums
                };
                this.cacheTarget.set(cacheID, res);
                return res;
            }
        },
        methods: {
            getTooltip: function (item, mod) {
                if (item[2] === 0) {
                    return null;
                }
                var hits = "命中" + item[0] + " 次, " + "总计" + item[1] + " 次 ";
                var percent;
                if (mod.skillBased) {
                    percent = this.round3(1000.0 * item[1] / this.phase.duration) + " 每秒命中"; 
                } else {
                    percent = this.round3(100.0 * item[0] / item[1]) + "%" + " 命中率";
                }
                var gain;
                if (mod.nonMultiplier) {
                  gain = "造成伤害: ";         
                } else {               
                  gain = "纯粹伤害: ";
                }
                gain += this.round3(item[2]);
                return hits + "<br>" + percent + "<br>" + gain;   
            },
            getCellValue: function (item, mod) {
                if (item[2] === 0) {
                    return '-';
                }
                if (mod.nonMultiplier) {
                   return 'Tooltip';
                }
                var damageIncrease = this.round3(100 * (item[3] / (item[3] - item[2]) - 1.0));
                if (Math.abs(damageIncrease) < 1e-6 || isNaN(damageIncrease)) {
                    return "-";
                }
                return damageIncrease + '%';
            }
        },
        mounted() {
            initTable("#"+this.id, 1, "asc");
        },
        updated() {
            updateTable("#" + this.id);
        },
    });
};