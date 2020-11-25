using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class NoInstructionCondition : Condition<LassoController>
    {
        bool timeIsOk = false;
        Coroutine t;

        public override bool CheckCondition(LassoController element)
        {
            return timeIsOk;
        }

        public void LaunchCondition(LassoController element)
        {
            timeIsOk = false;
            if (t != null)
                StopCoroutine(t);

            t = StartCoroutine(Timer(element));
        }

        IEnumerator Timer(LassoController element)
        {
            yield return new WaitForSeconds(element.Lasso.LassoData.timeWithNoInstructionAccorded);
            timeIsOk = true;
            yield break;
        }
    }
}