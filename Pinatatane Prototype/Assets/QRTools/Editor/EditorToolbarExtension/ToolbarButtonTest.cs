using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;

[InitializeOnLoad]
public class ToolbarButtonTest
{
	static ToolbarButtonTest()
	{
		ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
	}

	static void OnToolbarGUI()
	{
		GUILayout.FlexibleSpace();

		if (GUILayout.Button(new GUIContent("1", "1")))
		{
			Debug.Log("Button 1");
		}

		if (GUILayout.Button(new GUIContent("2", "2")))
		{
			Debug.Log("Button 1");
		}
	}
}
