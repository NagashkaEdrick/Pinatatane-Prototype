using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

namespace GameplayFramework.Network
{
    public class LagCompensation : MonoBehaviourPunCallbacks, IPunObservable
    {
        public Transform m_SharedTransform;
        [SerializeField] PhotonView m_PhotonView;

        [SerializeField] Vector3 m_RemoteSharedPosition;
        Quaternion m_RemoteSharedRotation;

        public bool GetControlInLocal = false;
        public Transform m_Target;

        public LagCompensation obj;

        public bool objTest = true;

        public PhotonView PhotonView { get => m_PhotonView; set => m_PhotonView = value; }

        private void Start()
        {
            if(PhotonView.IsMine)
                GetControlInLocal = true;

            m_RemoteSharedPosition = m_SharedTransform.position;
            m_RemoteSharedRotation = m_SharedTransform.rotation;

        }

        public void Update()
        {
            if (objTest && Input.GetKeyDown(KeyCode.P))
            {
                obj = FindObjectOfType<GrabableObject>().GetComponent<LagCompensation>();

                obj.GetControlInLocal = true;

                PhotonNetwork.GetPhotonView(obj.PhotonView.ViewID).TransferOwnership(PhotonNetwork.LocalPlayer);

                PhotonView.RPC("Test", RpcTarget.All, obj.PhotonView.ViewID, 
                     new Vector3(m_SharedTransform.position.x, 1f, m_SharedTransform.position.z));

                NetworkDebugger.Instance.Debug(m_RemoteSharedPosition, DebugType.LOCAL);
            }

            ActualisePositionAndRotation();

        }

        void ActualisePositionAndRotation()
        {
            if (GetControlInLocal)
                return;

            var lagDistance = m_RemoteSharedPosition - m_SharedTransform.position;

            if (lagDistance.magnitude > 5f)
            {
                transform.position = m_RemoteSharedPosition;
                lagDistance = Vector3.zero;
            }

            if (lagDistance.magnitude > .11f)
            {
                m_SharedTransform.position = Vector3.Lerp(m_SharedTransform.position, m_RemoteSharedPosition, .2f);
            }

            m_SharedTransform.rotation = Quaternion.Lerp(m_SharedTransform.rotation, m_RemoteSharedRotation, .2f);
        }

        [PunRPC]
        void Test(int id, Vector3 sharedPos)
        {
            PhotonNetwork.GetPhotonView(id).GetComponent<LagCompensation>().GetControlInLocal = false;
            PhotonNetwork.GetPhotonView(id).GetComponent<LagCompensation>().m_RemoteSharedPosition = sharedPos;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                if (!GetControlInLocal) // A TETSTER
                {
                    stream.SendNext(m_SharedTransform.position);
                    stream.SendNext(m_SharedTransform.rotation);
                }

                //if (obj != null)
                    //NetworkDebugger.Instance.Debug(obj.GetControlInLocal, DebugType.LOCAL);
            }
            else
            {
                m_RemoteSharedPosition = (Vector3)stream.ReceiveNext();
                m_RemoteSharedRotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}