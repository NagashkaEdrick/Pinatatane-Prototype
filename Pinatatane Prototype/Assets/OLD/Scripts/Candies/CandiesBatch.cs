using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OldPinatatane
{
    public class CandiesBatch : MonoBehaviour
    {
        public static CandiesBatch Instance;

        public PhotonView photonView;

        [SerializeField] Candy candyPrefab = default;

        public List<Candy> candies = new List<Candy>();

        public Transform pool, inGame;

        private void Awake()
        {
            Instance = this;
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.V))
        //        photonView.RPC("InstantiateCandies", RpcTarget.All, 5);


        //    if (Input.GetKeyDown(KeyCode.C))
        //        Pool(10, Vector3.zero);
        //}

        //public void Pool(int _qte, Vector3 _pos)
        //{
        //    for (int i = 0; i < _qte; i++)
        //    {
        //        GameObject go = PhotonNetwork.Instantiate("Candy", _pos, Quaternion.identity);
        //        Candy c = go.GetComponent<Candy>();
        //        c.Pool(_pos);
        //        c.name = "RPC Candy";
        //    }
        //}

        //[PunRPC]
        //public void InstantiateCandies(int _qte)
        //{
        //    if (PlayerManager.Instance.IsHosting())
        //    {
        //        for (int i = 0; i < _qte; i++)
        //        {
        //            GameObject go = PhotonNetwork.Instantiate("Candy", new Vector3(500, 500, 500), Quaternion.identity);
        //            Candy c = go.GetComponent<Candy>();
        //            candies.Add(c);
        //            c.name = "RPC Candy";
        //            c.Pool(Vector3.zero);
        //            //c.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }
}