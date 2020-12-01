using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using DG.Tweening;

namespace Pinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Gameplay/CandyData")]
    public class CandyData : SerializedScriptableObject
    {
        [SerializeField] float spawnRangeRdn = 2f;

        public float SpawnRangeRdn
        {
            get => Random.Range(0, spawnRangeRdn);
            set => spawnRangeRdn = value;
        }

        public Ease fallingEasing = Ease.OutBounce;

        public float
            touchRadius = .2f,
            attireRadius = 2f,
            attireSpeed = .5f;
    }
}