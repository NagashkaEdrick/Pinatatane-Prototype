using System.Collections;

using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

namespace GameplayFramework.Network
{
    [CreateAssetMenu(menuName = "GameplayFramework/Network/NetworkSettings", fileName = "Network Settings")]
    public class NetworkSettings : SerializedScriptableObject
    {
        [Range(2,20)] public byte MaxPlayerInRoom = 10;
    }
}