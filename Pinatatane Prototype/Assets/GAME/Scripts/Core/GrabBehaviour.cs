using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class GrabBehaviour : MonoBehaviour
    {
        private Coroutine grabCor = null;

        public float duration = 1f;
        public float speed = 10f;

        AnimatorBehaviour animator => PlayerManager.Instance.LocalPlayer.animatorBehaviour;

        public void GrabAction()
        {
            if (grabCor == null)
            {
                grabCor = StartCoroutine(LaunchGrab());
            }
        }

        IEnumerator LaunchGrab()
        {
            animator.SetBool("grab", true);
            transform.Translate(transform.forward * speed * Time.deltaTime);
            yield return new WaitForSeconds(duration);
            grabCor = null;
            animator.SetBool("grab", false);
            yield break;
        }
    }
}
