using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pinatatane;
using Photon.Pun;

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
        [SerializeField] private SimplePhysic body;

        float rightJoyX;
        bool movementActive = true;
        bool rotationActive = true;

        Coroutine movementCor = null;

        [SerializeField] Pinata myPinata;

        // Update is called once per frame
        void FixedUpdate()
        {
            if (movementActive && movementCor == null) movementCor = StartCoroutine(PlayerMovement());
            if (rotationActive) PlayerRotation();
        }

        // Gere les mouvement du joueur en fonction du joystick gauche
        IEnumerator PlayerMovement() {

            if (myPinata.player != PhotonNetwork.LocalPlayer)
                yield break;

            float horizontal = InputManagerQ.Instance.GetAxis("Horizontal");
            float vertical = InputManagerQ.Instance.GetAxis("Vertical");
            Vector3 movementVector = new Vector3(horizontal, 0, vertical)* data.movementSpeed;

            myPinata.characterMovementBehaviour.body.AddDirectForce(movementVector);

            myPinata.animatorBehaviour.SetFloat("vertical", vertical);
            myPinata.animatorBehaviour.SetFloat("horizontal", horizontal);

            yield return new WaitForEndOfFrame();

            movementCor = null;
        }
        
        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation() {
            cameraTarget.rotation = smoothRotation(cameraTarget.rotation, data.rotationAcceleration);
            myPinata.characterMovementBehaviour.transform.rotation = smoothRotation(myPinata.characterMovementBehaviour.transform.rotation, data.rotationAcceleration);
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

        public float getRotationAngle()
        {
            return rightJoyX;
        }
    }
}
