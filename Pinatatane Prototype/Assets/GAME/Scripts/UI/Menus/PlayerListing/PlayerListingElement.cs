using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pinatatane
{
    public class PlayerListingElement : UIElement
    {
        public TextMeshProUGUI playerNameText;
        public TextMeshProUGUI scoreText;

        public string playerIDref;

        public void Build(string _playerID)
        {
            playerIDref = _playerID;

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if(PhotonNetwork.PlayerList[i].UserId == playerIDref)
                {
                    playerNameText.text = PhotonNetwork.PlayerList[i].NickName;
                    scoreText.text = (PhotonNetwork.PlayerList[i].GetScore()).ToString();

                    if (PhotonNetwork.LocalPlayer.UserId == _playerID)
                        playerNameText.color = Color.red;
                }
            } 
        }

        public void RefreshScoreNetwork()
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i].UserId == playerIDref)
                {
                    playerNameText.text = PhotonNetwork.PlayerList[i].NickName;
                    scoreText.text = (PhotonNetwork.PlayerList[i].GetScore()).ToString();
                }
            }
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}