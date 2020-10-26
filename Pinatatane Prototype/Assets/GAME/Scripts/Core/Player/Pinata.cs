using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using Photon.Pun.UtilityScripts;
using System.Runtime.CompilerServices;

namespace Pinatatane
{
    public class Pinata : MonoBehaviour
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
        public PhotonView photonView;
        [FoldoutGroup("References", order: 0)]
        public Transform cameraTarget;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] PinataUI pinataUI;

        [HideInInspector] public Player player;

        [BoxGroup("Player Infos", order: 1)]
        public string ID;
        [BoxGroup("Player Infos", order: 1)]
        public bool isGrabbed = false;

        #region Publics
        public void InitPlayer()
        {
            PhotonNetwork.NickName = "Guest" + Random.Range(0, 999).ToString();
            player = PhotonNetwork.LocalPlayer;
            Debug.Log("Init Player -> " + player.NickName);

            ID = player.UserId;

            cameraController.target = cameraTarget;
            pinataUI?.InitPlayerUI();
            SetPlayerName();
        }

        private void Update()
        {
            #region Test
            if (Input.GetKeyDown(KeyCode.M))
            {
                photonView.RPC("IncrementeScore", RpcTarget.AllBuffered, 50, ID);
            }
            #endregion //Input M pour des tests
        }


        #region Call Network

        public void SetPlayerName()
        {
            photonView.RPC("SetName", RpcTarget.AllBuffered, player.NickName);
        }

        public void SetPlayerReady()
        {
            photonView.RPC("SetReady", RpcTarget.AllBuffered, player.NickName);
        }

        public void Grab(string _cible, string _attaquant)
        {
            photonView.RPC("GrabNetwork", RpcTarget.All, _cible, _attaquant);
            Debug.Log(string.Format("GRAB => cible = {0} | attaquant = {1}", _cible, _attaquant));
        }
        #endregion

        #endregion

        #region Network

        [PunRPC]
        public void IncrementeScore(int _increment, string _id)
        {
            if (ID == _id)
            {
                player.AddScore(50);
                UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").Refresh();
            }
        }

        [PunRPC]
        public void SetName(string _name)
        {
            pinataUI.playerName.text = _name;
        }

        [PunRPC]
        public void SetReady(string _playerID)
        {

        }        

        [PunRPC]
        public void GrabNetwork(string _cible, string _attaquant)
        {
            if(ID == _cible)
            {
                //comportement de la cible
            }
            else if(ID == _attaquant)
            {
                //comportement de l'attaquant
            }
        }
        #endregion
    }
}