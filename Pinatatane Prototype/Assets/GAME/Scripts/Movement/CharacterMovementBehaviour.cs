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
        Coroutine movementCor = null;

        [SerializeField] Pinata myPinata;

        [BoxGroup("Inputs", order: 2)]
        [SerializeField] QInputXBOXAxis horizontal, vertical, rotationX;
        [BoxGroup("Inputs", order: 2)]
        [SerializeField] QInputXBOXTouch aimLock;

        bool isAiming = false;
        float turnSmoothVelocity;

        private void Start()
        {
            /*horizontal.onJoystickMove.AddListener(MoveHorizontal);
            vertical.onJoystickMove.AddListener(MoveVertical);*/
            rotationX.onJoystickMove.AddListener(PlayerRotation);
            aimLock.onDown.AddListener(OnAim);
            aimLock.onUp.AddListener(OnRealeaseAim);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (myPinata.isAllowedToMove && movementCor == null)
            {
                if (isAiming) movementCor = StartCoroutine(PlayerMovement());
                else movementCor = StartCoroutine(PlayerMovement2());
            }
            //if (myPinata.isAllowedToRotate) PlayerRotation();
        }

        // Gere les mouvement du joueur en fonction du joystick gauche
        IEnumerator PlayerMovement()
        {

            if (myPinata.player != PhotonNetwork.LocalPlayer)
                yield break;

            Vector3 movementVector = new Vector3(horizontal.JoystickValue, 0, vertical.JoystickValue) * data.movementSpeed;

            myPinata.body.AddDirectForce(transform.TransformVector(movementVector));

            myPinata.animatorBehaviour.SetFloat("vertical", vertical.JoystickValue);
            myPinata.animatorBehaviour.SetFloat("horizontal", horizontal.JoystickValue);

            yield return new WaitForEndOfFrame();

            movementCor = null;
        }
        IEnumerator PlayerMovement2()
        {
            // Comportement quand on ne vise pas
            if (myPinata.player != PhotonNetwork.LocalPlayer)
                yield break;

            float horizontal = this.horizontal.JoystickValue;
            float vertical = this.vertical.JoystickValue;

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + myPinata.mainCamera.transform.eulerAngles.y ;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                myPinata.body.AddDirectForce(moveDir.normalized * data.movementSpeed);
            }

            myPinata.animatorBehaviour.SetFloat("vertical", vertical);
            myPinata.animatorBehaviour.SetFloat("horizontal", horizontal);
            
            yield return new WaitForEndOfFrame();

            movementCor = null;
        }


        /*void MoveHorizontal(float value)
        {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else
            {
                if (!myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToMove)
                {
                    Vector3 movementVector = new Vector3(value, 0, 0) * data.movementSpeed;
                    myPinata.characterMovementBehaviour.body.AddDirectForce(transform.TransformVector(movementVector));
                    myPinata.animatorBehaviour.SetFloat("horizontal", value);
                }
            }
        }

        void MoveVertical(float value)
        {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else
            {
                if (!myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToMove)
                {
                    Vector3 movementVector = new Vector3(0, 0, value) * data.movementSpeed;
                    myPinata.characterMovementBehaviour.body.AddDirectForce(transform.TransformVector(movementVector));
                    myPinata.animatorBehaviour.SetFloat("vertical", value);
                }
            }
        }*/

        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation(float value)
        {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else
            {
                if (!myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToRotate && isAiming)
                {
                    cameraTarget.rotation = smoothRotation(cameraTarget.rotation, data.rotationAcceleration, value);
                    myPinata.characterMovementBehaviour.transform.rotation = smoothRotation(myPinata.characterMovementBehaviour.transform.rotation, data.rotationAcceleration, value);
                }
            }
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
            myPinata.isAllowedToMove = value;
        }

        public void setRotationActive(bool value)
        {
            myPinata.isAllowedToRotate = value;
        }

        public float getRotationAngle()
        {
            return rightJoyX;
        }

        public void OnAim()
        {
            isAiming = true;
        }

        public void OnRealeaseAim()
        {
            isAiming = false;
        }
    }
}
