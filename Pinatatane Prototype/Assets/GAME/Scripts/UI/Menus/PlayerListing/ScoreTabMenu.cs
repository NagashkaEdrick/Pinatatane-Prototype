using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class ScoreTabMenu : UIMenu
    {
        public PhotonView photonView;
        public PlayerListingElement PlayerListingElementPrefab = default;
        [SerializeField] RectTransform container = default;

        public List<PlayerListingElement> playerListingElements = new List<PlayerListingElement>();

        public void AddPlayer(string _playerName, Pinata _pinata)
        {
            photonView.RPC("AddPlayerNetworking", RpcTarget.AllBuffered, _playerName, _pinata);
        }

        [PunRPC]
        public void AddPlayerNetworking(string _playerName, Pinata _pinata)
        {
            PlayerListingElement newListingElement = Instantiate(PlayerListingElementPrefab, container);
            newListingElement.pinata = _pinata;
            newListingElement.Build(_playerName);
        }

        public void RemovePlayer()
        {

        }

        public void Restore()
        {

        }

        public override void Refresh()
        {
            base.Refresh();
        }
    }
}