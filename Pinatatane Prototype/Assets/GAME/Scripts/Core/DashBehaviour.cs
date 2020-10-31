using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QRTools.Inputs;

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

        [BoxGroup("Inputs", order: 2)]
        [SerializeField] QInputXBOXTouch dashButton = default;
        [BoxGroup("Inputs", order: 2)]
        [SerializeField] QInputAxis
            moveX = default,
            moveY = default;



        private Coroutine dashCor = null;

        private void Start()
        {
            dashButton.onDown.AddListener(DashAction);
        }

        public void DashAction() {
            if (dashCor == null) {
                dashCor = StartCoroutine(StartDash());
            }
        }

        IEnumerator StartDash() {
            if (body.GetVelocity() == Vector3.zero) {
                body.AddForce(transform.forward * dashForce);
            } else {
                Vector3 movementVector = new Vector3(moveX.JoystickValue, 0, moveY.JoystickValue) * dashForce;
                body.AddForce(transform.TransformVector(movementVector));
            }
            yield return new WaitForSeconds(data.dashCooldown);
            dashCor = null;
        }
    }
}
