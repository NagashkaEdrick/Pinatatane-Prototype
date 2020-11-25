using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    public class QInputMouse : QInputs
    {
        [BoxGroup("Events", order: 50)]


        public override void TestInput()
        {
            if (!IsActive)
                return;


        }

        public override float TestAxis()
        {
            return 0;
        }
    }
}