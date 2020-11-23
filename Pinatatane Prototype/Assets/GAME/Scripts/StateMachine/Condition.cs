using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool TestCondition();
    }
}