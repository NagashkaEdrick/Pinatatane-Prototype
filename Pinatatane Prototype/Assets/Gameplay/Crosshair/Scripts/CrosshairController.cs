using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using UnityEngine.UI;

namespace Pinatatane
{
    public class CrosshairController : MonoBehaviour
    {

        [SerializeField] Image crosshairImage;
        [SerializeField] CrosshairData data;

        // Start is called before the first frame update
        void Start()
        {
            InputManager.Instance.aimButton.onDown.AddListener(Enable);
            InputManager.Instance.aimButton.onUp.AddListener(Disable);
            Disable();
        }

        void Enable()
        {
            crosshairImage.enabled = true;
        }

        private void Disable()
        {
            crosshairImage.enabled = false;
        }

        private void Update()
        {
            crosshairImage.transform.position = new Vector3(crosshairImage.transform.position.x, data.positionY * Screen.height, 0);
        }
    }
}
