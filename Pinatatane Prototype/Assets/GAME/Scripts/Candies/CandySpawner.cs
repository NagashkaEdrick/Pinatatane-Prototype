using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class CandySpawner : MonoBehaviour
    {
        [SerializeField] float timer;


        private void Awake()
        {
            StartCoroutine(SpawnCandies());
        }

        IEnumerator SpawnCandies()
        {
            yield return new WaitForSeconds(timer);
            CandiesBatch.Instance.Pool(5, transform.position);
            StartCoroutine(SpawnCandies());
            yield break;
        }
    }
}