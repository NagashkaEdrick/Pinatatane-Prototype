using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pinatatane
{
    public class ScriptablesObjectReferences : MonoBehaviour
    {
        [SerializeField] List<ScriptableObject> so = new List<ScriptableObject>();

#if UNITY_EDITOR

        [Button]
        void FindAllSO()
        {
            ScriptableObject[] s = GetAllInstances<ScriptableObject>();
            for (int i = 0; i < s.Length; i++)
            {
                so.Add(s[i]);
            }
        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;

        }
#endif
    }
}