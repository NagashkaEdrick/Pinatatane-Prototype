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

        [SerializeField] QInputXBOXTouch dashAction;  

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
                if (dashCor == null && !myPinata.pinataOverrideControl.isOverrided && !myPinata.isStatic)
                {
                    dashCor = StartCoroutine(StartDash());
                }
            }
        }

        IEnumerator StartDash() {
            if (body.GetVelocity() == Vector3.zero) {
                body.AddForce(transform.forward * dashForce);
            } else {
                float horizontal = InputManagerQ.Instance.GetAxis("Horizontal");
                float vertical = InputManagerQ.Instance.GetAxis("Vertical");
                Vector3 movementVector = new Vector3(horizontal, 0, vertical) * dashForce;
                body.AddForce(transform.TransformVector(movementVector));
            }
            yield return new WaitForSeconds(data.dashCooldown);
            dashCor = null;
        }
    }
}
