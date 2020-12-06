using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace Pinatatane
{
    public class DashController : MonoBehaviour
    {
        [SerializeField] Pinata m_pinata;

        Coroutine cor = null;

        private void Start()
        {
            InputManager.Instance.dashButton.onDown.AddListener(DashAction);
        }

        void DashAction()
        {
            if (cor == null) cor = StartCoroutine(Dash());
        }

        IEnumerator Dash()
        {
            Vector3 direction = new Vector3(InputManager.Instance.moveX.JoystickValue, 0f, InputManager.Instance.moveY.JoystickValue).normalized;
            if (direction == Vector3.zero || !InputManager.Instance.aimButton.IsTrigger)
            {
                m_pinata.Rigidbody.AddForce(m_pinata.transform.forward * m_pinata.PinataData.dashForce);
            } else
            {
                m_pinata.Rigidbody.AddForce(transform.InverseTransformPoint(direction * m_pinata.PinataData.dashForce));
            }
            yield return new WaitForSeconds(m_pinata.PinataData.dashCoolDown);
            cor = null;
        }
    }
}
