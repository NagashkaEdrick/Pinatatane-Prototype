using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Pinatatane;
using Photon.Pun;
using QRTools.Inputs;

namespace Pinatatane
{
    /***
     *  Comportement de mouvement du joueur lorsqu'il vise :
     *  Le joystick gauche sert a strafer dans les 4 directions avant, arriere, gauche, droite
     */

    public class AimingMovement : MonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;

        [BoxGroup("Inputs")]
        [SerializeField] QInputXBOXAxis horizontal, vertical, rotationX;

        [BoxGroup("Fix")]
        [SerializeField] Pinata myPinata;

        Coroutine movementCor = null;
        float rightJoyX;

        // Update is called once per frame
        void Update()
        {
            if (myPinata.isAllowedToMove && movementCor == null)
            {
                movementCor = StartCoroutine(PlayerMovement());
            }
        }

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

        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation(float value)
        {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else if (!myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToRotate) {
                    transform.rotation = smoothRotation(transform.rotation, data.rotationAcceleration, value);
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

        public void LinkRotationInput()
        {
            rotationX.onJoystickMove.AddListener(PlayerRotation);
        }

        public void UnlinkRotationInput()
        {
            rotationX.onJoystickMove.RemoveListener(PlayerRotation);
        }
    }
}
