using System.Collections;
using System.Collections.Generic;

using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    public class QInputsTouch : QInputs
    {

        [BoxGroup("Events", order: 50)]
        public UnityEvent
            onDown,
            onCurrent,
            onUp;

        [SerializeField, BoxGroup("Debug", order: 100), ReadOnly] bool isTrigger = false;
        public bool IsTrigger { get => isTrigger; set => isTrigger = value; }

        public override void TestInput()
        {
        }

        public override float TestAxis()
        {
            return 0;
        }
    }
}