using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QRTools.Inputs;
using Photon.Pun;
using Cinemachine;

namespace OldPinatatane {

    /***
     *  Comportement de mouvement du joueur lorsqu'il ne vise pas :
     *  Le joystick gauche sert a se deriger en fonction de la direction de la camera
     */

    public class FreeLookMovement : MonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public CharacterControllerData data;
        [BoxGroup("Tweaking")]
        [SerializeField] float rotationTime = 0.1f;

        [BoxGroup("Inputs")]
        [SerializeField] QInputXBOXAxis horizontal, vertical;

        [BoxGroup("Fix")]
        [SerializeField] Pinata myPinata;
        public CinemachineFreeLook cinemachine;

        float turnSmoothVelocity;
        Coroutine movementCor = null;

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

            float horizontal = this.horizontal.JoystickValue;
            float vertical = this.vertical.JoystickValue;

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + myPinata.mainCamera.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                myPinata.body.AddDirectForce(moveDir.normalized * data.movementSpeed);
            }

            myPinata.animatorBehaviour.SetFloat("vertical", vertical);
            myPinata.animatorBehaviour.SetFloat("horizontal", horizontal);

            yield return new WaitForEndOfFrame();

            movementCor = null;
        }
    }
}
