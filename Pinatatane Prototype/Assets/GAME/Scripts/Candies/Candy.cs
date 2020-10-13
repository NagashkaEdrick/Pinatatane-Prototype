using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class Candy : MonoBehaviour
    {
        public PhotonView photonView;
        [SerializeField] int candyValue = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Pinata>())
            {
                Debug.Log("touch");
                other.gameObject.GetComponent<Pinata>().IncrementeScore(candyValue);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}