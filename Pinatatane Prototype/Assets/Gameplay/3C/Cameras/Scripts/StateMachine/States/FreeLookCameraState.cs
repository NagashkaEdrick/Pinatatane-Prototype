using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class FreeLookCameraState : State<CameraManager>
    {
        public override void OnEnter(CameraManager element)
        {
            base.OnEnter(element);
            element.TransitionTo("FreeLook", 1f);
        }
    }
}