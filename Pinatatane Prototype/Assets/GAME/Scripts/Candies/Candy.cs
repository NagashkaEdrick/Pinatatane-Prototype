using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class Candy : MonoBehaviour
    {
        public PhotonView photonView;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Pinata>())
            {
                Debug.Log("touch");
                other.gameObject.GetComponent<Pinata>().IncrementeScore();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}