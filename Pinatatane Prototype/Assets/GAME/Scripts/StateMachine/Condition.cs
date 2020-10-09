using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool TestCondition();
    }
}