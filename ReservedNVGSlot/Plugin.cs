using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MoreShipUpgrades.Input;
using ReservedItemSlotCore;
using ReservedNVGSlot.Compat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReservedNVGSlot
{
    [BepInPlugin(Metadata.GUID, Metadata.NAME, Metadata.VERSION)]
    [BepInDependency("FlipMods.ReservedItemSlotCore", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rune580.LethalCompanyInputUtils", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.malco.lethalcompany.moreshipupgrades", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance;
        readonly Harmony harmony = new(Metadata.GUID);
        internal static readonly ManualLogSource mls = BepInEx.Logging.Logger.CreateLogSource(Metadata.NAME);
        public static ReservedItemInfo NVGInfo = new("Night Vision Goggles", 666, false, false, false, false);

        private void Awake()
        {
            Instance = this;

            IEnumerable<Type> types;
            try
            {
                types = Assembly.GetExecutingAssembly().GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null);
            }

            if (InputUtils_Compat.Enabled)
            {
                InputUtils_Compat.Init();
            }

            harmony.PatchAll();
        }
    }
}