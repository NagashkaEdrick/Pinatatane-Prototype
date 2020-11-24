using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class Pinata : Pawn
    {        
        public PinataData PinataData = default;

        public LassoController LassoController;

        [BoxGroup("A bouger ailleurs")]
        public Camera MainCamera;
        [BoxGroup("A bouger ailleurs")]
        public Image CrossHair;

        public override void OnStart()
        {
            base.OnStart();
            Controller.RegisterPawn(this);
        }
    }
}
