using Photon.Pun;
using VoidManager.CustomGUI;
using VoidManager.Utilities;
using static ExtraTargetLocks.BepinPlugin;
using static UnityEngine.GUILayout;

namespace ExtraTargetLocks
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name()
        {
            return "Extra Target Locks: " + BepinPlugin.Bindings.MaxTargetLocks;
        }

        string MTLString = string.Empty;

        public override void Draw()
        {
            if (Game.InGame && !PhotonNetwork.IsMasterClient)
            {
                Label("Must be host to configure. Current setting: " + BepinPlugin.Bindings.MaxTargetLocks);
                return;
            }

            Label("Extra Target Locks");
            MTLString = TextField(MTLString);

            if(int.TryParse(MTLString, out int value) && value >= 4 && value <= Bindings.AbsoluteMaxTargetLocks)
            {
                if(Button("Apply Setting - Current value: " + BepinPlugin.Bindings.MaxTargetLocks))
                {
                    BepinPlugin.Bindings.MaxTargetLocks.Value = value;
                    if(Patches.CurrentUsedTacticalLock != null)
                    {
                        Patches.CurrentUsedTacticalLock.LockLimit = value;
                    }
                }
            }
            else
            {
                Label("Cannot Change Setting - Must be a number between 4 and 7.");
            }
        }

        public override void OnOpen()
        {
            MTLString = BepinPlugin.Bindings.MaxTargetLocks.Value.ToString();
        }
    }
}
