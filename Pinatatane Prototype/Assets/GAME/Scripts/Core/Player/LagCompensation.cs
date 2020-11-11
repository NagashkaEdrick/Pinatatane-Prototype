using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

namespace Pinatatane
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

        private void Start()
        {
            photonView.ObservedComponents.Add(this);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                //We own this player: send the others our data
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                Debug.Log("write");
            }
            else
            {
                Debug.Log("read");

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

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine)
            {
                //Lag compensation
                double timeToReachGoal = currentPacketTime - lastPacketTime;
                currentTime += Time.deltaTime;

                float lerp = (float)(currentTime / timeToReachGoal);
                Debug.Log(lerp);

                //Update remote player

                DOTween.To(
                    ()=> positionAtLastPacket,
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

                //transform.position = Vector3.Lerp(positionAtLastPacket, latestPos, lerp);
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

        //float tick = .2f;
        //[SerializeField] PhotonView photonView;

        //private void Start()
        //{
        //    StartCoroutine(LagCompensationCor());
        //}

        //IEnumerator LagCompensationCor()
        //{
        //    Vector3 oldPos = transform.position;
        //    yield return new WaitForSeconds(tick);
        //    Vector3 movement = transform.position - oldPos;
        //    Debug.Log(movement);

        //    StartCoroutine(LagCompensationCor());



        //    yield break;
        //}

        //[PunRPC]
        //public void Repositionning()
        //{

        //}

        //[SerializeField] Rigidbody r;

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        r.AddForce(new Vector3(0, 1500, 0));
        //    }
        //}

        //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        //{
        //    if (stream.IsWriting)
        //    {
        //        stream.SendNext(r.position);
        //        stream.SendNext(r.rotation);
        //        stream.SendNext(r.velocity);

        //        Debug.Log("reg");
        //    }
        //    else
        //    {
        //        r.position = (Vector3)stream.ReceiveNext();
        //        r.rotation = (Quaternion)stream.ReceiveNext();
        //        r.velocity = (Vector3)stream.ReceiveNext();

        //        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
        //        r.position += r.velocity * lag;
        //    }
        //}
    }
}