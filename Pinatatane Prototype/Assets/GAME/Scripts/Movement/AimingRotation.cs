using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;
using QRTools.Inputs;

namespace Pinatatane
{
    public class AimingRotation : MonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;

        [BoxGroup("Inputs")]
        [SerializeField] QInputXBOXAxis rotationX;

        [BoxGroup("Fix")]
        [SerializeField] Pinata myPinata;

        int cptListener = 0;

        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation(float joyValue)
        {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else if (!myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToRotate)
            {
                float currentRotationDegree = transform.rotation.eulerAngles.y;
                currentRotationDegree += joyValue * data.rotationSpeed;
                currentRotationDegree %= 360;
                Quaternion endRotationVector = Quaternion.Euler(0, currentRotationDegree, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, endRotationVector, data.rotationAcceleration);
            }
        }


        public void LinkRotationInput()
        {
            if (cptListener == 0)
            {
                cptListener++;
                rotationX.onJoystickMove.AddListener(PlayerRotation);
            }
        }

        public void UnlinkRotationInput()
        {
            cptListener = 0;
            rotationX.onJoystickMove.RemoveAllListeners();
        }
    }
}