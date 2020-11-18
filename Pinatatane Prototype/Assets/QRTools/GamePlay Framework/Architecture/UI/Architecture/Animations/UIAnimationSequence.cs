using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class UIAnimationSequence : UIAnimation
    {
        [SerializeField] UIAnimField[] animations;

        protected override void Animation()
        {
            StartCoroutine(AnimationCoroutine());
        }

        IEnumerator AnimationCoroutine()
        {
            for (int i = 0; i < animations.Length; i++)
            {
                yield return new WaitForSeconds(animations[i].delay);
                animations[i].Animate();
            }
            yield break;
        }
    }
}