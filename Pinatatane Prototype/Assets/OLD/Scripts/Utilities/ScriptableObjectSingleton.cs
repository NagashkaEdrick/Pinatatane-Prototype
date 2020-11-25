using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane.Utilities
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T instance = null;
        
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    T[] results = Resources.FindObjectsOfTypeAll<T>();
                    if(results.Length == 0)
                    {
                        Debug.LogError("SingletonSO -> Instance -> Result length is 0 for type : " + typeof(T).ToString() + ".");
                        return null;
                    }
                    if(results.Length > 1)
                    {
                        Debug.LogError("SingletonSO -> Instance -> Result length is > 1 for type : " + typeof(T).ToString() + ".");
                        return null;
                    }

                    instance = results[0];
                }
                return instance;
            }
        }
    }
}