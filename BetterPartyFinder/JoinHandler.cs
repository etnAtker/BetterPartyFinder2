﻿using System;
using Dalamud.Game.Internal.Gui.Structs;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace BetterPartyFinder {
    public class JoinHandler : IDisposable {
        private Plugin Plugin { get; }

        internal JoinHandler(Plugin plugin) {
            this.Plugin = plugin;

            this.Plugin.Common.Functions.PartyFinder.JoinParty += this.OnJoin;
        }

        public void Dispose() {
            this.Plugin.Common.Functions.PartyFinder.JoinParty -= this.OnJoin;
        }

        private void OnJoin(PartyFinderListing listing) {
            if (!this.Plugin.Config.ShowDescriptionOnJoin) {
                return;
            }

            SeString msg = "Party description: ";
            msg.Payloads.AddRange(listing.Description.Payloads);

            this.Plugin.Interface.Framework.Gui.Chat.PrintChat(new XivChatEntry {
                Name = "Better Party Finder",
                Type = XivChatType.SystemMessage,
                MessageBytes = msg.Encode(),
            });
        }
    }
}
