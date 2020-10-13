using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pinatatane
{
    public class PlayerListingElement : UIElement
    {
        [SerializeField] PhotonView photonView;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] TextMeshProUGUI score;

        public void Build(string _playerName)
        {
            playerName.text = _playerName;
        }

        public void RefreshScore(int _score)
        {
            photonView.RPC("RefreshScoreNetwork", RpcTarget.AllBuffered, 15);
        }

        [PunRPC]
        public void RefreshScoreNetwork(int _score)
        {
            score.text = _score.ToString();
        }
    }
}