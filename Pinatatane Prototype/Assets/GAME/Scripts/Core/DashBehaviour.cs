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

        [BoxGroup("Fix")]
        [SerializeField]
        Rigidbody rigidBody;
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
            data.movementSpeed *= data.dashSpeed;
            if (rigidBody.velocity.x == 0 && rigidBody.velocity.z == 0) {
                movement.enabled = false;
                rigidBody.velocity = transform.forward * data.movementSpeed * Time.deltaTime;
            }
            yield return new WaitForSeconds(data.dashDuration);
            movement.enabled = true;
            data.movementSpeed /= data.dashSpeed;
            yield return new WaitForSeconds(data.dashCooldown);
            dashCor = null;
        }
    }
}
