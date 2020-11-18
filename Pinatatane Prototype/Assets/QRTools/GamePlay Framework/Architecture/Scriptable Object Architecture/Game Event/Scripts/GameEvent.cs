using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/SO Architecture/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField, TextArea(3, 5)] string description;

        private List<GameEventListener> listeners =
                new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        { listeners.Add(listener); }

        public void UnregisterListener(GameEventListener listener)
        { listeners.Remove(listener); }
    }
}