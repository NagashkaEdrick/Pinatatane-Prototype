using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using DG.Tweening;

using UnityEngine;
using TMPro;

namespace Pinatatane
{
    public class Pinata : MonoBehaviour
    {
        public CharacterMovementBehaviour characterMovementBehaviour;
        public CameraController cameraController;
        public AnimatorBehaviour animatorBehaviour;

        public PhotonView photonView;

        public Transform cameraTarget;

        public int score;

        public UIElement[] playerUIelements;

        public TextMeshProUGUI playerName;

        public void InitPlayer()
        {
            Debug.Log("Init Player -> " + PhotonNetwork.LocalPlayer.NickName);

            cameraController.target = cameraTarget;

            InitPlayerUI();
        }

        private void Update()
        {
            //Test
            if (Input.GetKeyDown(KeyCode.K))
            {
                photonView.RPC("TranslatePos", RpcTarget.Others, Vector3.zero);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                photonView.RPC("ChangeScore", RpcTarget.Others, score += 10);
            }
        }

        public void InitPlayerUI()
        {
            for (int i = 0; i < playerUIelements.Length; i++)
                playerUIelements[i].Refresh();
        }

        public void SetPlayerName()
        {
            photonView.RPC("SetName", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName);

        }

        [PunRPC]
        public void SetName(string _name)
        {
            playerName.text = _name;
        }

        [PunRPC]
        public void TranslatePos(Vector3 _newPos)
        {
            transform.position = _newPos;
        }

        [PunRPC]
        public void ChangeScore(int score)
        {
            UIManager.Instance.currentScore.text = score.ToString();
        }
    }
}