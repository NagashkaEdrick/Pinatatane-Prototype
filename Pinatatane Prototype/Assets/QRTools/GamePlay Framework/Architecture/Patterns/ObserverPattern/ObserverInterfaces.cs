using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework.ObserverPattern
{
    /// <summary>
    /// Interface to tag an object can be observer
    /// </summary>
    public interface IObserver
    {
        List<IObservable> Observables { get; set; }

        void NotifyObservables();
    }

    /// <summary>
    /// Interface to tag an object can be observed
    /// </summary>
    public interface IObservable
    {
        Observer Observer { get; set; }

        void RegisterObserver(IObserver _observer);
        void UnregisterObserver(IObserver _observer);
        void OnNotify();
    }
}