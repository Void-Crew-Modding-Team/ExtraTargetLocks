using Photon.Realtime;
using VoidManager.ModMessages;

namespace ExtraTargetLocks
{
    internal class UpdateCurrentMTLMessage : ModMessage
    {
        internal static UpdateCurrentMTLMessage Instance;

        public UpdateCurrentMTLMessage()
        {
            Instance = this;
        }

        internal void SendToPlayer(Player player)
        {
            Send(MyPluginInfo.PLUGIN_GUID, Instance.GetIdentifier(), player, new object[] { BepinPlugin.Bindings.CachedMaxTargetLocks }, true);
        }

        internal void SendToOthers()
        {
            Send(MyPluginInfo.PLUGIN_GUID, Instance.GetIdentifier(), Photon.Realtime.ReceiverGroup.Others, new object[] { BepinPlugin.Bindings.CachedMaxTargetLocks }, true);
        }

        public override void Handle(object[] arguments, Photon.Realtime.Player sender)
        {
            if (sender.IsMasterClient)
            {
                int CMTL = (int)arguments[0];
                BepinPlugin.Bindings.CachedMaxTargetLocks = CMTL;

                BepinPlugin.Log.LogInfo("Updating Cached Max Target Locks to " + CMTL);

                if (Patches.CurrentUsedTacticalLock != null)
                {
                    Patches.CurrentUsedTacticalLock.LockLimit = CMTL;
                }
            }
        }
    }
}
