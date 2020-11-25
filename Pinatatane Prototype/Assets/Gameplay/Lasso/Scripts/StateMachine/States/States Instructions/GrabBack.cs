using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class GrabBack : State<LassoController>
    {
        [SerializeField] float coefForce = 150f;

        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);

            Vector3 dir = element.PinataController.Pawn.PawnTransform.position - element.Lasso.CurrenObjectGrabbed.Transform.position;
            
            element.Lasso.CurrenObjectGrabbed.Rigidbody.AddForce(dir *  coefForce * element.Lasso.LassoData.grabBackForce);

            element.hasGrabbed = true;
        }

        public override void OnCurrent(LassoController element)
        {
            base.OnCurrent(element);

            Debug.Log("Grab back");
        }
    }
}