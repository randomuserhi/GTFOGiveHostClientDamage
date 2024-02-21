using HarmonyLib;

namespace ClientDamage.Patches {
    [HarmonyPatch]
    internal class Sentry {
        public static bool anySentryShot => sentryShot || shotgunSentryShot;

        public static bool sentryShot = false;

        [HarmonyPatch(typeof(SentryGunInstance_Firing_Bullets), nameof(SentryGunInstance_Firing_Bullets.FireBullet))]
        [HarmonyPrefix]
        private static void Prefix_SentryGunFiringBullet(bool doDamage, bool targetIsTagged) {
            if (!doDamage) return;
            sentryShot = true;
        }
        [HarmonyPatch(typeof(SentryGunInstance_Firing_Bullets), nameof(SentryGunInstance_Firing_Bullets.FireBullet))]
        [HarmonyPostfix]
        private static void Postfix_SentryGunFiringBullet() {
            sentryShot = false;
        }

        // Special case for shotgun sentry
        public static bool shotgunSentryShot = false;
        [HarmonyPatch(typeof(SentryGunInstance_Firing_Bullets), nameof(SentryGunInstance_Firing_Bullets.UpdateFireShotgunSemi))]
        [HarmonyPrefix]
        private static void Prefix_ShotgunSentryFiring(SentryGunInstance_Firing_Bullets __instance, bool isMaster, bool targetIsTagged) {
            if (!isMaster) return;
            if (!(Clock.Time > __instance.m_fireBulletTimer)) return;

            shotgunSentryShot = true;
        }
        [HarmonyPatch(typeof(SentryGunInstance_Firing_Bullets), nameof(SentryGunInstance_Firing_Bullets.UpdateFireShotgunSemi))]
        [HarmonyPostfix]
        private static void Postfix_ShotgunSentryFiring() {
            shotgunSentryShot = false;
        }
    }
}
