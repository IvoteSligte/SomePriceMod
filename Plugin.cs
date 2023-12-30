using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SomePriceMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin instance { get; private set; }
        
        internal static ManualLogSource log;

        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            instance = this;
            log = Logger;
            harmony.PatchAll();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}

namespace SomePriceMod.Patches
{
    [HarmonyPatch(typeof(Terminal))]
	internal class PricePatches
	{
        [HarmonyPatch("SetItemSales")]
		[HarmonyPostfix]
		private static void StorePrices(ref Item[] ___buyableItemsList)
		{
            ___buyableItemsList[6].creditsWorth = 30; // boombox
            ___buyableItemsList[7].creditsWorth = 30; // TZP
        }
    }
}
