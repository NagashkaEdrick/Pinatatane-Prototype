using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class IsGrabbingController : Condition<PinataController>
    {

        /// <summary>
        /// Return true si le lasso a grab un objet
        /// </summary>
        public override bool CheckCondition(PinataController element)
        {
            return element.Pinata.LassoController.Lasso.CurrenObjectGrabbed != null;
        }
    }
}
