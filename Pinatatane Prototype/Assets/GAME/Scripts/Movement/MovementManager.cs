using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;
using System;
using Cinemachine;

namespace Pinatatane
{

    /*** Gere la transition entre le mode viser et le mode free look
     *      - ecoute du bouton de viser
     *      - enlever / remettre le curseur
     *      - tourner le joueur dos a la camera quand on vise
     */

    public class MovementManager : MonoBehaviour
    {
        [SerializeField] FreeLookMovement freelook;
        [SerializeField] AimingMovement aiming;
        [SerializeField] QInputXBOXTouch aimInput;
        [SerializeField] float turningRotationSpeed;

        bool isNeedingFocus = false;
        Vector3 newForward;

        private void Start()
        {
            aimInput.onDown.AddListener(OnAim);
            aimInput.onUp.AddListener(OnRealeaseAim);
        }

        private void Update()
        {
            Debug.Log(isNeedingFocus);
            if (isNeedingFocus) {
                transform.forward = Vector3.RotateTowards(transform.forward, newForward.normalized, turningRotationSpeed * Time.deltaTime, 0.0f);
                if (Vector3.Distance(transform.forward, newForward) <= 0.05f)
                {
                    // Changement d'un comportement de camera a l'autre
                    PlayerManager.Instance.LocalPlayer.cameraController.enabled = true;
                    // Changement d'un comportement de mouvement a l'autre
                    aiming.enabled = true;
                    aiming.LinkRotationInput();
                    isNeedingFocus = false;

                    /***
                     *  Bug lorsqu'on reactive le comporte de aim car quand repasse a la rotation avec joyX dans AimingMovement
                     *  lui joyX s'est pas mis a jour pendant qu'on a tourner la cam en mode cinemachine
                     *  donc changer ne plus utiliser joyX mais retrouver joyX autrement avec le forward du joueur
                     *  parce que le forward du perso c'est le truc qui se met a jour dans les deux modes quoi qu'il arrive
                     */
                }
            }
        }

        // Lancer une fois lorsque la touche de viser est enfoncer
        private void OnAim()
        {
            // Debloquer le grab

            // Rotation du joueur
            Transform cameraPosition = PlayerManager.Instance.LocalPlayer.cameraController.transform;
            newForward = new Vector3(cameraPosition.forward.x, 0, cameraPosition.forward.z);
            isNeedingFocus = true;

            // Changement d'un comportement de camera a l'autre
            PlayerManager.Instance.LocalPlayer.cameraController.GetComponent<CinemachineBrain>().enabled = false;

            // Changement d'un comportement de mouvement a l'autre
            freelook.enabled = false;
        }

        // Lancer une fois lorsque la touche de viser est relacher
        private void OnRealeaseAim()
        {
            // Bloquer le grab

            // Changement d'un comportement de camera a l'autre
            PlayerManager.Instance.LocalPlayer.cameraController.GetComponent<CinemachineBrain>().enabled = true;
            PlayerManager.Instance.LocalPlayer.cameraController.enabled = false;

            // Changement d'un comportement de mouvement a l'autre
            aiming.UnlinkRotationInput();
            aiming.enabled = false;
            freelook.enabled = true;
        }
    }
}
