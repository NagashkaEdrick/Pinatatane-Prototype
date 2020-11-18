using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Editor
{
    public class SelectableButtons
    {
        public List<SelectableButtonGUI> buttons = new List<SelectableButtonGUI>();
        public bool HorizontalMode { get; set; } = false;

        public SelectableButtons(bool _horizontalMode = false)
        {
            HorizontalMode = _horizontalMode;
        }

        /// <summary>
        /// Add a button to the selectable buttons
        /// </summary>
        /// <param name="btnLabel"></param>
        /// <param name="btnEvent"></param>
        /// <returns></returns>
        public SelectableButtonGUI AddBtn(string btnLabel, ButtonEvent btnEvent)
        {
            SelectableButtonGUI btn = new SelectableButtonGUI();
            btn.btnLabel = btnLabel;
            btn.parent = this;
            btn.onClick += btnEvent;
            buttons.Add(btn);

            return btn;
        }

        public SelectableButtonGUI AddBtn(Texture2D icon, string tooltip, ButtonEvent btnEvent, float size = 44)
        {
            SelectableButtonGUI btn = new SelectableButtonGUI(icon, tooltip, size);
            btn.parent = this;
            btn.onClick += btnEvent;
            buttons.Add(btn);

            return btn;
        }

        /// <summary>
        /// Draw all buttons in GUI
        /// </summary>
        public void Show()
        {
            if (HorizontalMode)
                EditorGUILayout.BeginHorizontal();
            else
                EditorGUILayout.BeginVertical();

            if (buttons.Count > 0)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Show();
                }
            }

            if (HorizontalMode)
                EditorGUILayout.EndHorizontal();
            else
                EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Select a button and deselect other buttons
        /// </summary>
        /// <param name="btn"></param>
        public void SelectButton(SelectableButtonGUI btn)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] == btn)
                {
                    buttons[i].IsSelected = true;
                }
                else
                    buttons[i].IsSelected = false;
            }
        }
    }

    public delegate void ButtonEvent();
    public class SelectableButtonGUI
    {
        public bool IsSelected { get; set; } = false;
        public ButtonEvent onClick;
        public string btnLabel = "";

        public SelectableButtons parent;

        public Texture2D icon;
        public string tooltip = "";
        public float size;

        public SelectableButtonGUI() { }

        public SelectableButtonGUI(Texture2D _icon, string _toolTip, float _size = 44)
        {
            icon = _icon;
            tooltip = _toolTip;
            size = _size;
        }

        /// <summary>
        /// Draw button in GUI
        /// </summary>
        public void Show()
        {
            Color baseColor = GUI.color;
            if (IsSelected)
                GUI.color = baseColor;
            else GUI.color = new Color(.75f, .75f, .75f, 1f);

            if (icon != null)
            {
                ShowWithIcon(icon, tooltip, size);
            }
            else
            {
                if (GUILayout.Button(btnLabel))
                {
                    onClick?.Invoke();
                    parent.SelectButton(this);
                }
            }

            GUI.color = baseColor;
        }


        public void ShowWithIcon(Texture2D icon, string toolTip, float size)
        {
            if (GUILayout.Button(
                new GUIContent(icon, toolTip), 
                GUILayout.MaxHeight(size), 
                GUILayout.MinHeight(size), 
                GUILayout.MaxWidth(size), 
                GUILayout.MinWidth(size)))
            {
                onClick?.Invoke();
                parent.SelectButton(this);
            }
        }
    }
}