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
            Refresh();
            rectTransform = GetComponent<RectTransform>();
            TryGetComponent(out canvasGroup);
        }

        public virtual void Refresh()
        {

        }

        public void Show()
        {
            OnShow();
        }

        public void Hide()
        {
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