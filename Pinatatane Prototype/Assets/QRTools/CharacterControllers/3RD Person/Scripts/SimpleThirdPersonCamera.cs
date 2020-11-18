using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;

namespace QRTools
{
    public class SimpleThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] QInputAxis horizontal, vertical;

        public Transform handler;
        public Transform target;

        public Vector3 offset = new Vector3();
        public float lerpFollowingSpeed = 0.02f;

        public float angleH;
        public float angleV;

        public float speed = 5;
        public Vector2 clampV = new Vector2();

        Vector3 targetPos;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            FollowTarget();
            LookTarget();
        }

        void FollowTarget()
        {
            handler.transform.position = Vector3.Lerp(handler.transform.position, target.transform.position, lerpFollowingSpeed);
        }

        void LookTarget()
        {
            targetPos = target.position + offset;
            transform.LookAt(targetPos);
        }

        void Init()
        {
            horizontal.onJoystickMove.AddListener(MoveVertical);
            vertical.onJoystickMove.AddListener(MoveHorizontal);
        }

        void MoveHorizontal(float _value)
        {
            angleH += _value * speed * Time.deltaTime;

            handler.localRotation = Quaternion.Euler(handler.localRotation.eulerAngles.x, angleH, handler.localRotation.eulerAngles.z);
        }

        void MoveVertical(float _value)
        {
            angleV += _value * speed * Time.deltaTime;

            angleV = Mathf.Clamp(angleV, clampV.x, clampV.y);

            handler.localRotation = Quaternion.Euler(angleV, handler.localRotation.eulerAngles.y, handler.localRotation.eulerAngles.z);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(targetPos, .5f);
            Gizmos.DrawLine(targetPos, transform.position);
        }
    }
}