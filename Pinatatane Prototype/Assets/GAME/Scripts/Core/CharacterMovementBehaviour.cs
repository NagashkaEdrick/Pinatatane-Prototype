using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pinatatane;
using Photon.Pun;
using QRTools.Inputs;

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

        [SerializeField] Pinata myPinata;

        [BoxGroup("Inputs", order: 2)]
        [SerializeField] QInputXBOXAxis horizontal, vertical, rotationX;

        private void Start()
        {
            horizontal.onJoystickMove.AddListener(MoveHorizontal);
            vertical.onJoystickMove.AddListener(MoveVertical);
            rotationX.onJoystickMove.AddListener(PlayerRotation);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //if (movementActive && movementCor == null) movementCor = StartCoroutine(PlayerMovement());
            //if (!rotationX.IsActive) PlayerRotation();
        }

        // Gere les mouvement du joueur en fonction du joystick gauche
        //IEnumerator PlayerMovement() {

        //    if (myPinata.player != PhotonNetwork.LocalPlayer)
        //        yield break;

        //    //float horizontal = horizontal.
        //    float vertical = InputManagerQ.Instance.GetAxis("Vertical");
        //    Vector3 movementVector = new Vector3(horizontal, 0, vertical)* data.movementSpeed;

        //    myPinata.characterMovementBehaviour.body.AddDirectForce(transform.TransformVector(movementVector));

        //    myPinata.animatorBehaviour.SetFloat("vertical", vertical);
        //    myPinata.animatorBehaviour.SetFloat("horizontal", horizontal);

        //    yield return new WaitForEndOfFrame();

        //    movementCor = null;
        //}

        void MoveHorizontal(float value)
        {
            /*if (myPinata.player != PlayerManager.Instance.LocalPlayer.player)
                return;*/

            Vector3 movementVector = new Vector3(value, 0, 0) * data.movementSpeed;
            myPinata.characterMovementBehaviour.body.AddDirectForce(transform.TransformVector(movementVector));
            myPinata.animatorBehaviour.SetFloat("horizontal", value);
            //movementCor = null;
        }

        void MoveVertical(float value)
        {
            /*if (myPinata.player != PlayerManager.Instance.LocalPlayer.player)
                return;*/

            Vector3 movementVector = new Vector3(0, 0, value) * data.movementSpeed;
            myPinata.characterMovementBehaviour.body.AddDirectForce(transform.TransformVector(movementVector));
            myPinata.animatorBehaviour.SetFloat("vertical", value);
            //movementCor = null;
        }

        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation(float value)
        {
            cameraTarget.rotation = smoothRotation(cameraTarget.rotation, data.rotationAcceleration, value);
            myPinata.characterMovementBehaviour.transform.rotation = smoothRotation(myPinata.characterMovementBehaviour.transform.rotation, data.rotationAcceleration, value);
        }

        // Realise une rotation par acceleration
        Quaternion smoothRotation(Quaternion startRotationVector, float smoothSpeed, float value)
        {
            rightJoyX += value * data.rotationSpeed;
            rightJoyX %= 360;
            Quaternion endRotationVector = Quaternion.Euler(0, rightJoyX, 0);
            return Quaternion.Slerp(startRotationVector, endRotationVector, smoothSpeed);
        }

        public void setMovementActive(bool value)
        {
            Debug.Log("Mouvements set to " + value);
            /*horizontal.IsActive = value;
            vertical.IsActive = value;*/
        }

        public void setRotationActive(bool value)
        {
            Debug.Log("Rotation set to " + value);
            //rotationX.IsActive = value;
        }

        public float getRotationAngle()
        {
            return rightJoyX;
        }
    }
}
