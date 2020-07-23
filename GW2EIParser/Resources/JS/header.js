﻿/*jshint esversion: 6 */

var compileHeader = function () {
    Vue.component("encounter-component", {
        props: [],
        template: `${tmplEncounter}`,
        methods: {
            getGradient: function (percent) {
                var template = 'linear-gradient(to right, $left$, $middle$, $right$)';
                var greenPercent = "green " + (100 - percent) + "%";
                var redPercent = "red " + (percent) + "%";
                var middle = percent + "%";
                template = template.replace('$right$', greenPercent);
                template = template.replace('$left$', redPercent);
                template = template.replace('$middle$', middle);
                return template;
            }
        },
        computed: {
            encounter: function () {
                var targets = [];
                for (var i = 0; i < logData.phases[0].targets.length; i++) {
                    var targetData = logData.targets[logData.phases[0].targets[i]];
                    targets.push(targetData);
                }

                var encounter = {
                    name: logData.fightName,
                    icon: logData.fightIcon,
                    duration: logData.encounterDuration,
                    targets: targets
                };
                return encounter;
            },
            resultStatus: function () {
                return logData.success ? {text:'击杀', class: ["text-success"]} : { text: '失败', class: ["text-warning"] };
            }
        }
    });

    Vue.component("phase-component", {
        props: ["phases"],
        template: `${tmplPhase}`,
        methods: {
            select: function (phase) {
                for (var i = 0; i < this.phases.length; i++) {
                    this.phases[i].active = false;
                }
                phase.active = true;
            },
            getPhaseData: function(id) {
                return logData.phases[id];
            }
        }
    });

    Vue.component("target-component", {
        props: ["targets", "phaseindex"],
        template: `${tmplTargets}`,
        computed: {
            phase: function() {
                return logData.phases[this.phaseindex];
            }
        },
        methods: {
            show: function (index) {
                return this.phase.targets.indexOf(index) !== -1;
            },
            getTargetData: function(id) {
                return logData.targets[id];
            }
        }
    });

    Vue.component("player-component", {
        props: ["playerindex", "players"],
        template: `${tmplPlayers}`,
        methods: {
            getIcon: function (path) {
                return urls[path];
            },
            select: function (id) {
                var oldStatus = this.players[id].active;
                for (var i = 0; i < this.players.length; i++) {
                    this.players[i].active = false;
                }
                this.players[id].active = !oldStatus;
            }
        },
        computed: {
            scoreExpl: function () {
                return "<span style='text-align:left;display: block;'> bilibili_吟游神。</span>"
            },
            groups: function () {
                var aux = [];
                var i = 0;
                for (i = 0; i < logData.players.length; i++) {
                    var playerData = logData.players[i];
                    if (playerData.isConjure) {
                        continue;
                    }
                    if (!aux[playerData.group]) {
                        aux[playerData.group] = [];
                    }
                    aux[playerData.group].push(playerData);
                }

                var noUndefinedGroups = [];
                for (i = 0; i < aux.length; i++) {
                    if (aux[i]) {
                        noUndefinedGroups.push(aux[i]);
                    }
                }
                return noUndefinedGroups;
            }
        }
    });
};