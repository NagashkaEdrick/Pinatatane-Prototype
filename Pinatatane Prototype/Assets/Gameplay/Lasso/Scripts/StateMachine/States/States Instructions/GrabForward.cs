using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class GrabForward : State<LassoController>
    {
        [SerializeField] float coefForce = 150f;

        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);

            Vector3 dir = element.Lasso.CurrenObjectGrabbed.Transform.position - element.PinataController.Pawn.PawnTransform.position;

            element.PinataController.Pinata.Rigidbody.AddForce(dir * coefForce * element.Lasso.LassoData.grabForwardForce);

            element.hasGrabbed = true;
        }

        public override void OnCurrent(LassoController element)
        {
            base.OnCurrent(element);

            Debug.Log("Grab forward");
        }
    }
}