using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// Check si le joueur ne vise pas/plus ou n'appuie pas/plus sur la touche grab
    /// </summary>
    public class AimAndRightTriggerUpCondition : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            if (!element.PinataController.PhotonView.IsMine)
                return false;

            return !InputManager.Instance.aimButton.IsTrigger || !InputManager.Instance.grabButton.IsTrigger;
        }
    }
}