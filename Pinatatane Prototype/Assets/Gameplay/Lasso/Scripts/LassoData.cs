using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Gameplay/Lasso Data", fileName = "Lasso Data")]
    public class LassoData : SerializedScriptableObject
    {
        [BoxGroup("Construction Lasso")]
        public float 
            constructionTime,
            constructionDistance;

        [BoxGroup("Gameplay Datas")]
        public float timeWithNoInstructionAccorded = 2f;
        [BoxGroup("Gameplay Datas"), Range(0,10)]
        public float grabBackForce = 5;
    }
}