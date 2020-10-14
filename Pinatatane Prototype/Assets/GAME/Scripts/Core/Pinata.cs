using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class Pinata : MonoBehaviour
    {
        [FoldoutGroup("References", order: 0)]
        public CharacterMovementBehaviour characterMovementBehaviour;
        [FoldoutGroup("References", order: 0)]
        public CameraController cameraController;
        [FoldoutGroup("References", order: 0)]
        public AnimatorBehaviour animatorBehaviour;
        [FoldoutGroup("References", order: 0)]
        public PhotonView photonView;
        [FoldoutGroup("References", order: 0)]
        public Transform cameraTarget;
        [FoldoutGroup("References", order: 0)]
        public UIElement[] playerUIelements;
        [FoldoutGroup("References", order: 0)]
        public TextMeshProUGUI playerName;

        [TitleGroup("Gameplay Value", Order = 10)]
        [SerializeField] int score;

        public PlayerListingElement playerListingElement;

        public Player player => PhotonNetwork.LocalPlayer;

        #region Properties
        public int Score
        {
            get => score;
            set
            {
                score = value;                
            }
        }
        #endregion

        #region Publics
        public void InitPlayer()
        {
            Debug.Log("Init Player -> " + player.NickName);
            cameraController.target = cameraTarget;

            InitPlayerUI();
            UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").AddPlayer(player);

            Score = 0;
        }

        [PunRPC]
        public void IncrementeScore(int _increment, string _id)
        {
            if(player.UserId == _id)
                Score += _increment;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                photonView.RPC("IncrementeScore", RpcTarget.OthersBuffered, 50, player.UserId);
        }

        public void SetPlayerName()
        {
            photonView.RPC("SetName", RpcTarget.AllBuffered, player.NickName);
        }
        #endregion

        #region Network
        [PunRPC]
        public void SetName(string _name)
        {
            playerName.text = _name;
        }
        #endregion

        #region Private
        void InitPlayerUI()
        {
            for (int i = 0; i < playerUIelements.Length; i++)
                playerUIelements[i].Refresh();
        }
        #endregion
    }
}