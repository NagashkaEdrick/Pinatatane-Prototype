using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

public class ConditionTestA : Condition<Transform>
{
    public Vector3 v;

    public override bool CheckCondition(Transform element)
    {
        if (element.position == v)
            return true;
        return false;
    }
}
