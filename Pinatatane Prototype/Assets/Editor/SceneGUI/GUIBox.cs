using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using GameplayFramework.Network;

namespace Pinatatane
{
    [CustomEditor(typeof(GUIOptions))]
    public class GUIBox : Editor
    {
        const float
            maxH = 80,
            maxV = 20;

        private void OnSceneGUI()
        {
            GUIStyle style = new GUIStyle("box");
            Rect r = new Rect(0, 0, 150, 500);
            GUILayout.BeginArea(r, style);
            EditorGUILayout.BeginVertical("box");
            {
                NetworkManager nm = FindObjectOfType<NetworkManager>();
                nm.UseNetwork = EditorGUILayout.ToggleLeft("Use Network", nm.UseNetwork);
            }
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}