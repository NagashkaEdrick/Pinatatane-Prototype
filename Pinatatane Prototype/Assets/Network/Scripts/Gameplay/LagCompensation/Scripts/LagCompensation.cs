using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace GameplayFramework.Network
{
    public class LagCompensation : MonoBehaviourPunCallbacks, IPunObservable, IPunOwnershipCallbacks
    {
        public Transform m_SharedTransform;
        [SerializeField] PhotonView m_PhotonView;

        [SerializeField] Vector3 m_RemoteSharedPosition;
        Quaternion m_RemoteSharedRotation;

        public bool GetControlInLocal = false;

        public LagCompensation obj;

        public bool objTest = true;

        public PhotonView PhotonView { get => m_PhotonView; set => m_PhotonView = value; }

        private void Start()
        {
            if(PhotonView.IsMine)
                GetControlInLocal = true;

            PhotonNetwork.AddCallbackTarget(this);

            m_RemoteSharedPosition = m_SharedTransform.position;
            m_RemoteSharedRotation = m_SharedTransform.rotation;

        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void Update()
        {
            if(!PhotonView.IsMine)
                ActualisePositionAndRotation();
        }

        public void OverrideLagCompensation(LagCompensation target)
        {
            if (GetControlInLocal)
                return;

            PhotonNetwork.GetPhotonView(target.PhotonView.ViewID).TransferOwnership(PhotonNetwork.LocalPlayer);
            PhotonView.RPC("RPC_SetControlInLocal", RpcTarget.AllBuffered, target.PhotonView.ViewID, false);
        }

        public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
        {
            if (targetView != PhotonView)
                return;
        }

        public void OnOwnershipTransfered(PhotonView targetView, Photon.Realtime.Player previousOwner)
        {
            if (targetView != PhotonView)
            {
                GetControlInLocal = false;
            }
            else
            {
                GetControlInLocal = true;
            }
        }

        void ActualisePositionAndRotation()
        {
            var lagDistance = m_RemoteSharedPosition - m_SharedTransform.position;

            if (lagDistance.magnitude > 5f)
            {
                m_SharedTransform.position = m_RemoteSharedPosition;
                lagDistance = Vector3.zero;
            }

            if (lagDistance.magnitude > .11f)
            {
                m_SharedTransform.position = Vector3.Lerp(m_SharedTransform.position, m_RemoteSharedPosition, .2f);
            }

            m_SharedTransform.rotation = Quaternion.Lerp(m_SharedTransform.rotation, m_RemoteSharedRotation, .2f);
        }

        [PunRPC]
        void RPC_SetControlInLocal(int id, bool state)
        {
            PhotonNetwork.GetPhotonView(id).GetComponent<LagCompensation>().GetControlInLocal = state;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(m_SharedTransform.position);
                stream.SendNext(m_SharedTransform.rotation);
            }
            else
            {
                m_RemoteSharedPosition = (Vector3)stream.ReceiveNext();
                m_RemoteSharedRotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}