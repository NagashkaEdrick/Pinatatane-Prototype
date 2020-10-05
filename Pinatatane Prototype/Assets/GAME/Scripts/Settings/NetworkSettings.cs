using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

namespace Pinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Settings/Network Settings", fileName = "Network Settings")]
    public class NetworkSettings : SerializedScriptableObject
    {
        [Range(1, 20)] public byte playerCountMax;
    }
}