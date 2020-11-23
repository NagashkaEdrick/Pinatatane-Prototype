using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    public class Billboard : MonoBehaviour
    {
        public Transform camTransform;

        private void LateUpdate()
        {
            if (camTransform != null)
            {
                transform.LookAt(transform.position + camTransform.rotation * Vector3.forward,
                    camTransform.rotation * Vector3.up
                    );
            }
        }


    }
}