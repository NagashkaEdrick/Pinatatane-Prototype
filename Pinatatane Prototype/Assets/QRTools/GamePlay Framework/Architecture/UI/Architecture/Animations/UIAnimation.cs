using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

namespace GameplayFramework
{
    public abstract class UIAnimation : MonoBehaviour
    {
        public float animationSpeed = 1f;

        [SerializeField] protected const float m_delay = 0f;

        public virtual void Animate(float _delay = m_delay)
        {
            StartCoroutine(AnimationCoroutine(_delay));
        }

        protected IEnumerator AnimationCoroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            Animation();
            yield break;
        }

        protected abstract void Animation();
    }

    [System.Serializable]
    public class UIAnimField
    {
        [BoxGroup("Anim", ShowLabel = false)]
        public UIAnimation animation;
        [BoxGroup("Anim", ShowLabel = false)]
        public float delay;

        public void Animate() => animation?.Animate();
    }
}