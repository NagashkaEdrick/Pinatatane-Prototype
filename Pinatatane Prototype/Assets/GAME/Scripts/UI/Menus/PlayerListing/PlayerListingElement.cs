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
        public Pinata pinata;

        public void Build(string _playerName)
        {
            playerName.text = _playerName;
        }

        public void RefreshScore()
        {
            photonView.RPC("RefreshScoreNetwork", RpcTarget.AllBuffered, pinata.Score);
        }

        [PunRPC]
        public void RefreshScoreNetwork(int _score)
        {
            score.text = _score.ToString();
        }
    }
}