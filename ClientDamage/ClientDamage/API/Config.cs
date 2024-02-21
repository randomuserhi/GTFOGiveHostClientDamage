using BepInEx;
using BepInEx.Configuration;

namespace ClientDamage {
    internal static partial class ConfigManager {
        static ConfigManager() {
            string text = Path.Combine(Paths.ConfigPath, $"{Module.Name}.cfg");
            ConfigFile configFile = new ConfigFile(text, true);

            debug = configFile.Bind(
                "Debug",
                "enable",
                false,
                "Enables debug messages when true.");

            delay = configFile.Bind(
                "Settings",
                "delay",
                0.05f,
                "Delay in seconds until a limb breaks. Clamped to a value between 0 and 0.2 to prevent abuse.");

            delayBulletDamage = configFile.Bind(
                "Settings",
                "delayBulletDamage",
                false,
                "Delay your bullet damage to mimic delay on client - Useful for mimicing delayed enemy reactions for burst weapons to make hitting full burst more consistent.\nWARNING: This literally adds ping to your shots which may make your play experience much worse. Use at your own risk.");

            includeShotgunSentry = configFile.Bind(
                "Settings",
                "includeShotgunSentry",
                false,
                "Set to true if you want shotgun sentry to be able to abuse the limb destruction delay.");
        }

        public static bool Debug {
            get { return debug.Value; }
            set { debug.Value = value; }
        }
        private static ConfigEntry<bool> debug;

        public static float Delay {
            get { return delay.Value; }
            set { delay.Value = value; }
        }
        private static ConfigEntry<float> delay;

        public static bool DelayBulletDamage {
            get { return delayBulletDamage.Value; }
            set { delayBulletDamage.Value = value; }
        }
        private static ConfigEntry<bool> delayBulletDamage;

        public static bool IncludeShotgunSentry {
            get { return includeShotgunSentry.Value; }
            set { includeShotgunSentry.Value = value; }
        }
        private static ConfigEntry<bool> includeShotgunSentry;
    }
}