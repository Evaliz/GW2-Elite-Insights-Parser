/*jshint esversion: 6 */
var Layout = function (desc) {
    this.desc = desc;
    this.tabs = null;
};

Layout.prototype.addTab = function (tab) {
    if (this.tabs === null) {
        this.tabs = [];
    }
    this.tabs.push(tab);
};

var Tab = function (name, options) {
    this.name = name;
    options = options ? options : {};
    this.layout = null;
    this.desc = options.desc ? options.desc : null;
    this.active = options.active ? options.active : false;
    this.dataType =
        typeof options.dataType !== "undefined" ? options.dataType : -1;
};

var compileLayout = function () {
    // Compile
    Vue.component("general-layout-component", {
        name: "general-layout-component",
        template: `${tmplGeneralLayout}`,
        props: ["layout", "phaseindex"],
        methods: {
            select: function (tab, tabs) {
                for (var i = 0; i < tabs.length; i++) {
                    tabs[i].active = false;
                }
                tab.active = true;
            }
        },
        computed: {
            phase: function () {
                return logData.phases[this.phaseindex];
            },
            layoutName: function () {
                if (this.phaseindex < 0) {
                    return this.layout.desc;
                }
                return this.layout.desc ?
                    this.phase.name + " " + this.layout.desc :
                    this.phase.name;
            }
        }
    });
    //
    var layout = new Layout("总结");
    // general stats
    var stats = new Tab("总览", {
        active: true
    });
    var statsLayout = new Layout(null);
    statsLayout.addTab(
        new Tab("输出", {
            active: true,
            dataType: DataTypes.damageTable
        })
    );
    statsLayout.addTab(
        new Tab("受到技能影响", {
            dataType: DataTypes.gameplayTable
        })
    );
    statsLayout.addTab(
        new Tab("防御措施覆盖", {
            dataType: DataTypes.defTable
        })
    );
    statsLayout.addTab(
        new Tab("其他", {
            dataType: DataTypes.supTable
        })
    );
    stats.layout = statsLayout;
    layout.addTab(stats);
    // buffs
    var buffs = new Tab("有益法术");
    var buffLayout = new Layout(null);
    buffLayout.addTab(
        new Tab("增益", {
            active: true,
            dataType: DataTypes.boonTable
        })
    );
    if (logData.offBuffs.length > 0) {
        buffLayout.addTab(new Tab("进攻性法术", {
            dataType: DataTypes.offensiveBuffTable
        }));
    }
    if (logData.defBuffs.length > 0) {
        buffLayout.addTab(new Tab("防御性法术", {
            dataType: DataTypes.defensiveBuffTable
        }));
    }
    if (logData.persBuffs) {
        var hasPersBuffs = false;
        for (var prop in logData.persBuffs) {
            if (logData.persBuffs.hasOwnProperty(prop) && logData.persBuffs[prop].length > 0) {
                hasPersBuffs = true;
                break;
            }
        }
        if (hasPersBuffs) {
            buffLayout.addTab(new Tab("个人增益", {
                dataType: DataTypes.personalBuffTable
            }));
        }
    }
    buffs.layout = buffLayout;
    layout.addTab(buffs);
    // damage modifiers
    if (!logData.targetless) {
        var damageModifiers = new Tab("增伤途径", {
            dataType: DataTypes.dmgModifiersTable
        });
        layout.addTab(damageModifiers);
    }
    // mechanics
    if (logData.mechanicMap.length > 0 && !logData.noMechanics) {
        var mechanics = new Tab("机制", {
            dataType: DataTypes.mechanicTable
        });
        layout.addTab(mechanics);
    }
    // graphs
    var graphs = new Tab("图表", {
        dataType: DataTypes.dpsGraph
    });
    layout.addTab(graphs);
    // targets
    if (!logData.targetless && !logData.wvw) {
        var targets = new Tab("目标总览", {
            dataType: DataTypes.targetTab
        });
        layout.addTab(targets);
    }
    // player
    var player = new Tab("玩家总览", {
        dataType: DataTypes.playerTab
    });
    layout.addTab(player);
    return layout;
};