using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ShowSelectedBlueprint {
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin {
        public const string PluginGUID = "com.maxsch.Shapez2.ShowSelectedBlueprint";
        public const string PluginName = "Show Selected Blueprint";
        public const string PluginVersion = "0.0.1";

        public static ManualLogSource Log { get; private set; }

        private void Awake() {
            Log = Logger;

            Harmony harmony = new Harmony(PluginGUID);
            harmony.PatchAll();

            Logger.LogInfo(PluginName + " has been loaded!");
        }
    }
}
