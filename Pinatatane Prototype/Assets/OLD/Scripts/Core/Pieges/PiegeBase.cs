using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace OldPinatatane
{
    public abstract class PiegeBase<T> : SerializedMonoBehaviour where T : PiegeData
    {
        [SerializeField] public T piegeData;

        public abstract void Effect(Pinata p);
        public abstract void FeedBackObject();
    }
}