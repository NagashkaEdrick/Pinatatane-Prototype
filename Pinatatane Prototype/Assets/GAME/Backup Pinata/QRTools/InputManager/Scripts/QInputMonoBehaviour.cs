using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    public class QInputMonoBehaviour : MonoBehaviour
    {
        public QInputs[] inputs;

        private void Update()
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].TestInput();
            }
        }
    }
}