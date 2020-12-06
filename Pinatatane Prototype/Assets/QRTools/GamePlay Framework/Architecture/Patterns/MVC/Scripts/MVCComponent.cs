using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class MVCComponent : MyMonoBehaviour
    {
        [SerializeField] Model m_Model;
        [SerializeField] View m_View;
        [SerializeField] Controller m_Controller;

        public Model Model { get => m_Model; set => m_Model = value; }
        public View View { get => m_View; set => m_View = value; }
        public Controller Controller { get => m_Controller; set => m_Controller = value; }
    }
}