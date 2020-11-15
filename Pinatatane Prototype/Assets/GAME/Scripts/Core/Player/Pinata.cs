using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using Photon.Pun.UtilityScripts;

namespace Pinatatane
{
    public class Pinata : MonoBehaviourPunCallbacks, IGrabable
    {
        /*
         * Le joueur + ses comportements
         */

        [FoldoutGroup("References", order: 0)]
        public CharacterMovementBehaviour characterMovementBehaviour;
        [FoldoutGroup("References", order: 0)]
        [HideInInspector] public CameraController cameraController;
        [FoldoutGroup("References", order: 0)]
        public AnimatorBehaviour animatorBehaviour;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] private PhotonView photonView = default;
        [FoldoutGroup("References", order: 0)]
        public Transform cameraTarget;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] PinataUI pinataUI;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] DashBehaviour dashBehaviour;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] GrabBehaviour grabBehaviour;

        public PinataOverrideControl pinataOverrideControl;

        [HideInInspector] public Player player;

        [BoxGroup("Player Infos", order: 1)]
        public int ID;

        [BoxGroup("Player Infos", order: 1)]
        public bool isGrabbed = false;
        [BoxGroup("Player Infos", order: 1)]
        public bool canBeGrabbed = true;
        [BoxGroup("Player Infos", order: 1)]
        public bool isReady = false;
        [BoxGroup("Player Infos", order: 1)]
        public bool isStatic = false;
        [BoxGroup("Player Infos", order: 1)]
        [SerializeField] float hp = 5f;
        public float HP
        {
            get => hp;
            set
            {
                if (hp <= 0)
                    Death();
                hp = value;
            }
        }
        [BoxGroup("Player Infos", order: 1)]
        public float velocity;

        public PhotonView PhotonView { get => photonView; set => photonView = value; }
        public bool CanBeGrabbed { get => canBeGrabbed; set => canBeGrabbed = value; }

        #region Publics
        /// <summary>
        /// Inititialisation du player
        /// </summary>
        public void InitPlayer()
        {
            PhotonView.RPC("InitAllPlayer", RpcTarget.All);

            SetID();

            cameraController.target = cameraTarget;
            grabBehaviour._camera = cameraController.GetComponent<Camera>();
            pinataUI?.InitPlayerUI();
            SetPlayerName();
            Respawn();
        }

        /// <summary>
        /// Creation de l'ID du joueur
        /// </summary>
        void SetID()
        {
            player = PhotonView.Owner;
            ID = PhotonView.ViewID;
        }

        private void Update()
        {
            #region Test
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (PhotonView.IsMine)
                {
                    SetPlayerReady();
                }
            }
            #endregion //Input M pour des tests
        }

        public void Respawn() => PartyManager.Instance.SpawnToAFreePoint(transform);


        #region Call Network
        public void SetPlayerName()
        {
            PhotonView.RPC("SetName", RpcTarget.AllBuffered, player.NickName);
        }

        public void SetPlayerReady()
        {
            PhotonView.RPC("SetReady", RpcTarget.All, PhotonView.ViewID, !isReady);
        }

        public void StartGrab(int _cible)
        {
            pinataOverrideControl.photonView.RPC("RPCLoseControl", RpcTarget.All, _cible);
        }

        public void EndGrab(int _cible)
        {
            Debug.Log(_cible);
            pinataOverrideControl.photonView.RPC("RPCWinControl", RpcTarget.All, _cible);
        }

        public void OnGrab(int _cible, int _attaquant)
        {
            PhotonView.RPC("GrabNetwork", RpcTarget.AllBuffered, _cible, _attaquant);
        }

        public void Death()
        {
            photonView.RPC("DeathRPC", RpcTarget.All, photonView.ViewID);
        }
        #endregion

        #endregion

        #region Network

        [PunRPC]
        public void DeathRPC(int _targetID)
        {
            PhotonNetwork.GetPhotonView(_targetID).GetComponent<Pinata>().Death();
        }

        /// <summary>
        /// Ajoute du score au joueur ciblé
        /// </summary>
        /// <param name="_increment"></param>
        /// <param name="_id"></param>
        [PunRPC]
        public void IncrementeScore(int _increment, string _id)
        {
            if (player.UserId == _id)
            {
                player.AddScore(50);
                UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").Refresh();
            }
        }

        /// <summary>
        /// Modifie le nom du joueur au dessus de sa tête
        /// </summary>
        /// <param name="_name"></param>
        [PunRPC]
        public void SetName(string _name)
        {
            pinataUI.playerName.text = _name;
        }


        [PunRPC]
        public void SetReady(int _targetID, bool _state)
        {
            if (PhotonView.ViewID == _targetID)
            {
                isReady = _state;
            }
        }

        #region Grab
        /// <summary>
        /// RPC : Permet de grabber en réseau
        /// </summary>
        /// <param name="_cible"></param>
        /// <param name="_attaquant"></param>
        [PunRPC]
        public void GrabNetwork(int _cible, int _attaquant)
        {
            if (PhotonView.ViewID == _attaquant)
            {
                UIManager.Instance.networkStatutElement.SetText("attaquant");

                NetworkDebugger.Instance.Debug("CIBLE :" + _cible.ToString() + " || ATTAQUANT :" + _attaquant.ToString(), DebugType.NETWORK);

                PhotonView.RPC("ChangePos", RpcTarget.All, _cible, Vector3.zero);

                //PhotonNetwork.GetPhotonView(_cible).transform.localScale *= 2;
                //PhotonNetwork.GetPhotonView(_attaquant).transform.localScale /= 2;
            }
        }

        /// <summary>
        /// RPC : Modifier la position d'un joueur ciblé en réseau 
        /// </summary>
        /// <param name="_targetID"></param>
        /// <param name="_pos"></param>
        [PunRPC]
        public void ChangePos(int _targetID, Vector3 _pos)
        {
            PhotonNetwork.GetPhotonView(_targetID).transform.position = _pos;
            NetworkDebugger.Instance.Debug("targetID : " + _targetID + " POS : " + _pos, DebugType.LOCAL);
        }

        /// <summary>
        /// RPC : Modifier la rotation d'un joueur ciblé en réseau
        /// </summary>
        /// <param name="_targetID"></param>
        /// <param name="_rot"></param>
        [PunRPC]
        public void ChangeRot(int _targetID, Vector3 _rot)
        {
            Quaternion q = Quaternion.Euler(_rot);
            PhotonNetwork.GetPhotonView(_targetID).transform.rotation = q;
        }

        /// <summary>
        /// Set un hote pour tout les joueurs (Le premier connecté à la room)
        /// </summary>
        /// <param name="_hostID"></param>
        [PunRPC]
        public void SetHostForAll(int _hostID)
        {
            PlayerManager.Instance.hostID = _hostID;
            NetworkDebugger.Instance?.Debug("Host ID = " + _hostID, DebugType.LOCAL);
        }

        #endregion

        [PunRPC]
        public void InitAllPlayer()
        {
            player = PhotonView.Owner;

            var obj = FindObjectsOfType<Pinata>();
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].SetID();
                obj[i].pinataUI.playerName.SetText(obj[i].ID.ToString());
            }

            PhotonNetwork.NickName = "Guest" + Random.Range(0, 999).ToString();
            Debug.Log("Init Player -> " + player.NickName);
        }
        #endregion
    }
}