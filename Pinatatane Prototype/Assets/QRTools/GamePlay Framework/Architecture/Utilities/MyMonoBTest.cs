using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
public class MyMonoBTest : MyMonoBehaviour
{
    public float a, b, c;

    protected override void OnGameStart()
    {
        Debug.Log("start");
    }

    protected override void GameBreakEnter()
    {
        Debug.Log("enter");
    }

    protected override void GameBreakExit()
    {
        Debug.Log("exit");
    }

    protected override void OnGameEnd()
    {
        Debug.Log("End");
    }
}
