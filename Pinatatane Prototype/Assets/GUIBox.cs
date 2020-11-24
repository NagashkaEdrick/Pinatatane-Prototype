using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace GameplayFramework.Editor
{
    public class GUIBox : UnityEditor.Editor
    {
        const float
            maxH = 80,
            maxV = 20;

        private void OnSceneGUI()
        {
            GUI.Box(new Rect(0, 0, maxH, maxV), "Settings");
        }
    }
}