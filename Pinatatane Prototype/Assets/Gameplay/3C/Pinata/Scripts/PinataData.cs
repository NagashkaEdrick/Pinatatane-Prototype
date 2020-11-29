using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Gameplay/3C/Pinata", fileName = "PinataData")]
    public class PinataData : SerializedScriptableObject
    {
        
        [FoldoutGroup("Pinata Movement")]
        public float movementSpeed = 5f;

        [FoldoutGroup("Pinata Movement")]
        public float aimMovementSpeed = 4f;

        [FoldoutGroup("Pinata Movement")]
        public float aimTransitionTime = 1f;

        [FoldoutGroup("Pinata Movement")]
        public float rotationSpeedLerp = 2f;

        [FoldoutGroup("Pinata Movement")]
        public float grabMovementSpeed = 1f;

        [FoldoutGroup("Dash")]
        public float dashForce = 100f;

        [FoldoutGroup("Dash")]
        public float dashCoolDown = 1f;
    }
}