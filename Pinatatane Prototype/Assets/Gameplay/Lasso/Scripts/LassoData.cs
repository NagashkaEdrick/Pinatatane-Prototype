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

        [BoxGroup("Construction Lasso (Avec Maillon)")]
        public int numberOfLink = 10;
    }
}