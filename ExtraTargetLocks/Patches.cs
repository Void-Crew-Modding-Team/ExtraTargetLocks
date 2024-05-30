using Gameplay.TacticalTargeting;
using HarmonyLib;

namespace ExtraTargetLocks
{
    class Patches
    {
        internal static TacticalLock CurrentUsedTacticalLock;

        [HarmonyPatch(typeof(TacticalLock), "AbilityStarted")]
        internal class TacticalLockPatch
        {
            [HarmonyPostfix]
            static void Patch(TacticalLock __instance)
            {
                CurrentUsedTacticalLock = __instance;
                __instance.LockLimit = BepinPlugin.Bindings.MaxTargetLocks.Value;
            }
        }
    }
}
