using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

namespace GameplayFramework.Network
{
    public class NetworkSharedTransform : MonoBehaviourPunCallbacks, IPunObservable, IPunOwnershipCallbacks
    {
        public Transform m_SharedTransform;
        [SerializeField] PhotonView m_PhotonView;
        [SerializeField] Rigidbody m_Rigidbody;

        [SerializeField] Vector3 m_RemoteSharedPosition;
        Quaternion m_RemoteSharedRotation;

        [SerializeField] float teleportDistance = 5f;

        public bool GetControlInLocal = false;

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

        /// <summary>
        /// Donne l'owner du control de la position à un autre joueur.
        /// </summary>
        public void OverrideNetworkSharedTransform(NetworkSharedTransform target)
        {
            PhotonNetwork.GetPhotonView(target.PhotonView.ViewID).TransferOwnership(PhotonNetwork.LocalPlayer);
            PhotonView.RPC("RPC_SetControlInLocal", RpcTarget.AllBuffered, target.PhotonView.ViewID, false);
        }

        /// <summary>
        /// Récupère l'owner initial.
        /// </summary>
        public void OverrideNetworkSharedTransformToDefaultPlayer()
        {
            PhotonNetwork.GetPhotonView(PhotonView.ViewID).TransferOwnership(PhotonNetwork.LocalPlayer);
            PhotonView.RPC("RPC_SetControlInLocal", RpcTarget.AllBuffered, PhotonView.ViewID, true);
        }

        public void OnOwnershipRequest(PhotonView targetView, Photon.Realtime.Player requestingPlayer)
        {
            if (targetView != PhotonView)
                return;
        }

        public void ResetPosAndRot()
        {
            m_RemoteSharedPosition = m_SharedTransform.position;
            m_RemoteSharedRotation = m_SharedTransform.rotation;
        }

        /// <summary>
        /// Callback quand il y a un changement d'owner.
        /// </summary>
        /// <param name="targetView"></param>
        /// <param name="previousOwner"></param>
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

        /// <summary>
        /// Actualise les positions et la rotation en lecture des autres objets.
        /// </summary>
        void ActualisePositionAndRotation()
        {
            var lagDistance = m_RemoteSharedPosition - m_SharedTransform.position;

            if (lagDistance.magnitude > teleportDistance)
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
            PhotonNetwork.GetPhotonView(id).GetComponent<NetworkSharedTransform>().GetControlInLocal = state;
        }

        [PunRPC]
        void RPC_UpdatePosAndRot(int id, Vector3 pos, Quaternion rot)
        {
            NetworkSharedTransform nst = PhotonNetwork.GetPhotonView(id).GetComponent<NetworkSharedTransform>();
            if (nst == null) return;
            m_RemoteSharedPosition = pos;
            m_RemoteSharedRotation = rot;
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