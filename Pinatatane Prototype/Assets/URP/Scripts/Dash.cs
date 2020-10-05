using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay {
    public class Dash : MonoBehaviour 
    {
        [BoxGroup("Tweaking")]
        public FloatValue speed;

        [BoxGroup("Fix")]
        public Rigidbody rigidBody;
        [BoxGroup("Fix")]
        public CharacterController movement;
        [BoxGroup("Fix")]
        
        private bool dashEnded;

        // Start is called before the first frame update
        void Start() {
            dashEnded = true;
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (Input.GetAxisRaw("Left Trigger") > 0 && dashEnded) StartCoroutine(StartDash());
        }

        IEnumerator StartDash() {
            dashEnded = false;
            speed.value *= 10;
            if (rigidBody.velocity == Vector3.zero)
            {
                movement.enabled = false;
                rigidBody.velocity = transform.forward * speed.value * Time.deltaTime;
            }
            yield return new WaitForSeconds(0.1f);
            movement.enabled = true;
            speed.value /= 10;
            yield return new WaitForSeconds(1f);
            dashEnded = true;
        }
    }
}
