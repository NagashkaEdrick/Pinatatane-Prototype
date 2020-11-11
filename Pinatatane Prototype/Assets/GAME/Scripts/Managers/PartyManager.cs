using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Pinatatane
{
    public class PartyManager : MonoBehaviourStateMachine
    {
        /*
         * Gèrer les différentes phases de jeu et leurs transition (une genre de state machine)
         */
         
        public static PartyManager Instance;

        [BoxGroup("References")]
        public PhotonView photonView;
        [BoxGroup("References")]
        public SpawnPoint[] spawnPoints;

        [BoxGroup("References")]
        public float spawnPointFreeTimer = 5f;

        private void Awake()
        {
            Instance = this;
        }

        public void OnJoinGame()
        {
            if (PlayerManager.Instance.IsHosting())
                StartStateMachine();
        }

        public void SpawnToAFreePoint(Transform _transform)
        {
            _transform.position = new Vector3(
                GetFreeSpawnpoint().transform.position.x,
                _transform.position.y,
                GetFreeSpawnpoint().transform.position.z);
        }

        SpawnPoint GetFreeSpawnpoint()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].isFree)
                {
                    //photonView.RPC("SetSpawnPointFreedomState", RpcTarget.AllBuffered, i, false);
                    //StartCoroutine(FreeSpawnpoint(i));
                    return spawnPoints[i];
                }
            }

            return null;
        }

        IEnumerator FreeSpawnpoint(int _index)
        {
            yield return new WaitForSeconds(spawnPointFreeTimer);
            spawnPoints[_index].isFree = true;
            photonView.RPC("SetSpawnPointFreedomState", RpcTarget.AllBuffered, _index, true);
            yield break;
        }

        #region RPC Functions
        [PunRPC]
        public void SetSpawnPointFreedomState(int _index, bool _state)
        {
            spawnPoints[_index].isFree = _state;
        }
        #endregion

#if UNITY_EDITOR
        [Button]
        void FindSpawnPoints()
        {
            //COMMENTAIRES
            spawnPoints = new SpawnPoint[transform.GetChild(0).childCount];

            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                spawnPoints[i] = transform.GetChild(0).GetChild(i).GetComponent<SpawnPoint>();
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}