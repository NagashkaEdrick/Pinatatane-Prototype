using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// Cette state attend une instruction du joueur (haut bas droite ou gauche)
    /// </summary>
    public class WaitLassoInstruction : State<LassoController>
    {
        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);
            if(element.debugMode) Debug.Log("<color=yellow>Lasso:</color> Attente d'une instruction...");
        }

        public override void OnCurrent(LassoController element)
        {
            base.OnCurrent(element);
        }
    }
}