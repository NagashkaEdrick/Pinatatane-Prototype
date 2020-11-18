using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QRTools.Editor;

namespace GameplayFramework.Editor
{
    public class WindowTemplateWithToolBar : WindowTemplate<WindowTemplateWithToolBar>
    {
        protected SelectableButtons toolbarButtons;

        [MenuItem("Tools/QRTools/DefaultWithToolbar")]
        static void OpenWindow()
        {
            Open();
        }

        public override void Init()
        {
            base.Init();
            toolbarButtons = new SelectableButtons(true);

            Texture2D svicon = Resources.Load("EditorIcons/SaveIcon") as Texture2D;
            toolbarButtons.AddBtn(svicon, "Save", delegate { Debug.Log("coucou"); });
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(Screen.width), GUILayout.ExpandHeight(true));
            TitleContent();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
            RightMenuContent();

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true), GUILayout.MinHeight(750));
            BodyContent();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        public override void BodyContent()
        {
            base.BodyContent();
            ToolbarContent();
        }

        protected void ToolbarContent()
        {

            EditorGUILayout.BeginHorizontal("box", GUILayout.MinHeight(45));

            toolbarButtons.Show();

            EditorGUILayout.EndHorizontal();
        }
    }
}