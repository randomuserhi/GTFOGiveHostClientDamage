using BepInEx.Unity.IL2CPP.Utils.Collections;
using CharacterDestruction;
using HarmonyLib;
using SNetwork;
using UnityEngine;

namespace ClientDamage.Patches {
    [HarmonyPatch]
    internal static class LimbDestruction {
        private static System.Collections.IEnumerator DelayLimbDestruction(Dam_EnemyDamageLimb limb, sDestructionEventData data) {
            yield return new WaitForSeconds(Mathf.Clamp(ConfigManager.Delay, 0, 0.2f));
            callOriginal = true;
            limb.DestroyLimb(data);
            callOriginal = false;
        }

        private static bool callOriginal = false;
        [HarmonyPatch(typeof(Dam_EnemyDamageLimb), nameof(Dam_EnemyDamageLimb.DestroyLimb))]
        [HarmonyPrefix]
        private static bool Prefix_ReceiveDestroyLimb(Dam_EnemyDamageLimb __instance, sDestructionEventData destructionEventData) {
            if (!SNet.IsMaster || ConfigManager.Delay == 0) return true;

            if (!callOriginal) {
                __instance.StartCoroutine(DelayLimbDestruction(__instance, destructionEventData).WrapToIl2Cpp());
            }
            return callOriginal;
        }
    }

    [HarmonyPatch]
    internal static class CustomLimbDestruction {
        private static System.Collections.IEnumerator DelayLimbDestruction(Dam_EnemyDamageLimb_Custom limb, sDestructionEventData data) {
            yield return new WaitForSeconds(Mathf.Clamp(ConfigManager.Delay, 0, 0.2f));
            callOriginal = true;
            limb.DestroyLimb(data);
            callOriginal = false;
        }

        private static bool callOriginal = false;
        [HarmonyPatch(typeof(Dam_EnemyDamageLimb_Custom), nameof(Dam_EnemyDamageLimb_Custom.DestroyLimb))]
        [HarmonyPrefix]
        private static bool Prefix_ReceiveDestroyLimb(Dam_EnemyDamageLimb_Custom __instance, sDestructionEventData destructionEventData) {
            if (!SNet.IsMaster || ConfigManager.Delay == 0) return true;

            if (!callOriginal) {
                __instance.StartCoroutine(DelayLimbDestruction(__instance, destructionEventData).WrapToIl2Cpp());
            }
            return callOriginal;
        }
    }
}
