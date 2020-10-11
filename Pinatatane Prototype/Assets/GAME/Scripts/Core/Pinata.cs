using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;

namespace Pinatatane
{
    public class Pinata : MonoBehaviour
    {
        public CharacterMovementBehaviour characterMovementBehaviour;
        public CameraController cameraController;
        public AnimatorBehaviour animatorBehaviour;

        public PhotonView photonView;

        public Transform cameraTarget;

        public void InitPlayer()
        {
            cameraController.target = cameraTarget;
        }

        private void Update()
        {
            //Test
            if (Input.GetKeyDown(KeyCode.K))
            {
                photonView.RPC("Big", RpcTarget.Others, new Vector3(2,2,2));
            }
        }

        [PunRPC]
        public void Big(Vector3 _value)
        {
            transform.localScale = _value;
        }
    }
}