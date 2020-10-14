using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pinatatane;

namespace Pinatatane
{
    /* CHARACTER MOVEMENT BEHAVIOUR:
     * Gere les deplacement du joueur et ses mouvements de camera
     */
    public class CharacterMovementBehaviour : MonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;

        [BoxGroup("Fix")]
        public Transform _camera; // Reference sur la camera
        [BoxGroup("Fix")]
        public Transform cameraTarget; // Reference sur la cible de la camera
        [BoxGroup("Fix")]
        [SerializeField] public Rigidbody rigidBody;

        float rightJoyX;
        bool movementActive = true;
        bool rotationActive = true;

        CharacterMovementBehaviour cc => PlayerManager.Instance.LocalPlayer.characterMovementBehaviour;
        AnimatorBehaviour ab => PlayerManager.Instance.LocalPlayer.animatorBehaviour;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (movementActive) PlayerMovement();
            else cc.rigidBody.velocity = Vector3.zero;
            if (rotationActive) PlayerRotation();
        }

        // Gere les mouvement du joueur en fonction du joystick gauche
        void PlayerMovement() {
            float horizontal = InputManagerQ.Instance.GetAxis("Horizontal");
            float vertical = InputManagerQ.Instance.GetAxis("Vertical");
            cc.rigidBody.velocity = new Vector3(horizontal, -data.gravity, vertical).normalized * data.movementSpeed * Time.deltaTime;

            ab.SetFloat("vertical", vertical);
            ab.SetFloat("horizontal", horizontal);
        }
        
        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation() {
            cameraTarget.rotation = smoothRotation(cameraTarget.rotation, data.rotationAcceleration);
            cc.transform.rotation = smoothRotation(cc.transform.rotation, data.rotationAcceleration);
            cc.rigidBody.velocity = Quaternion.Euler(0, rightJoyX, 0) * cc.rigidBody.velocity;
        }

        // Realise une rotation par acceleration
        Quaternion smoothRotation(Quaternion startRotationVector, float smoothSpeed) {
            rightJoyX += InputManagerQ.Instance.GetAxis("RotationX") * data.rotationSpeed;
            rightJoyX %= 360;
            Quaternion endRotationVector = Quaternion.Euler(0, rightJoyX, 0);
            return Quaternion.Slerp(startRotationVector, endRotationVector, smoothSpeed);
        }

        public bool isMovementActive() {
            return movementActive;
        }

        public void setMovementActive(bool value) {
            movementActive = value;
        }

        public void setRotationActive(bool value)
        {
            rotationActive = value;
        }
    }
}
