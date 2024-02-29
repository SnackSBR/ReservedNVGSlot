using LethalCompanyInputUtils.Api;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem;

namespace ReservedNVGSlot.Input
{
    internal class IngameKeybinds : LcInputActions
    {
        public static IngameKeybinds Instance = new();

        internal static InputActionAsset GetAsset()
        {
            return Instance.Asset;
        }

        [InputAction("<Keyboard>/q", Name = "[ReservedNVGSlot]\nToggle Night Vision Goggles")]
        public InputAction ActivateNVGAction { get; set; }
    }
}
