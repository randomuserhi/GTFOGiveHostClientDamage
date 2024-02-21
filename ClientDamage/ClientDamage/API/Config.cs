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
    }
}