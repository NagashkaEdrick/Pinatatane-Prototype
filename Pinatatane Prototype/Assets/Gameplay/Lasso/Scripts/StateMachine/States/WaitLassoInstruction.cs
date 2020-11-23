using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class WaitLassoInstruction : State<LassoController>
    {
        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);
            Debug.Log("Wait Lasoo Instruction");
        }
    }
}