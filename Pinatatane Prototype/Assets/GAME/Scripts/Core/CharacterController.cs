using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pinatatane;

namespace Pinatatane
{
    /* CHARACTER CONTROLLER:
     * Gere les deplacement du joueur et ses mouvements de camera
     */
    public class CharacterController : MonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public FloatValue speed; // Vitesse de deplacement du joueur
        [BoxGroup("Tweaking")]
        public FloatValue RotationSpeed; // Vitesse de rotation de la camera
        [BoxGroup("Tweaking")]
        public IntervalFloatValue rotationAcceleration; // Acceleration de la camera

        [BoxGroup("Fix")]
        public Transform _camera; // Reference sur la camera
        [BoxGroup("Fix")]
        public Transform cameraTarget; // Reference sur la cible de la camera
        [BoxGroup("Fix")]
        public Rigidbody rigidBody;

        float rightJoyX;
        AnimatorBehaviour animatorBehaviour;

        CharacterController cc => PlayerManager.Instance.localPlayer.characterController;

        private void Start()
        {
            animatorBehaviour = GetComponent<AnimatorBehaviour>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            PlayerMovement();
            PlayerRotation();
        }

        // Gere les mouvement du joueur en fonction du joystick gauche
        void PlayerMovement() {
            float horizontal = InputManagerQ.Instance.GetAxis("Horizontal");
            float vertical = InputManagerQ.Instance.GetAxis("Vertical");
            animatorBehaviour.animator.SetFloat("horizontal", horizontal);
            animatorBehaviour.animator.SetFloat("vertical", vertical);
            rigidBody.velocity = new Vector3(horizontal, rigidBody.velocity.y, vertical) * speed.value * Time.deltaTime;
        }


        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation() {
            cameraTarget.rotation = smoothRotation(cameraTarget.rotation, rotationAcceleration.value);
            cc.transform.rotation = smoothRotation(transform.rotation, rotationAcceleration.value);
            cc.rigidBody.velocity = Quaternion.Euler(0, rightJoyX, 0) * cc.rigidBody.velocity;
        }

        // Realise une rotation par acceleration
        Quaternion smoothRotation(Quaternion startRotationVector, float smoothSpeed) {
            rightJoyX += InputManagerQ.Instance.GetAxis("RotationX") * RotationSpeed.value;
            rightJoyX %= 360;
            Quaternion endRotationVector = Quaternion.Euler(0, rightJoyX, 0);
            return Quaternion.Slerp(startRotationVector, endRotationVector, smoothSpeed);
        }
    }
}
