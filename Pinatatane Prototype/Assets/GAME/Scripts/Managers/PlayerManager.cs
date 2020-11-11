using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Cinemachine;

namespace Pinatatane
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        /*
         * Créé le joueur local
         * Gère aussi les autres joueurs si tu es l'hôte
         * Sa création
         */

        [BoxGroup("References", order: 1)] public static PlayerManager Instance;
        [BoxGroup("References", order: 1)] public GameObject camerasPrefab = default;
        [BoxGroup("References", order: 1), SerializeField] Transform playerParent;

        [SerializeField][ReadOnly, BoxGroup("Infos", order: 5)] Pinata localPlayer;
        public Pinata LocalPlayer
        {
            get => localPlayer;
            private set => localPlayer = value;
        }

        [SerializeField, ReadOnly, BoxGroup("Infos", order: 5)] public List<Pinata> pinatas = new List<Pinata>();

        [SerializeField, ReadOnly, BoxGroup("Infos", order: 5)] public int hostID;

        private void Awake()
        {
            Instance = this;
        }

        public void CreatePlayer()
        {
            GameObject newPlayerGO = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            newPlayerGO.transform.parent = playerParent;

            Pinata player = newPlayerGO.GetComponent<Pinata>();

            GameObject cameras = Instantiate(camerasPrefab, transform.position, Quaternion.identity);
            player.mainCamera = cameras.transform.GetChild(0).GetComponent<Camera>();

            CinemachineFreeLook freeLook = cameras.transform.GetChild(1).GetComponent<CinemachineFreeLook>();
            freeLook.m_Follow = player.transform;
            freeLook.m_LookAt = player.transform;

            Transform aimCamera = cameras.transform.GetChild(2);
            aimCamera.GetComponent<CinemachineVirtualCamera>().m_LookAt = player.transform;
            aimCamera.GetComponent<AimCameraBehaviour>().target = player.transform;

            LocalPlayer = player;
            player.InitPlayer();

            StartCoroutine(Gestion());
        }

        IEnumerator Gestion()
        {
            yield return new WaitForSeconds(.05f);
            FindAllPinata();
            SetHost();
            PartyManager.Instance.OnJoinGame();
            yield break;
        }

        public void FindAllPinata()
        {
            pinatas.Clear();
            for (int i = 0; i < PhotonNetwork.PhotonViews.Length; i++)
            {
                if (PhotonNetwork.PhotonViews[i].GetComponent<Pinata>())
                    pinatas.Add(PhotonNetwork.PhotonViews[i].GetComponent<Pinata>());
            }
        }

        void SetHost()
        {
            if(pinatas.Count <= 1)
            {
                hostID = LocalPlayer.PhotonView.ViewID;
                LocalPlayer.PhotonView.RPC("SetHostForAll", RpcTarget.AllBuffered, hostID);
            }
        }        

        public Pinata FindPinata(int viewID)
        {
            for (int i = 0; i < PhotonNetwork.PhotonViews.Length; i++)
            {
                if (PhotonNetwork.PhotonViews[i].ViewID == viewID)
                    return PhotonNetwork.PhotonViews[i].GetComponent<Pinata>();
            }

            return null;
        }

        public bool IsHosting()
        {
            //Commentaire

            if (LocalPlayer.PhotonView.ViewID == hostID)
                return true;
            else
                return false;
        }
    }
}