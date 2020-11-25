using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

namespace OldPinatatane
{
    public class LagCompensation : MonoBehaviour, IPunObservable
    {
        //Values that will be synced over network
        Vector3 latestPos;
        Quaternion latestRot;
        //Lag compensation
        float currentTime = 0;
        double currentPacketTime = 0;
        double lastPacketTime = 0;
        Vector3 positionAtLastPacket = Vector3.zero;
        Quaternion rotationAtLastPacket = Quaternion.identity;

        [SerializeField] private PhotonView photonView;


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //We own this player: send the others our data
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                //Network player, receive data
                latestPos = (Vector3)stream.ReceiveNext();
                latestRot = (Quaternion)stream.ReceiveNext();

                //Lag compensation
                currentTime = 0.0f;
                lastPacketTime = currentPacketTime;
                currentPacketTime = info.SentServerTime;
                positionAtLastPacket = transform.position;
                rotationAtLastPacket = transform.rotation;
            }
        }

        void Update()
        {
            if (!photonView.IsMine)
            {
                //Lag compensation
                double timeToReachGoal = currentPacketTime - lastPacketTime;
                currentTime += Time.deltaTime;

                float lerp = (float)(currentTime / timeToReachGoal);

                //Update remote player

                DOTween.To(
                    () => positionAtLastPacket,
                    x => positionAtLastPacket = x,
                    latestPos,
                    .25f
                    );

                transform.DOMove(positionAtLastPacket, currentTime);

                Vector3 e = rotationAtLastPacket.eulerAngles;
                DOTween.To(
                    () => e,
                    x => e = x,
                    latestRot.eulerAngles,
                    .25f
                    );

                transform.DORotateQuaternion(latestRot, .25f);

                //transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, .2f);
                //transform.rotation = Quaternion.Lerp(rotationAtLastPacket, latestRot, lerp);

                Debug.Log("ac");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(latestPos, 1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(positionAtLastPacket, 1f);
        }
    }
}