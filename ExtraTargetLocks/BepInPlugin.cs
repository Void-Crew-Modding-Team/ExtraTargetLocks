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
            Bindings.CurrentMaxTargetLocks = Config.Bind("General", "CurrentMaxTargetLocks", 12, Bindings.CurrentDescription);
            Bindings.AbsoluteMaxTargetLocks = Config.Bind("General", "AbsoluteMaxTargetLocks", 12, Bindings.AbsoluteDescription);
            Bindings.CachedCurrentMaxTargetLocks = Bindings.CurrentMaxTargetLocks.Value;

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        internal class Bindings
        {
            internal static ConfigDescription CurrentDescription = new ConfigDescription("The currently active number of max target locks.", new AcceptableValueRange<int>(4, 26));
            internal static ConfigDescription AbsoluteDescription = new ConfigDescription("The absolute maximum, which cannot be changed during gameplay. All clients must have the same value assigned. Has a minor impact on network performance.", new AcceptableValueRange<int>(4, 26));

            internal static ConfigEntry<int> CurrentMaxTargetLocks;
            internal static ConfigEntry<int> AbsoluteMaxTargetLocks;

            internal static int CachedCurrentMaxTargetLocks;
        }
    }
}