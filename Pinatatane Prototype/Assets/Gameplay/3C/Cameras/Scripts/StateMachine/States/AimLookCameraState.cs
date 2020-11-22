using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimLookCameraState : State<CameraManager>
    {
        Coroutine transitionCoroutine;
        [SerializeField] PinataData pinataData;

        public override void OnEnter(CameraManager element)
        {
            base.OnEnter(element);

            transitionCoroutine = StartCoroutine(Transition(element));
        }

        IEnumerator Transition(CameraManager element)
        {
            yield return new WaitForSeconds(pinataData.aimTransitionTime);
            element.GetCameraHandler("AimLook").CopyAnglesValues(element.GetCameraHandler("FreeLook"));
            element.TransitionTo("AimLook", 1f);

            yield break;
        }

        public override void OnExit(CameraManager element)
        {
            StopCoroutine(transitionCoroutine);
            base.OnExit(element);
        }
    }
}