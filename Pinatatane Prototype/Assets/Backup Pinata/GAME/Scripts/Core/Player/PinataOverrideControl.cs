using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Pinatatane
{
    public class PinataOverrideControl : MonoBehaviour
    {
        [SerializeField] Pinata myPinata;
        public int pinatatControlledByViewID = -100;

        public UnityEvent myEventTest = new UnityEvent();

        public void LoseControl()
        {
            NetworkDebugger.Instance.Debug("Je perd le control", DebugType.LOCAL);
        }

        public void WinControl()
        {
            NetworkDebugger.Instance.Debug("Je gagne le control", DebugType.LOCAL);
        }

        public void OnControlledStart()
        {

        }

        public void OnControlledEnd()
        {

        }

        [Button]
        public void RaiseEvent()
        {
            myPinata.PhotonView.RPC("CallOverrideControl", RpcTarget.Others, PhotonNetwork.PhotonViews[1].ViewID);
        }

        [PunRPC]
        public void AddEventToPinata(int viewID)
        {
            PinataOverrideControl oc = PhotonNetwork.GetPhotonView(viewID).GetComponent<PinataOverrideControl>();

        }

        [PunRPC]
        public void RemoveEventToPinata(int viewID)
        {
            PinataOverrideControl oc = PhotonNetwork.GetPhotonView(viewID).GetComponent<PinataOverrideControl>();

        }

        [PunRPC]
        public void CallOverrideControl(int viewID)
        {
            PinataOverrideControl oc = PhotonNetwork.GetPhotonView(viewID).GetComponent<PinataOverrideControl>();
            oc.OnControlledStart();
            oc.myEventTest.Invoke();
            oc.OnControlledEnd();
        }
    }
}