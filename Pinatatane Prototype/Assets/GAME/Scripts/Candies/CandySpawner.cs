using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Pinatatane
{
    public class CandySpawner : MonoBehaviour
    {
        [SerializeField] float timer;


        //private void Awake()
        //{
        //    StartCoroutine(SpawnCandies()); // Quand la partie se lance...
        //}

        //IEnumerator SpawnCandies()
        //{            
        //    yield return new WaitForSeconds(timer);
        //    if (PlayerManager.Instance.IsHosting() && PhotonNetwork.IsConnected)
        //        CandiesBatch.Instance.Pool(5, transform.position);
        //    else
        //        yield break;
        //    StartCoroutine(SpawnCandies());
        //    yield break;
        //}
    }
}