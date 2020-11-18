using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace GameplayFramework.ObserverPattern
{
    /// <summary>
    /// An observer notify all object registered to it.
    /// </summary>
    public class Observer : SerializedMonoBehaviour, IObserver
    {
        [SerializeField] List<IObservable> m_Observables = new List<IObservable>();
        public List<IObservable> Observables { get => m_Observables; set => m_Observables = value; }

        public void NotifyObservables()
        {
            if (m_Observables != null)
            {
                for (int i = 0; i < m_Observables.Count; i++)
                {
                    m_Observables[i].OnNotify();
                }
            }
        }
    }
}