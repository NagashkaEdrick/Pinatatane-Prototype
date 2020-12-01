using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using Photon.Pun;

using GameplayFramework.Network;

namespace Pinatatane
{
    public class NetworkSharedComponents : SerializedMonoBehaviour
    {
        public PhotonView PhotonView;

        public PinataController PinataController;
        public NetworkSharedTransform NetworkSharedTransform;

        public void OnStartGrab()
        {
            PhotonView.RPC("RPC_OnStartGrabbed", RpcTarget.All);
        }

        public void OnEndGrab()
        {
            PhotonView.RPC("RPC_OnEndGrabbed", RpcTarget.All);
        }

        [PunRPC]
        void RPC_OnStartGrabbed()
        {
            PinataController _pinataController = PhotonNetwork.GetPhotonView(PhotonView.ViewID).GetComponent<NetworkSharedComponents>().PinataController;
            if (_pinataController == null)
                return;

            _pinataController.IsControllable = false;
        }

        [PunRPC]
        void RPC_OnEndGrabbed()
        {
            PinataController _pinataController = PhotonNetwork.GetPhotonView(PhotonView.ViewID).GetComponent<NetworkSharedComponents>().PinataController;
            if (_pinataController == null)
                return;

            Debug.Log("END GRABBBB");

            NetworkSharedTransform.OverrideNetworkSharedTransformToDefaultPlayer();
            _pinataController.IsControllable = true;
        }
    }
}