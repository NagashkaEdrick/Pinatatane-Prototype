using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

public class StateMachineTest : StateMachine<Transform>
{
    private void Awake()
    {
        StartStateMachine(null, transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
            CheckCurrentState(transform);

    }
}
