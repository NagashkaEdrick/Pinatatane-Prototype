using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class CameraTransition : MonoBehaviour
    {
        [BoxGroup("Fix")]
        [SerializeField] AnimatorBehaviour animator;
        [BoxGroup("Fix")]
        [SerializeField] QInputXBOXTouch button;

        private void Start()
        {
            button.onDown.AddListener(OnAim);
            button.onUp.AddListener(OnRealeaseAim);
        }

        public void OnAim()
        {
            animator.SetBool("isAiming", true);
        }

        public void OnRealeaseAim()
        {
            animator.SetBool("isAiming", false);
        }
    }
}
