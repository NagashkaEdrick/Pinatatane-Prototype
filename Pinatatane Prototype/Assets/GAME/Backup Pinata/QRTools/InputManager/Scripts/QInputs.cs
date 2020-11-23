using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

namespace QRTools.Inputs
{
    public abstract class QInputs : SerializedScriptableObject
    {
        [TextArea(3, 5), SerializeField, BoxGroup("Input", order: 5)] string description;

        [SerializeField, BoxGroup("Input", order: 5)] string _inputName;
        public string InputName
        {
            get => _inputName;
            set => _inputName = value;
        }

        [SerializeField, BoxGroup("Input", order: 5)] bool isActive = true;
        public bool IsActive { get => isActive; set => isActive = value; }

        public abstract void TestInput();

        public abstract float TestAxis();
    }
}
