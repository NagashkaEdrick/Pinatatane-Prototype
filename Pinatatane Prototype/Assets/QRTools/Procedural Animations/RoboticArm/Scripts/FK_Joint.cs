using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework.ProceduralAnimation
{
    public class FK_Joint : MonoBehaviour
    {
        public Vector3 axis;
        public Vector3 startOffset;

        private void Start()
        {
            startOffset = transform.localPosition;
        }
    }
}