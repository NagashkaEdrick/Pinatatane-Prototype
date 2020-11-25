using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using OldPinatatane;
using Photon.Pun;
using QRTools.Inputs;

namespace OldPinatatane
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
    }
}
