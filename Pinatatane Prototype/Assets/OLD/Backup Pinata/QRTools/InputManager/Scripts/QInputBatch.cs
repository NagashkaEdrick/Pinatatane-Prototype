using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs {
    [CreateAssetMenu(menuName = "QRTools/Inputs/Input Batch", fileName = "New Batch")]
    public class QInputBatch : QInputs
    {
        [BoxGroup("Input", order: 5)]
        public QInputs[] inputs;

        public override float TestAxis()
        {
            return 0;
        }

        public override void TestInput()
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].TestInput();
            }
        }
    }
}