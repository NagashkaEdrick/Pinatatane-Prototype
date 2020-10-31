using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public abstract class MonoBehaviourCondition : MonoBehaviour
    {
        public abstract bool IsValidate();
    }
}