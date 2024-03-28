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
    public class BepinPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = Logger;
            Bindings.MaxTargetLocks = Config.Bind("General", "CurrentMaxTargetLocks", 12, new ConfigDescription("The currently active number of max target locks. The host's value is automatically sent to clients and can be changed during runtime.", new AcceptableValueRange<int>(4, 26)));
            //Bindings.AbsoluteMaxTargetLocks = Config.Bind("General", "AbsoluteMaxTargetLocks", 12, new ConfigDescription("The absolute maximum, which cannot be changed during gameplay. All clients must have the same value assigned. Has a minor impact on network performance.", new AcceptableValueRange<int>(4, 26)));
            Bindings.CachedCurrentMaxTargetLocks = Bindings.MaxTargetLocks.Value;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        internal class Bindings
        {
            internal static ConfigEntry<int> MaxTargetLocks;
            //internal static ConfigEntry<int> AbsoluteMaxTargetLocks;
            internal const int AbsoluteMaxTargetLocks = 26;

            internal static int CachedCurrentMaxTargetLocks;
        }
    }
}