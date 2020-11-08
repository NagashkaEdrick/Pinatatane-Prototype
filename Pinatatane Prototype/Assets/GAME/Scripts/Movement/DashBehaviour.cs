using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QRTools.Inputs;
using Photon.Pun;

namespace Pinatatane
{
    public class DashBehaviour : MonoBehaviour 
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;
        public float dashForce;

        [BoxGroup("Fix")]
        [SerializeField]
        SimplePhysic body;
        [BoxGroup("Fix")]
        [SerializeField]
        CharacterMovementBehaviour movement;
        [SerializeField] Pinata myPinata;

        [SerializeField]
        QInputXBOXTouch dashAction;

        [SerializeField]
        QInputXBOXAxis horizontal, vertical;

        private Coroutine dashCor = null;

        private void Start()
        {
            dashAction.onDown.AddListener(DashAction);
        }

        public void DashAction() {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else
            {
                if (dashCor == null && !myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToMove)
                {
                    dashCor = StartCoroutine(StartDash());
                }
            }
        }

        IEnumerator StartDash() {
            NetworkDebugger.Instance.Debug(myPinata.body.GetVelocity(), DebugType.LOCAL);
            if (myPinata.body.GetVelocity() == Vector3.zero) {
                //NetworkDebugger.Instance.Debug("Front dash", DebugType.LOCAL);
                body.AddForce(transform.forward * dashForce);
            } else {
                //NetworkDebugger.Instance.Debug(horizontal.JoystickValue + "   " + vertical.JoystickValue, DebugType.LOCAL);
                Vector3 movementVector = new Vector3(horizontal.JoystickValue, 0, vertical.JoystickValue) * dashForce;
                body.AddForce(transform.TransformVector(movementVector));
            }
            yield return new WaitForSeconds(data.dashCooldown);
            dashCor = null;
        }
    }
}
