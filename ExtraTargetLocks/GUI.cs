using Photon.Pun;
using VoidManager.CustomGUI;
using VoidManager.Utilities;
using static UnityEngine.GUILayout;

namespace ExtraTargetLocks
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name()
        {
            return $"{MyPluginInfo.USERS_PLUGIN_NAME}: " + BepinPlugin.Bindings.CachedMaxTargetLocks;
        }

        string MTLString = string.Empty;

        public override void Draw()
        {
            if (Game.InGame && !PhotonNetwork.IsMasterClient)
            {
                Label("Must be host to configure. Current setting: " + BepinPlugin.Bindings.CachedMaxTargetLocks);
                return;
            }

            Label("Extra Target Locks");
            MTLString = TextField(MTLString);

            if(int.TryParse(MTLString, out int value) && value >= 4 && value <= 26)
            {
                if(Button("Apply Setting - Current value: " + BepinPlugin.Bindings.CachedMaxTargetLocks))
                {
                    BepinPlugin.Bindings.CachedMaxTargetLocks = value;
                    BepinPlugin.Bindings.MaxTargetLocks.Value = value;
                    if(Patches.CurrentUsedTacticalLock != null)
                    {
                        Patches.CurrentUsedTacticalLock.LockLimit = value;
                    }
                    UpdateCurrentMTLMessage.Instance.SendToOthers();
                }
            }
            else
            {
                Label("Cannot Change Setting - Must be a number between 4 and 26.");
            }
        }

        public override void OnOpen()
        {
            MTLString = BepinPlugin.Bindings.MaxTargetLocks.Value.ToString();
        }
    }
}
