using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Debugging
{
    [CreateAssetMenu(menuName = "QRTools/Debugging/Message", fileName = "Debug Message")]
    public class DebugSO : ScriptableObject
    {
        public void DebugMessage(string message) => Debug.Log(message);
    }
}