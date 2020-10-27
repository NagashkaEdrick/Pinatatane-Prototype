using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pinatatane
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        /*
         * Créé le joueur local
         * Gère aussi les autres joueurs si tu es l'hôte
         * Sa création
         */

        public static PlayerManager Instance;

        public CameraController camPrefab = default;

        [SerializeField] Transform playerParent;

        [SerializeField] Pinata localPlayer;
        public Pinata LocalPlayer
        {
            get => localPlayer;
            private set => localPlayer = value;
        }

        public List<Pinata> pinatas = new List<Pinata>();

        private void Awake()
        {
            Instance = this;
        }

        public void CreatePlayer()
        {
            GameObject newPlayerGO = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            newPlayerGO.transform.parent = playerParent;

            Pinata player = newPlayerGO.GetComponent<Pinata>();

            CameraController _camController = Instantiate(camPrefab, transform.position, Quaternion.identity);
            player.cameraController = _camController;

            LocalPlayer = player;
            player.InitPlayer();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            
        }

        public override void OnJoinedRoom()
        {
            
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            
        }
    }
}