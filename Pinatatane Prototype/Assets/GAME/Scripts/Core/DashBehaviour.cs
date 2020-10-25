using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class DashBehaviour : MonoBehaviour 
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;
        public float dashForce;

        [BoxGroup("Fix")]
        [SerializeField]
        SimplePhysic body;
        [BoxGroup("Fix")]
        [SerializeField]
        CharacterMovementBehaviour movement;

        private Coroutine dashCor = null;

        public void DashAction() {
            if (dashCor == null) {
                dashCor = StartCoroutine(StartDash());
            }
        }

        IEnumerator StartDash() {
            if (body.GetVelocity() == Vector3.zero) {
                body.AddForce(Vector3.forward * dashForce);
            } else {
                float horizontal = InputManagerQ.Instance.GetAxis("Horizontal");
                float vertical = InputManagerQ.Instance.GetAxis("Vertical");
                Vector3 movementVector = new Vector3(horizontal, 0, vertical) * dashForce;
                body.AddForce(movementVector);
            }
            yield return new WaitForSeconds(data.dashCooldown);
            dashCor = null;
        }
    }
}
