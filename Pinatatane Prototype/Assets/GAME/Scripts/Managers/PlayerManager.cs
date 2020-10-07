using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

using UnityEngine;

using Gameplay;

namespace Pinatatane
{
    public class PlayerManager : MonoBehaviour
    {
        public CameraController camPrefab = default;

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

            Gameplay.CharacterController cc = go.GetComponent<Gameplay.CharacterController>();
            CameraController c = Instantiate(camPrefab, transform.position, Quaternion.identity);

            c.target = go.transform;
            cc._camera = c.transform;
        }
    }
}