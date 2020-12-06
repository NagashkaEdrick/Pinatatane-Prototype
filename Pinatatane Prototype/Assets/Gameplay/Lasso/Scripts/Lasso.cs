using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using GameplayFramework;

namespace Pinatatane {
    public class Lasso : MyMonoBehaviour
    {
        [SerializeField] LassoData m_LassoData;
        [SerializeField] IGrabbable m_MyGraddable;
        [SerializeField, ReadOnly] IGrabbable m_CurrenObjectGrabbed;
        [SerializeField] LassoGraphics m_LassoGraphics;
        [SerializeField] LassoController m_LassoController;

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
        public LassoGraphics LassoGraphics { get => m_LassoGraphics; set => m_LassoGraphics = value; }
        public IGrabbable MyGraddable { get => m_MyGraddable; set => m_MyGraddable = value; }
        public LassoController LassoController { get => m_LassoController; set => m_LassoController = value; }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (m_CurrenObjectGrabbed != null)
                LassoGraphics.Launch(m_CurrenObjectGrabbed.Transform.position);
        }

        protected override void OnGameEnd()
        {
        }

        protected override void OnGameStart()
        {
        }
    }
}