using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// Cette state gère le lasso quand il est inutilisé
    /// </summary>
    public class LassoOff : State<LassoController>
    {
        public override void OnEnter(LassoController element)
        {
            base.OnEnter(element);
            element.Retract();
            element.Lasso.LassoGraphics.LassoDebug(Color.white);
        }
    }
}