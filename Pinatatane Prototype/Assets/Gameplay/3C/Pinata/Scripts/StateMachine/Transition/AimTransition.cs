using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimTransition : State<PinataController>
    {
        public override void OnEnter(PinataController element)
        {
            base.OnEnter(element);

            element.TweenAllignPawnOnCameraForward(element.Pinata.PinataData.aimTransitionTime);
        }
    }
}