using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Gameplay/3C/Pinata", fileName = "PinataData")]
    public class PinataData : SerializedScriptableObject
    {
        public float movementSpeed = 5f;
        public float movementLateralSpeed = 2.5f;
    }
}