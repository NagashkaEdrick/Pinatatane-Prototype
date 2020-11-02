using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pinatatane
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
                Pool(10, Vector3.zero);
        }

        public void Pool(int _qte, Vector3 _pos)
        {
            for (int i = 0; i < _qte; i++)
            {
                GameObject go = PhotonNetwork.Instantiate("Candy", _pos, Quaternion.identity);
                Candy c = go.GetComponent<Candy>();
                c.Pool(_pos);

                //if (!candies[i].IsPool)
                //{
                //    candies[i].IsPool = true;
                //    candies[i].transform.parent = inGame;
                //    candies[i].Pool(_pos);
                //    photonView.RPC("PoolRPC", RpcTarget.All, i, _pos);
                //}
            }
        }

        public void Push(int _ID)
        {
            candies[_ID].Push();
            candies[_ID].transform.parent = pool;
        }

        public void MoveCandie(int _ID, Vector3 _pos)
        {
            photonView.RPC("MoveCandieRPC", RpcTarget.All, _ID, _pos);
        }

        #region RPC Functions
        [PunRPC]
        public void PoolRPC(int _ID, Vector3 _pos)
        {
            candies[_ID].IsPool = true;
            candies[_ID].transform.parent = inGame;
            candies[_ID].Pool(_pos);
        }

        [PunRPC]
        public void MoveCandieRPC(int _ID, Vector3 _pos)
        {
            candies[_ID].transform.position = _pos;
        }
        #endregion

#if UNITY_EDITOR
        [Button]
        void InstantiateCandies(int quantite)
        {
            if (quantite == 0)
            {
                for (int i = 0; i < candies.Count; i++)
                {
                    GameObject c = candies[i].gameObject;
                    c.SetActive(true);
                    DestroyImmediate(c);
                }
                candies.Clear();
            }

            for (int i = 0; i < quantite; i++)
            {
                Candy c = PrefabUtility.InstantiatePrefab(candyPrefab) as Candy;
                c.transform.position = new Vector3(500, 0, 500);
                c.transform.parent = pool;
                candies.Add(c);
                candies[i].ID = i;

                c.IsPool = false;
            }

            EditorUtility.SetDirty(this);
        }
#endif
    }
}