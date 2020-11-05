using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using QRTools.Inputs;

namespace Pinatatane
{
    public class PinataOverrideControl : MonoBehaviour
    {
        [SerializeField] Pinata myPinata;
        public PhotonView photonView;
        [SerializeField] QInputs[] controlsInputs = default;

        public bool isOverrided = false;

        [PunRPC]
        public void RPCWinControl(int _ID)
        {
            if (photonView.ViewID == _ID)
            {
                isOverrided = false;
                NetworkDebugger.Instance.Debug("-->" + PhotonNetwork.GetPhotonView(_ID).gameObject.name, DebugType.NETWORK);
            }
        }

        [PunRPC]
        public void RPCLoseControl(int _ID)
        {
            if (photonView.ViewID == _ID)
            {
                isOverrided = true;
            }
        }
    }
}