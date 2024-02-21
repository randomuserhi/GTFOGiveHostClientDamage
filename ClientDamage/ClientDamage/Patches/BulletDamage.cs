using Agents;
using API;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using Player;
using SNetwork;
using UnityEngine;

namespace ClientDamage.Patches {
    [HarmonyPatch]
    internal static class BulletDamage {
        private static System.Collections.IEnumerator DelayBulletDamage(Dam_EnemyDamageLimb limb, float dam, Agent sourceAgent, Vector3 position, Vector3 direction, Vector3 normal, bool allowDirectionalBonus, float staggerMulti, float precisionMulti, uint gearCategoryId) {
            yield return new WaitForSeconds(Mathf.Clamp(ConfigManager.Delay, 0, 0.2f));
            if (limb != null) {
                callOriginal = true;
                limb.BulletDamage(dam, sourceAgent, position, direction, normal, allowDirectionalBonus, staggerMulti, precisionMulti, gearCategoryId);
                callOriginal = false;
            }
        }

        private static bool callOriginal = false;
        [HarmonyPatch(typeof(Dam_EnemyDamageLimb), nameof(Dam_EnemyDamageLimb.BulletDamage))]
        [HarmonyPrefix]
        private static bool Prefix_ReceiveBulletDamage(Dam_EnemyDamageLimb __instance, float dam, Agent sourceAgent, Vector3 position, Vector3 direction, Vector3 normal, bool allowDirectionalBonus, float staggerMulti, float precisionMulti, uint gearCategoryId) {
            if (!SNet.IsMaster || Sentry.anySentryShot || ConfigManager.Delay == 0 || !ConfigManager.DelayBulletDamage) return true;

            if (sourceAgent.GlobalID != PlayerManager.GetLocalPlayerAgent().GlobalID) return true;

            if (!callOriginal) {
                APILogger.Debug("Delay bullet shot.");
                __instance.StartCoroutine(DelayBulletDamage(__instance, dam, sourceAgent, position, direction, normal, allowDirectionalBonus, staggerMulti, precisionMulti, gearCategoryId).WrapToIl2Cpp());
            }
            return callOriginal;
        }
    }
}
