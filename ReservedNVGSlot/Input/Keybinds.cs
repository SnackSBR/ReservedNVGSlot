using GameNetcodeStuff;
using HarmonyLib;
using MoreShipUpgrades.UpgradeComponents.TierUpgrades;
using ReservedItemSlotCore.Config;
using UnityEngine.InputSystem;
using UnityEngine;
using MoreShipUpgrades.Compat;
using MoreShipUpgrades.Managers;

namespace ReservedNVGSlot.Input
{
    [HarmonyPatch]
    internal static class Keybinds
    {
        public static InputActionAsset Asset;
        public static InputActionMap ActionMap;
        private static InputAction ActivateNVGAction;

        public static PlayerControllerB LocalPlayerController => StartOfRound.Instance?.localPlayerController;

        [HarmonyPatch(typeof(PreInitSceneScript), "Awake")]
        [HarmonyPrefix]
        public static void AddToKeybindMenu()
        {
            if (InputUtils_Compat.Enabled)
            {
                Asset = InputUtils_Compat.Asset;
                ActionMap = Asset.actionMaps[0];
                ActivateNVGAction = IngameKeybinds.Instance.ActivateNVGAction;
            }
            else
            {
                Asset = ScriptableObject.CreateInstance<InputActionAsset>();
                ActionMap = new InputActionMap("ReservedNVGSlot");
                InputActionSetupExtensions.AddActionMap(Asset, ActionMap);
                ActivateNVGAction = InputActionSetupExtensions.AddAction(ActionMap, "ReservedNVGSlot.ActivateNVGAction", binding: "<Keyboard>/q", interactions: "Press");
            }
        }

        [HarmonyPatch(typeof(StartOfRound), "OnEnable")]
        [HarmonyPostfix]
        public static void OnEnable()
        {
            Asset.Enable();
            ActivateNVGAction.performed += OnActivateNVGPerformed;
        }

        [HarmonyPatch(typeof(StartOfRound), "OnDisable")]
        [HarmonyPostfix]
        public static void OnDisable()
        {
            Asset.Disable();
            ActivateNVGAction.performed -= OnActivateNVGPerformed;
        }

        private static void OnActivateNVGPerformed(InputAction.CallbackContext context)
        {
            if(LocalPlayerController == null || !LocalPlayerController.isPlayerControlled || (LocalPlayerController.IsServer && !LocalPlayerController.isHostPlayerObject))
            {
                return;
            }

            if (NightVision.Instance && !NightVision.Instance.batteryExhaustion)
            {
                NightVision.Instance.Toggle();
            }
        }
    }
}
