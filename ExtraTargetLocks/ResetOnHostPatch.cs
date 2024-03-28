using HarmonyLib;

namespace ExtraTargetLocks
{
    [HarmonyPatch(typeof(GameSessionManager), "HostGameSession")]
    internal class ResetOnHostPatch
    {
        [HarmonyPrefix]
        static void Patch()
        {
            BepinPlugin.Bindings.CachedMaxTargetLocks = BepinPlugin.Bindings.MaxTargetLocks.Value;
        }
    }
}
