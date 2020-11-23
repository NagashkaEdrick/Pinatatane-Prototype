using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEditor;

namespace QRTools.Editor
{
    public static class EditorFunctions
    {       

        public static Texture2D GetSimpleTexture(Color _c)
        {
            Texture2D t = new Texture2D(1, 1);
            t.SetPixel(0, 0, _c);
            t.Apply();
            return t;
        }

        public static void Separator()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
    }
}