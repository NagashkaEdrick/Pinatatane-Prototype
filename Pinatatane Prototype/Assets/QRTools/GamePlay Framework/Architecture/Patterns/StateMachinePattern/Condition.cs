using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameplayFramework
{
    /// <summary>
    /// A condition for a generic state machine 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Condition<T> : SerializedMonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, TextArea(3, 5)] string Description = "";
#endif

        public abstract bool CheckCondition(T element);
    }
}