﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class Dash : MonoBehaviour 
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;

        [BoxGroup("Fix")]
        [SerializeField]
        Rigidbody rigidBody;
        [BoxGroup("Fix")]
        [SerializeField]
        CharacterController movement;

        private Coroutine dashCor = null;

        public void DashAction() {
            if (dashCor == null) {
                dashCor = StartCoroutine(StartDash());
            }
        }

        IEnumerator StartDash() {
            data.movementSpeed *= data.dashSpeed;
            if (rigidBody.velocity == Vector3.zero) {
                movement.enabled = false;
                rigidBody.velocity = transform.forward * data.movementSpeed * Time.deltaTime;
            }
            yield return new WaitForSeconds(0.1f);
            movement.enabled = true;
            data.movementSpeed /= data.dashSpeed;
            yield return new WaitForSeconds(1f);
            dashCor = null;
        }
    }
}
