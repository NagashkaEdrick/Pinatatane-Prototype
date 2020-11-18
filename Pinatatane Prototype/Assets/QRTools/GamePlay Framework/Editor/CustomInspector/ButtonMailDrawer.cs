using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameplayFramework;
using System;
using System.Text.RegularExpressions;

namespace GameplayFramework.Editor
{
    [CustomPropertyDrawer(typeof(ButtonMail))]
    public class ButtonMailDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(GUILayout.Button("Notify a bug by mail"))
            {
                Notify(property);
            }
        }


        public void Notify(SerializedProperty property)
        {
            MailWindow mw = MailWindow.Open();
            mw.Object = "Bug sur le script : " + property.serializedObject.targetObject.GetType();
            mw.Message = "Liste : \n";
        }
    }
}