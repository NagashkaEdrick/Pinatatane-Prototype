using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using QRTools.Inputs;

namespace OldPinatatane
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
            PhotonNetwork.GetPhotonView(_ID).GetComponent<PinataOverrideControl>().isOverrided = true;
            Debug.Log("win");

            //if (photonView.ViewID == _ID)
            //{
            //    isOverrided = false;
            //}
        }

        [PunRPC]
        public void RPCLoseControl(int _ID)
        {
            Debug.Log("Lose");

            if (photonView.ViewID == _ID)
            {
                isOverrided = false;
            }
        }
    }
}