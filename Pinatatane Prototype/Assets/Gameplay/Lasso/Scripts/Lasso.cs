using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using GameplayFramework;

namespace Pinatatane {
    public class Lasso : MyMonoBehaviour
    {
        [SerializeField] LassoData m_LassoData;
        [SerializeField, ReadOnly] IGrabbable m_CurrenObjectGrabbed;

        public LassoData LassoData { get => m_LassoData; set => m_LassoData = value; }
        public IGrabbable CurrenObjectGrabbed
        {
            get => m_CurrenObjectGrabbed;
            set
            {
                m_CurrenObjectGrabbed = value;
                if (m_CurrenObjectGrabbed != null)
                {
                    m_CurrenObjectGrabbed.OnStartGrabbed();
                }
            }
        }

        protected override void OnGameEnd()
        {
        }

        protected override void OnGameStart()
        {
        }
    }
}