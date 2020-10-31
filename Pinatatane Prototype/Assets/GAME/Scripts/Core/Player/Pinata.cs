using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using Photon.Pun.UtilityScripts;

namespace Pinatatane
{
    public class Pinata : MonoBehaviourPunCallbacks
    {
        /*
         * Le joueur + ses comportements
         */ 

        [FoldoutGroup("References", order: 0)]
        public CharacterMovementBehaviour characterMovementBehaviour;
        [FoldoutGroup("References", order: 0)]
        [HideInInspector] public CameraController cameraController;
        [FoldoutGroup("References", order: 0)]
        public AnimatorBehaviour animatorBehaviour;
        [FoldoutGroup("References", order: 0)]
        public PhotonView photonView = default;
        [FoldoutGroup("References", order: 0)]
        public Transform cameraTarget;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] PinataUI pinataUI;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] DashBehaviour dashBehaviour;
        [FoldoutGroup("References", order: 0)]
        [SerializeField] GrabBehaviour grabBehaviour;

        public PinataOverrideControl pinataOverrideControl;

        [HideInInspector] public Player player;

        [BoxGroup("Player Infos", order: 1)]
        public int ID;

        [BoxGroup("Player Infos", order: 1)]
        public bool isGrabbed = false;

        #region Publics
        public void InitPlayer()
        {
            photonView.RPC("InitAllPlayer", RpcTarget.All);

            SetID();

            cameraController.target = cameraTarget;
            grabBehaviour._camera = cameraController.GetComponent<Camera>();
            pinataUI?.InitPlayerUI();
            SetPlayerName();
        }

        void SetID()
        {
            player = photonView.Owner;
            ID = photonView.ViewID;
        }

        private void Update()
        {
            #region Test
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (photonView.IsMine)
                {
                    MyGrab();
                }
            }
            #endregion //Input M pour des tests
        }


        #region Call Network

        public void SetPlayerName()
        {
            photonView.RPC("SetName", RpcTarget.AllBuffered, player.NickName);
        }

        public void SetPlayerReady()
        {
            photonView.RPC("SetReady", RpcTarget.AllBuffered, player.NickName);
        }

        public void Grab(int _cible, int _attaquant)
        {
            photonView.RPC("GrabNetwork", RpcTarget.AllBuffered, _cible, _attaquant);
        }

        public void MyGrab()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, 4f))
            {
                Debug.DrawRay(transform.position, transform.forward * 4f, Color.red);

                if (hit.collider.GetComponent<Pinata>())
                {
                    Pinata p = hit.collider.GetComponent<Pinata>();
                    //Debug.Log("MyGrab => " + p.gameObject.name);

                    if (p != this)
                    {
                        Grab(p.photonView.ViewID, photonView.ViewID);
                        UIManager.Instance.networkStatutElement.SetText(p.ID.ToString());
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Network

        [PunRPC]
        public void IncrementeScore(int _increment, string _id)
        {
            if (player.UserId == _id)
            {
                player.AddScore(50);
                UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").Refresh();
            }
        }

        [PunRPC]
        public void SetName(string _name)
        {
            pinataUI.playerName.text = _name;
        }

        [PunRPC]
        public void SetReady(string _playerID)
        {

        }

        #region Grab
        [PunRPC]
        public void GrabNetwork(int _cible, int _attaquant)
        {
            if(photonView.ViewID == _attaquant)
            {
                UIManager.Instance.networkStatutElement.SetText("attaquant");

                NetworkDebugger.Instance.Debug("CIBLE :" + _cible.ToString() + " || ATTAQUANT :" + _attaquant.ToString(), DebugType.NETWORK);

                //PhotonNetwork.GetPhotonView(_cible).transform.localScale *= 2;
                //PhotonNetwork.GetPhotonView(_attaquant).transform.localScale /= 2;
            }
        }

        [PunRPC]
        public void ChangePos(int _targetID, Vector3 _pos)
        {
            if (photonView.ViewID == _targetID)
            {
                transform.position = _pos;
            }
        }

        [PunRPC]
        public void ChangeRot(int _targetID, Vector3 _rot)
        {
            if (photonView.ViewID == _targetID)
            {
                transform.rotation = Quaternion.Euler(_rot);
            }
        }

        /* Exemple
        private IEnumerator ChangePositionInCoroutine()
        {
            WaitForSeconds t = new WaitForSeconds(3.0f);

            while (true)
            {
                Vector3 _pos = [...];

                photonView.RPC("ChangePos", RpcTarget.All, _pos);

                yield return t;
            }
        }
        */
        #endregion

        [PunRPC]
        public void InitAllPlayer()
        {
            player = photonView.Owner;

            var obj = FindObjectsOfType<Pinata>();
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].SetID();
                obj[i].pinataUI.playerName.SetText(obj[i].ID.ToString());
            }

            PhotonNetwork.NickName = "Guest" + Random.Range(0, 999).ToString();
            Debug.Log("Init Player -> " + player.NickName);
        }
        #endregion
    }
}