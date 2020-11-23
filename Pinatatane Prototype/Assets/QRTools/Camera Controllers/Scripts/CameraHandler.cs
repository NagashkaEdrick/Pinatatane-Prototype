using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools {
    public class CameraHandler : MonoBehaviour
    {
        public Camera camera;
        public CameraController cameraController = default;
        public Transform cameraTransform = default;
        public Transform handler;
        public Transform target;
        public float angleH;
        public float angleV;

        private void OnDrawGizmosSelected()
        {
            if (target != null && cameraTransform != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(target.position, .5f);
                Gizmos.DrawLine(cameraTransform.position, transform.position);
            }
        }
    }
}