using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class Pinata : Pawn
    {        
        public PinataData PinataData = default;

        public LassoController LassoController;

        public override void OnStart()
        {
            base.OnStart();
            Controller.RegisterPawn(this);
        }
    }
}
