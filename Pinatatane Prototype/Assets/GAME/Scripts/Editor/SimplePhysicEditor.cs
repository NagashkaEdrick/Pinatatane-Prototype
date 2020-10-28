using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimplePhysic))]
public class SimplePhysicEditor : Editor
{
    Transform t;
    Collider c;

    private void OnEnable() {
        t = (target as SimplePhysic).transform;
        c = (target as SimplePhysic).GetComponent<Collider>();
    }

    private void OnSceneGUI() {
        Handles.FreeMoveHandle(t.position, t.rotation, c.bounds.size.y, Vector3.zero, Handles.SphereHandleCap);
    }
}
