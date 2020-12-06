using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// Check si le joueur vise et appuie sur la touche grab
    /// </summary>
    public class AimAndRightTriggerDownCondition : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            if (!element.PinataController.Pinata.ImLocalPinata)
                return false;

            if (element.isConstructed)
                return false;

            return InputManager.Instance.aimButton.IsTrigger && InputManager.Instance.grabButton.IsTrigger;
        }
    }
}