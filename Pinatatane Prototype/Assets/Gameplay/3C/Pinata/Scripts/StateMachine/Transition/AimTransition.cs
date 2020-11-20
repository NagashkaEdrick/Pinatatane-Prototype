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

            DOTween.To(
            () => element.Pawn.PawnTransform.forward,
            x => element.Pawn.PawnTransform.forward = x,
            new Vector3(element.cameraTransform.forward.x, element.Pawn.PawnTransform.forward.y, element.cameraTransform.forward.z),
            element.Pinata.PinataData.aimTransitionTime
            ).SetEase(Ease.InOutSine);
        }

        //IEnumerator OnTransition(PinataController element)
        //{
        //    while (inTransition)
        //    {
        //        if (isNeedingFocus)
        //        {
        //            transform.forward = Vector3.RotateTowards(transform.forward, newForward.normalized, turningRotationSpeed * Time.deltaTime, 0.0f);
        //            if (Vector3.Distance(transform.forward, newForward) <= 0.05f)
        //            {
        //                // Changement d'un comportement de mouvement a l'autre
        //                aimingMovement.enabled = true;
        //                aimingRotation.enabled = true;
        //                aimingRotation.LinkRotationInput();
        //                isNeedingFocus = false;
        //                mode = 1;
        //            }
        //        }

        //        yield return null;
        //    }

        //    yield break;
        //}
    }
}