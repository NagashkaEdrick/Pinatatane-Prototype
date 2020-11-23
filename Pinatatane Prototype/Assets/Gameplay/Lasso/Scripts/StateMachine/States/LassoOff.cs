using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class LassoOff : State<LassoController>
    {
        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);
            element.Retract();
        }

        public override void OnCurrent(LassoController element)
        {
            Debug.Log("Le joueur n'essaie pas de grab.");
            base.OnCurrent(element);
        }
    }
}