using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace GameplayFramework
{
    [CreateAssetMenu(menuName = "GameplayFramework/3C/Camera/Camera Profile", fileName = "Camera Profile")]
    public class CameraControllerProfile : SerializedScriptableObject
    {
        [BoxGroup("Settings"), Tooltip("Rotation speed of the camera.")]
        public float rotationSpeed = 150;

        [BoxGroup("Settings"), Tooltip("Add an offset on the target for the look rotation.")]
        public Vector3 positionOffset = new Vector3();

        [BoxGroup("Settings"), Tooltip("Add an offset on the camera transform.")]
        public Vector3 lookTargetOffset = new Vector3();

        [FoldoutGroup("More Settings"), Tooltip("Lerp speed for the following movement.")]
        public float lerpFollowingSpeed = 0.2f;

        [FoldoutGroup("More Settings"), Tooltip("Lerp speed for the camera according to it's offset.")]
        public float lerpFollowingOffsetSpeed = 0.2f;

        [FoldoutGroup("More Settings"), Tooltip("The camera can't go beyond this values on Y rotation.")]
        public Vector2 clamp_RotationY = new Vector2(10f, 80f);

        [FoldoutGroup("More Settings"), Tooltip("Sensibility on X axis"), Range(0, 2)]
        public float cameraSensibilityX = 1;

        [FoldoutGroup("More Settings"), Tooltip("Sensibility on Y axis"), Range(0, 2)]
        public float cameraSensibilityY = 1;

        [FoldoutGroup("More Settings"), Tooltip("Block rotation on a target axis")]
        public bool
            blockRotationInX = false,
            blockRotationInY = false;
    }
}