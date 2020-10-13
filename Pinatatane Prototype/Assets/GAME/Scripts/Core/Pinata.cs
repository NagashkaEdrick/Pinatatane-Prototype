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
            Debug.Log("Init Player -> " + PhotonNetwork.LocalPlayer.NickName);
            cameraController.target = cameraTarget;

            InitPlayerUI();
            UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").AddPlayer(PhotonNetwork.LocalPlayer.NickName, this);
        }

        public void IncrementeScore(int _increment)
        {
            photonView.RPC("ChangeScore", RpcTarget.Others, Score += _increment);
        }

        public void SetPlayerName()
        {
            photonView.RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);
        }
        #endregion

        #region Network
        [PunRPC]
        public void SetName(string _name)
        {
            playerName.text = _name;
        }

        [PunRPC]
        public void ChangeScore(int _score)
        {
            UIManager.Instance.currentScore.text = _score.ToString();
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