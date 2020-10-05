using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

using UnityEngine;

namespace Pinatatane
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [SerializeField] Transform playerParent;

        private void Awake()
        {
            Instance = this;
        }

        public void CreatePlayer()
        {
            GameObject go = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            go.transform.parent = playerParent;
        }
    }
}