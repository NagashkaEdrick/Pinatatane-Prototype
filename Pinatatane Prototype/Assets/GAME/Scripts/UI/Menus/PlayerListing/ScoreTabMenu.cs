using Photon.Pun;
using Photon.Realtime;
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

        public void AddPlayer(string _playerID)
        {
            photonView.RPC("AddPlayerNetworking", RpcTarget.AllBuffered, _playerID);
        }

        [PunRPC]
        public void AddPlayerNetworking(string _playerID)
        {
            GameObject go = PhotonNetwork.Instantiate("PlayerListingElementPrefab", transform.position, Quaternion.identity);
            go.transform.parent = container;

            PlayerListingElement newListingElement = go.GetComponent<PlayerListingElement>(); /*Instantiate(PlayerListingElementPrefab, container);*/
            playerListingElements.Add(newListingElement);

            newListingElement.Build(_playerID);
        }

        public override void Refresh()
        {
            base.Refresh();

            photonView.RPC("Rfr", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void Rfr()
        {
            for (int i = 0; i < playerListingElements.Count; i++)
            {
                playerListingElements[i].RefreshScoreNetwork();
            }
        }

        public void RemoveListingElement(string _playerID)
        {
            for (int i = 0; i < playerListingElements.Count; i++)
            {
                if (playerListingElements[i].playerIDref == _playerID)
                    playerListingElements[i].Remove();
            }
        }
    }
}