using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

namespace GameplayFramework.Network
{
    public class LagCompensation : MonoBehaviour, IPunObservable
    {
        [SerializeField] Transform m_SharedTransform;
        [SerializeField] PhotonView m_PhotonView;
        [SerializeField] Controller m_Controller;

        Vector3 m_RemotePlayerPosition;
        Quaternion m_RemotePlayerRotation;

        public void Update()
        {
            if (m_PhotonView.IsMine || !NetworkManager.Instance.UseNetwork)
                return;

            var lagDistance = m_RemotePlayerPosition - m_SharedTransform.position;

            if(lagDistance.magnitude > 5f)
            {
                transform.position = m_RemotePlayerPosition;
                lagDistance = Vector3.zero;
            }

            if(lagDistance.magnitude > .11f)
            {
                m_Controller.Pawn.PawnTransform.position = Vector3.Lerp(m_Controller.Pawn.PawnTransform.position, m_RemotePlayerPosition, .2f);
            }

            m_Controller.Pawn.PawnTransform.rotation = Quaternion.Lerp(m_Controller.Pawn.PawnTransform.rotation, m_RemotePlayerRotation, .2f);
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
                m_RemotePlayerPosition = (Vector3)stream.ReceiveNext();
                m_RemotePlayerRotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}