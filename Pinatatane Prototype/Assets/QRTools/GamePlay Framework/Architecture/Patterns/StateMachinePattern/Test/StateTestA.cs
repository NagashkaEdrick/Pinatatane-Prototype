using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

public class StateTestA : State<Transform>
{
    public int n;

    public override void OnCurrent(Transform element)
    {
        Debug.Log("On Current : " + n);
    }

    public override void OnEnter(Transform element)
    {
        Debug.Log("On Enter : " + n);
    }

    public override void OnExit(Transform element)
    {
        Debug.Log("On Exit : " + n);
    }
}
