using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using Photon.Pun;

namespace Pinatatane
{
    public class NetworkSharedComponents : SerializedMonoBehaviour
    {
        public PhotonView PhotonView;

        public PinataController PinataController;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                OnStartGrab();
        }

        public void OnStartGrab()
        {
            PhotonView.RPC("RPC_OnStartGrabbed", RpcTarget.All);
        }

        [PunRPC]
        void RPC_OnStartGrabbed()
        {
            PinataController _pinataController = PhotonNetwork.GetPhotonView(PhotonView.ViewID).GetComponent<NetworkSharedComponents>().PinataController;
            if (_pinataController == null)
                return;

            Debug.Log("a");

            _pinataController.IsControllable = false;
        }

        [PunRPC]
        void RPC_OnEndGrabbed()
        {
            PinataController _pinataController = PhotonNetwork.GetPhotonView(PhotonView.ViewID).GetComponent<NetworkSharedComponents>().PinataController;
            if (_pinataController == null)
                return;

            _pinataController.IsControllable = true;
        }
    }
}