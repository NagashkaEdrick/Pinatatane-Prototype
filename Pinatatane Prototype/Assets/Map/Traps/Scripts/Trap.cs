using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Pinatatane {
    public abstract class Trap<T> : SerializedMonoBehaviour where T:TrapData
    {
        [SerializeField] UnityEvent m_OnEnter;
        [SerializeField] UnityEvent m_OnExit;
        [SerializeField] protected T trapData;

        public abstract void OnEnter(IReceiveDamages objectTrapped);
        public abstract void OnExit();

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision detecter " + collision.collider.gameObject.name + collision.gameObject.name);
            if (collision.collider.gameObject.GetComponent(typeof(IReceiveDamages)))
            {
                Debug.Log("collision detecter avec receivedDamages");
                m_OnEnter?.Invoke();
                IReceiveDamages objectTrapped = (IReceiveDamages)collision.collider.gameObject.GetComponent(typeof(IReceiveDamages));
                OnEnter(objectTrapped);
                objectTrapped.ReceivedDamage(CalculateDamages(objectTrapped.Velocity));
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent(typeof(IReceiveDamages)))
            {
                m_OnExit?.Invoke();
            }
        }

        protected float CalculateDamages(float velocity)
        {
            return trapData.damage + (velocity > trapData.minVelocity ? trapData.baseDamage * velocity * trapData.damageMultiplicator : 0f);
        }

        protected void BlockObject(IBlockable blockedObject, float time)
        {
            StartCoroutine(BlockCoroutine(blockedObject, time));
        }

        IEnumerator BlockCoroutine(IBlockable blockedObject, float time)
        {
            blockedObject.IsBlocked = true;
            blockedObject.OnBlockedEnter();
            yield return new WaitForSeconds(time);
            blockedObject.IsBlocked = false;
            blockedObject.OnBlockedExit();
            yield break;
        }
    }
}
