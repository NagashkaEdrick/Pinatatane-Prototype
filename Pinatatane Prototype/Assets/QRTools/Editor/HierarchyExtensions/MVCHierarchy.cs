using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace GameplayFramework.Editor
{
    [InitializeOnLoad]
    public static class MVCHierarchy
    {
        [MenuItem("GameObject/GameplayFramework/MVC Object", false, 10)]
        public static void AddMVC()
        {
            AddObjectWithChild("MVC Object", "Model", "View", "Controller");
        }

        [MenuItem("GameObject/GameplayFramework/State Machine", false, 10)]
        public static void AddStateMachine()
        {
            AddObjectWithChild("State Machine", "States", "Conditions", "Transitions");
        }

        public static void AddObjectWithChild(string rootName, params string[] childs)
        {
            GameObject root = new GameObject(rootName);

            for (int i = 0; i < childs.Length; i++)
            {
                GameObject child = new GameObject(childs[i]);
                child.transform.parent = root.transform;
            }
        }
    }
}