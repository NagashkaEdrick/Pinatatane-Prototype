using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane {
    public class Cactus : Trap<CactusData>
    {
        public override void OnEnter(IReceiveDamages objectTrapped)
        {
            if (objectTrapped is IBlockable) BlockObject((IBlockable) objectTrapped, trapData.blockingTime);
        }

        public override void OnExit()
        {
        }
    }
}
