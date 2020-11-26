using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

namespace GameplayFramework.Network
{
    public class LagCompensation : MonoBehaviour, IPunObservable
    {
        public Transform m_SharedTransform;
        [SerializeField] PhotonView m_PhotonView;

        Vector3 m_RemotePlayerPosition;
        Quaternion m_RemotePlayerRotation;

        public bool isOverride = false;
        public Transform m_Target;

        Vector3 m_RemoteTargetPosition;
        Quaternion m_RemoteTargetRotation;

        public LagCompensation obj;

        public PhotonView PhotonView { get => m_PhotonView; set => m_PhotonView = value; }

        private void Start()
        {
            isOverride = false;
            m_RemotePlayerPosition = m_SharedTransform.position;
            m_RemotePlayerRotation = m_SharedTransform.rotation;

            obj = FindObjectOfType<GrabableObject>().GetComponent<LagCompensation>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                m_Target = obj.transform;
                m_RemoteTargetPosition = m_Target.position;
                m_RemoteTargetRotation = m_Target.rotation;

                PhotonView.RPC("Test", RpcTarget.All);
            }

            if (Input.GetKey(KeyCode.I))
            {
                if (m_Target != null)
                {
                    m_Target.transform.position += Vector3.forward * 2f * Time.deltaTime;
                }
            }

            if (!isOverride)
            {                
                ActualisePositionAndRotation();

                if(m_Target != null)
                {
                    ActualiseTargetPositionAndRotation();
                }
            }
        }

        void ActualisePositionAndRotation()
        {
            if (PhotonView.IsMine || !NetworkManager.Instance.UseNetwork)
                return;

            var lagDistance = m_RemotePlayerPosition - m_SharedTransform.position;

            if (lagDistance.magnitude > 5f)
            {
                transform.position = m_RemotePlayerPosition;
                lagDistance = Vector3.zero;
            }

            if (lagDistance.magnitude > .11f)
            {
                m_SharedTransform.position = Vector3.Lerp(m_SharedTransform.position, m_RemotePlayerPosition, .2f);
            }

            m_SharedTransform.rotation = Quaternion.Lerp(m_SharedTransform.rotation, m_RemotePlayerRotation, .2f);
        }

        void ActualiseTargetPositionAndRotation()
        {
            var lagDistance = m_RemoteTargetPosition - m_Target.position;
             
            Debug.Log("target = " + m_Target.transform.position);
            Debug.Log("remoteTraget = " + m_RemoteTargetPosition);

            if (lagDistance.magnitude > .11f)
            {
                m_Target.position = m_RemoteTargetPosition;
                m_Target.rotation = m_RemoteTargetRotation;
            }
        }

        [PunRPC]
        void Test()
        {            
            obj.PhotonView.GetComponent<LagCompensation>().isOverride = true;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (!isOverride)
            {
                if (stream.IsWriting)
                {
                    stream.SendNext(m_SharedTransform.position);
                    stream.SendNext(m_SharedTransform.rotation);
                }
                else
                {
                    m_RemotePlayerPosition = (Vector3)stream.ReceiveNext();
                    m_RemotePlayerRotation = (Quaternion)stream.ReceiveNext();
                }
            }
            else
            {
                if (stream.IsWriting)
                {
                    //stream.SendNext(m_SharedTransform.position);
                    //stream.SendNext(m_SharedTransform.rotation);

                    stream.SendNext(m_Target.position);
                    stream.SendNext(m_Target.rotation);
                }
                else
                {
                    //    m_RemotePlayerPosition = (Vector3)stream.ReceiveNext();
                    //    m_RemotePlayerRotation = (Quaternion)stream.ReceiveNext();

                    m_RemoteTargetPosition = (Vector3)stream.ReceiveNext();
                    m_RemoteTargetRotation = (Quaternion)stream.ReceiveNext();
                }
            }
        }
    }
}