using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;
using UnityEngine.Events;

namespace GameplayFramework
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : SerializedMonoBehaviour
    {
        public CanvasGroup CanvasGroup { get; protected set; }

        UnityEvent onShow, onHide;

        public virtual void Show()
        {
            onShow?.Invoke();
        }

        public virtual void Hide()
        {
            onHide?.Invoke();
        }

        public void FindReferences()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }
    }
}