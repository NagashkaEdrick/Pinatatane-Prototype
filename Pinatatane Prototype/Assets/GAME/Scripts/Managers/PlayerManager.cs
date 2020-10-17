using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pinatatane
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public CameraController camPrefab = default;

        [SerializeField] Transform playerParent;

        public List<Pinata> pinatas = new List<Pinata>();

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

        [Button]
        public void FindAllPinatas()
        {
            pinatas.Clear();
            Pinata[] _pinatas = FindObjectsOfType<Pinata>();

            for (int i = 0; i < _pinatas.Length; i++)
                pinatas.Add(_pinatas[i]);
        }

        public Pinata GetPinata(string _playerID)
        {
            for (int i = 0; i < pinatas.Count; i++)
            {
                if(pinatas[i].ID == _playerID)
                    return pinatas[i];
            }

            return null;
        }

        [Button]
        void DebugAllPlayers()
        {
            for (int i = 0; i < pinatas.Count; i++)
            {
                Debug.Log(pinatas[i].ID);
            }
        }
    }
}