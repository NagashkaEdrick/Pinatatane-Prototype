using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using GameplayFramework.Singletons;

namespace Pinatatane
{
    public class UIReferenceManager : MonobehaviourSingleton<UIReferenceManager>
    {
        [SerializeField] Image m_CrossHair;

        public Image CrossHair { get => m_CrossHair; set => m_CrossHair = value; }
    }
}