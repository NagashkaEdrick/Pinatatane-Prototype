using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

namespace GameplayFramework.Singletons
{
    /// <summary>
    /// An unique Instance callable every where
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonobehaviourSingleton<T> : MyMonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null)
                    {
                        //Debug.LogError("There is no " + typeof(T).Name + " in the scene.");
                    }
                }
                return _instance;
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