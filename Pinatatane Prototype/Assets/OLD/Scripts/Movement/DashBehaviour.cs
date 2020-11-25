using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QRTools.Inputs;
using Photon.Pun;

namespace OldPinatatane
{
    public class DashBehaviour : MonoBehaviour 
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;
        public float dashForce;

        [BoxGroup("Fix")]
        [SerializeField]
        SimplePhysic body;
        [SerializeField] Pinata myPinata;

        [SerializeField]
        QInputXBOXTouch dashAction;

        [SerializeField]
        QInputXBOXAxis horizontal, vertical;

        Coroutine dashCor = null;

        private void Start()
        {
            dashAction.onDown.AddListener(DashAction);
        }

        public void DashAction() {
            if (myPinata.player != PlayerManager.Instance.LocalPlayer.player && PhotonNetwork.IsConnected)
                return;
            else if (dashCor == null && !myPinata.pinataOverrideControl.isOverrided && myPinata.isAllowedToMove)
                dashCor = StartCoroutine(StartDash());
        }

        IEnumerator StartDash() {
            Vector3 movementVector = new Vector3(horizontal.JoystickValue, 0, vertical.JoystickValue) * dashForce;
            if (movementVector == Vector3.zero) {
                body.AddForce(transform.forward * dashForce);
            } else {
                if (myPinata.movement.isAiming()) body.AddForce(transform.TransformVector(movementVector));
                else body.AddForce(transform.forward * dashForce);
            }
            yield return new WaitForSeconds(data.dashCooldown);
            dashCor = null;
        }
    }
}
