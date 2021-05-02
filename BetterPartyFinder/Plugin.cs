﻿using Dalamud.Plugin;
using XivCommon;

namespace BetterPartyFinder {
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Plugin : IDalamudPlugin {
        public string Name => "Better Party Finder";

        internal DalamudPluginInterface Interface { get; private set; } = null!;
        internal Configuration Config { get; private set; } = null!;
        private Filter Filter { get; set; } = null!;
        internal PluginUi Ui { get; private set; } = null!;
        private Commands Commands { get; set; } = null!;
        internal XivCommonBase Common { get; private set; } = null!;
        private JoinHandler JoinHandler { get; set; } = null!;

        public void Initialize(DalamudPluginInterface pluginInterface) {
            this.Interface = pluginInterface;

            this.Config = Configuration.Load(this) ?? new Configuration();
            this.Config.Initialise(this);

            this.Common = new XivCommonBase(this.Interface, Hooks.PartyFinder);
            this.Filter = new Filter(this);
            this.JoinHandler = new JoinHandler(this);
            this.Ui = new PluginUi(this);
            this.Commands = new Commands(this);

            // start task to determine maximum item level (based on max chestpiece)
            Util.CalculateMaxItemLevel(this.Interface.Data);
        }

        public void Dispose() {
            this.Commands.Dispose();
            this.Ui.Dispose();
            this.JoinHandler.Dispose();
            this.Filter.Dispose();
            this.Common.Dispose();
        }
    }
}
