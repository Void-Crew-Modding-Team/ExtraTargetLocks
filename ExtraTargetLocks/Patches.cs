using CG.Ship.TargetLocking;
using Gameplay.TacticalTargeting;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using static VoidManager.Utilities.HarmonyHelpers;

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
                __instance.LockLimit = BepinPlugin.Bindings.CachedCurrentMaxTargetLocks;
            }
        }


        static IEnumerable<CodeInstruction> Patch26(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction[] instructionsArray = instructions.ToArray();
            bool found = false;
            for (int i = instructionsArray.Length - 1; i >= 0; i--)
            {
                if (instructionsArray[i].opcode == OpCodes.Ldc_I4_7)
                {
                    instructionsArray[i].opcode = OpCodes.Ldc_I4;
                    instructionsArray[i].operand = BepinPlugin.Bindings.AbsoluteMaxTargetLocks.Value;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                BepinPlugin.Log.LogError("Patch26() failed to find and patch target sequence.");
            }
            return instructionsArray;
        }

        [HarmonyPatch(typeof(TargetLockComponent), MethodType.Constructor)]
        internal class TLCConstructorPatch
        {
            //static FieldInfo TargetLocksFI = AccessTools.Field(typeof(TargetLock), "TargetLocks");
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                bool found = false;
                foreach (CodeInstruction CI in instructions)
                {
                    if (CI.opcode == OpCodes.Ldc_I4_7)
                    {
                        CI.opcode = OpCodes.Ldc_I4;
                        CI.operand = 26;
                        break;
                    }
                }
                if (!found)
                {
                    BepinPlugin.Log.LogError("TargetLockComponent Constructor Transpiler failed to find and patch target sequence.");
                }
                return instructions;
            }
        }

        [HarmonyPatch(typeof(TargetLockComponent), "Awake")]
        internal class TLCAwakePatch
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                return Patch26(instructions);
            }
        }

        [HarmonyPatch(typeof(TargetLockComponent), "OnPhotonSerializeView")]
        internal class TLCOnPhotonSerializeViewPatch
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                return Patch26(instructions);
            }
        }

        [HarmonyPatch(typeof(TargetLockComponent), "TryGetTargetsInView")]
        internal class TLCTryGetTargetsInViewPatch
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                return Patch26(instructions);
            }
        }

        [HarmonyPatch(typeof(TargetLockComponent), "GetFirstValidMissileTarget")]
        internal class TLCGetFirstValidMissileTargetPatch
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                return Patch26(Patch26(instructions));
            }
        }

        [HarmonyPatch(typeof(TargetLockComponent), "IncrementFocusIndex")]
        internal class TLCIncrementFocusIndexPatch
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                return Patch26(Patch26(instructions));
            }
        }
        [HarmonyPatch(typeof(TargetLockComponent), "TryClearAll")]
        internal class TLCTryClearAllPatch
        {
            [HarmonyTranspiler]
            static IEnumerable<CodeInstruction> Patch(IEnumerable<CodeInstruction> instructions)
            {
                return Patch26(instructions);
            }
        }
    }
}
