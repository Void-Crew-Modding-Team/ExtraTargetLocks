using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace ExtraTargetLocks
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency("VoidManager")]
    [BepInIncompatibility("ExtraTargetLocks")]
    public class BepinPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = Logger;
            Bindings.MaxTargetLocks = Config.Bind("General", "CurrentMaxTargetLocks", 7, new ConfigDescription("The currently active number of max target locks.", new AcceptableValueRange<int>(4, Bindings.AbsoluteMaxTargetLocks)));

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        internal class Bindings
        {
            internal static ConfigEntry<int> MaxTargetLocks;
            internal const int AbsoluteMaxTargetLocks = 7;
        }
    }
}