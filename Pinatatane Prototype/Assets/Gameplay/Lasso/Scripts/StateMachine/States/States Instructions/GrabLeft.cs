using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class GrabLeft : State<LassoController>
    {
        public override void OnCurrent(LassoController element)
        {
            base.OnCurrent(element);

            Debug.Log("Grab left");
        }
    }
}