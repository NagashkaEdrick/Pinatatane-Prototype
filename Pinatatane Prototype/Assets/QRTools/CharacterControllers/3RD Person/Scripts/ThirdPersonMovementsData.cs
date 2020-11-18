using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/PlayerControllers/ThirdPersonMovementsData")]
    public class ThirdPersonMovementsData : ScriptableObject
    {
        public float playerSpeed = 5f;
        public float playerRotationSpeed = 150f;
        [Range(0,1)] public float accelerationLerp = .02f;
        public float playerLerpRotation = .02f;
    }
}