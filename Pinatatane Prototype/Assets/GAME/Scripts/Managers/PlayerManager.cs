using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

using CharacterController = Pinatatane.CharacterController;

namespace Pinatatane
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public CameraController camPrefab = default;

        [SerializeField] Transform playerParent;

        [SerializeField] Pinata localPlayer;
        public Pinata LocalPlayer
        {
            get => localPlayer;
            private set => localPlayer = value;
        }

        private void Awake()
        {
            Instance = this;
        }

        public void CreatePlayer()
        {
            GameObject newPlayerGO = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            newPlayerGO.transform.parent = playerParent;

            Pinata player = newPlayerGO.GetComponent<Pinata>();

            CameraController c = Instantiate(camPrefab, transform.position, Quaternion.identity);
            player.cameraController = c;

            localPlayer = player;
            player.InitPlayer();
        }
    }
}