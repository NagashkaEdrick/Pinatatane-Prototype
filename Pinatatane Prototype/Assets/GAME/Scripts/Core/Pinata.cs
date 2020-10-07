using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class Pinata : MonoBehaviour
    {
        public CharacterController characterController;
        public CameraController cameraController;

        public Transform cameraTarget;

        public void InitPlayer()
        {
            cameraController.target = cameraTarget;
        }
    }
}