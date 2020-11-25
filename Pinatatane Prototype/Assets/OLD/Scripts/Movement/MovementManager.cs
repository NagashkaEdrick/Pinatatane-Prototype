using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;

namespace OldPinatatane
{

    /*** Gere la transition entre le mode viser et le mode free look
     *      - ecoute du bouton de viser
     *      - enlever / remettre le curseur
     *      - tourner le joueur dos a la camera quand on vise
     */

    public class MovementManager : MonoBehaviour
    {
        [SerializeField] Pinata myPinata;
        [SerializeField] FreeLookMovement freelook;
        [SerializeField] AimingMovement aimingMovement;
        [SerializeField] AimingRotation aimingRotation;
        [SerializeField] QInputXBOXTouch aimInput;
        [SerializeField] float turningRotationSpeed;

        bool isNeedingFocus = false;
        Vector3 newForward;
        int mode = 0; // 0: free 1: aim

        private void Start()
        {
            aimInput.onDown.AddListener(OnAim);
            aimInput.onUp.AddListener(OnRealeaseAim);
        }

        private void Update()
        {
            if (isNeedingFocus) {
                transform.forward = Vector3.RotateTowards(transform.forward, newForward.normalized, turningRotationSpeed * Time.deltaTime, 0.0f);
                if (Vector3.Distance(transform.forward, newForward) <= 0.05f)
                {
                    // Changement d'un comportement de mouvement a l'autre
                    aimingMovement.enabled = true;
                    aimingRotation.enabled = true;
                    aimingRotation.LinkRotationInput();
                    isNeedingFocus = false;
                    mode = 1;
                }
            }
        }

        // Lancer une fois lorsque la touche de viser est enfoncer
        private void OnAim()
        {
            // Debloquer le grab
            myPinata.grabBehaviour.SetGrabActive(true);

            // Rotation du joueur
            Transform cameraPosition = PlayerManager.Instance.LocalPlayer.mainCamera.transform;
            newForward = new Vector3(cameraPosition.forward.x, 0, cameraPosition.forward.z);
            isNeedingFocus = true;

            // Changement d'un comportement de mouvement a l'autre
            freelook.enabled = false;
        }

        // Lancer une fois lorsque la touche de viser est relacher
        private void OnRealeaseAim()
        {
            // Bloquer le grab
            myPinata.grabBehaviour.SetGrabActive(false);

            // Changement d'un comportement de mouvement a l'autre
            aimingRotation.UnlinkRotationInput();
            aimingMovement.enabled = false;
            aimingRotation.enabled = false;
            freelook.enabled = true;
            mode = 0;
        }

        public void setMovementActive(bool value)
        {
            if (mode == 0) freelook.enabled = value;
            else aimingMovement.enabled = value;
        }

        public void setRotationActive(bool value)
        {
            if (mode == 1)
            {
                if (value) aimingRotation.LinkRotationInput();
                else aimingRotation.UnlinkRotationInput();
            }
        }

        public bool isAiming()
        {
            return mode == 1;
        }
    }
}
