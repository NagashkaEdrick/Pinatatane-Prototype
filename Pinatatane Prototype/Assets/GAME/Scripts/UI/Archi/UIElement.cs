using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Pinatatane
{
    public class UIElement : MonoBehaviour
    {
        public RectTransform rectTransform;
        public CanvasGroup canvasGroup;

        public UnityEvent
            onShow,
            onHide;

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            rectTransform = GetComponent<RectTransform>();
            TryGetComponent(out canvasGroup);
        }

        public virtual void Refresh()
        {
            
        }

        [ButtonGroup("Buttons")]
        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            OnShow();
        }

        [ButtonGroup("Buttons")]
        public void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            OnHide();
        }

        public virtual void OnShow()
        {
            onShow?.Invoke();
        }

        public virtual void OnHide()
        {
            onHide?.Invoke();
        }
    }
}