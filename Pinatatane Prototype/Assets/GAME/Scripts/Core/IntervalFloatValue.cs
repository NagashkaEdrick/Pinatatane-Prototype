using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Interval Float Variable", fileName = "Interval Float Variable")]
public class IntervalFloatValue : ScriptableObject
{
    [Range(0f, 1f)]
    public float value = 0f;
}
