using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace QRTools
{
    public abstract class Behaviour<T> : SerializedScriptableObject
    {
        [SerializeField, TextArea(3, 5)] string description;

        public abstract void Init(T element);

        public abstract void Execute(T element);
    }
}