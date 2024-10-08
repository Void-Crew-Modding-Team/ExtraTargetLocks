﻿using VoidManager;
using VoidManager.MPModChecks;

namespace ExtraTargetLocks
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public VoidManagerPlugin()
        {
            Events.Instance.HostVerifiedClient += HostVerifiedClient;
        }

        static void HostVerifiedClient(object source, Events.PlayerEventArgs Player)
        {
            //If statement not needed, This mod is marked as MPType.All and should be installed on the given client already.
            //if(MPModCheckManager.Instance.NetworkedPeerHasMod(Player.player, MyPluginInfo.PLUGIN_GUID))
                UpdateCurrentMTLMessage.Instance.SendToPlayer(Player.player);
        }

        public override MultiplayerType MPType => MultiplayerType.All;

        public override string Author => MyPluginInfo.PLUGIN_AUTHORS;

        public override string Description => MyPluginInfo.PLUGIN_DESCRIPTION;

        public override string ThunderstoreID => MyPluginInfo.PLUGIN_THUNDERSTORE_ID;
    }
}
