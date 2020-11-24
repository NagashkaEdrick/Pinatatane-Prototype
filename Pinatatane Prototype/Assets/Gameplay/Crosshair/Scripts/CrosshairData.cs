using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/UI/CrosshairData", fileName = "CrosshairData")]
    public class CrosshairData : ScriptableObject
    {
        [Range(0f, 1f)]
        public float positionY = 0.5f;
    }
}
