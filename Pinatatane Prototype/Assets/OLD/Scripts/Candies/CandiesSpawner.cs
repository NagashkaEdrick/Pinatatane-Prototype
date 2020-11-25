using Photon.Pun;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    public class CandiesSpawner : MonoBehaviour
    {
        public PhotonView photonView;

        [Button]
        public void SpawnCandy()
        {
            PhotonNetwork.Instantiate("Candy", transform.position, Quaternion.identity);
        }
    }
}