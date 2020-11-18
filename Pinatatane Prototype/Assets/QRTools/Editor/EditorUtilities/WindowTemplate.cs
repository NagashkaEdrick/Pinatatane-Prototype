using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using QRTools.Editor;

namespace GameplayFramework.Editor {
    public class WindowTemplate<T> : EditorWindow where T : EditorWindow
    {
        protected static T window;

        protected SelectableButtons btns;

        [MenuItem("Tools/QRTools/Default")]
        public static T Open()
        {
            window = (T)GetWindow(typeof(T));
            window.Show();
            window.minSize = new Vector2(1250f, 750f);
            window.maxSize = new Vector2(1250f, 750f);
            return window;
        }

        private void OnEnable()
        {
            Init();
        }

        public virtual void Init()
        {
            btns = new SelectableButtons();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(Screen.width), GUILayout.ExpandHeight(true));
            TitleContent();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
            RightMenuContent();

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            BodyContent();

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        public virtual void TitleContent()
        {
            EditorGUILayout.LabelField("TITLE");
        }

        public virtual void RightMenuContent()
        {
            EditorGUILayout.LabelField("MENU");
            EditorFunctions.Separator();
            ShowBtn();
        }

        public virtual void BodyContent()
        {
            EditorGUILayout.LabelField("CONTENT");
            EditorFunctions.Separator();
        }

        void ShowBtn()
        {
            btns.Show();
        }
    }
}