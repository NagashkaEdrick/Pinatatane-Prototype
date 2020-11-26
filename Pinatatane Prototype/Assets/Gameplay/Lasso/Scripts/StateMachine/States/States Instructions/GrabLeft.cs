using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class GrabLeft : State<LassoController>
    {
        Transform attacker;
        Transform target;
        Rigidbody targetBody;

        float distance;

        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);

            this.attacker = element.PinataController.Pawn.PawnTransform;
            this.target = element.Lasso.CurrenObjectGrabbed.Transform;
            targetBody = element.Lasso.CurrenObjectGrabbed.Rigidbody;

            distance = Vector3.Distance(target.position, attacker.position);
        }
        public override void OnCurrent(LassoController element)
        {
            base.OnCurrent(element);

            target.forward = -attacker.forward;
            Vector3 to = attacker.position + attacker.forward.normalized * distance;
            to.y = target.position.y;
            targetBody.MovePosition(to);

            if (element.debugMode)
                Debug.DrawLine(attacker.position, attacker.position + attacker.forward.normalized * distance, Color.magenta);
        }

        public override void OnExit(LassoController element)
        {
            targetBody.AddForce(-attacker.right * element.Lasso.LassoData.grabRotatingForce * 500);

            element.hasGrabbed = true;
        }
    }
}