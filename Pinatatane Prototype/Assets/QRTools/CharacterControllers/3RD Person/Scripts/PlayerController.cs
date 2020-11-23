using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace QRTools
{
    public class PlayerController : Pawn
    {
        //Input
        public QInputs[] playerInputs = default;

        //CharacterController
        public CharacterController characterController = default;

        //PlayerCameraManager
        public CameraHandler cameraHandler = default;

        private void Start()
        {
            characterController?.Init(this);
            cameraHandler?.cameraController?.Init(cameraHandler);
        }

        private void Update()
        {
            CalculateInputs();
            characterController?.Execute(this);
            cameraHandler?.cameraController?.Execute(cameraHandler);
        }

        void CalculateInputs()
        {
            for (int i = 0; i < playerInputs.Length; i++)
                playerInputs[i].TestInput();
        }

        private void OnDrawGizmosSelected()
        {
            if (cameraHandler?.target != null && cameraHandler?.cameraTransform != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(cameraHandler.target.position, .5f);
                Gizmos.DrawLine(cameraHandler.cameraTransform.position, transform.position);
            }
        }
    }
}