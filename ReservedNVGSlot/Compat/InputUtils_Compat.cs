using ReservedNVGSlot.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace ReservedNVGSlot.Compat
{
    public static class InputUtils_Compat
    {
        internal static bool Enabled => BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rune580.LethalCompanyInputUtils");
        internal static InputActionAsset Asset => IngameKeybinds.GetAsset();
        public static InputAction ActivateNVGAction => IngameKeybinds.Instance.ActivateNVGAction;

        internal static void Init()
        {
            if (Enabled && IngameKeybinds.Instance == null)
            {
                IngameKeybinds.Instance = new();
            }
        }
    }
}
