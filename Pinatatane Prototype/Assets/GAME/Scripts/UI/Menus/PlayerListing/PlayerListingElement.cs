using Photon.Pun;
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
        [SerializeField] PhotonView photonView;
        public TextMeshProUGUI playerNameText;
        [SerializeField] TextMeshProUGUI scoreText;

        Player player;

        public void Build(string _playerName)
        {
            playerNameText.text = _playerName;
            RefreshScore(0);
        }

        public void RefreshScore(int _score)
        {
            photonView.RPC("RefreshScoreNetwork", RpcTarget.AllBuffered, _score);
        }

        [PunRPC]
        public void RefreshScoreNetwork(int _score)
        {
            scoreText.text = _score.ToString();
        }
    }
}