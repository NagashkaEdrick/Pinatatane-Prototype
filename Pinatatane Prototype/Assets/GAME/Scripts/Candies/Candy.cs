using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;

namespace Pinatatane
{
    public class Candy : MonoBehaviour
    {
        [SerializeField] bool isPool = false;
        public bool IsPool
        {
            get => isPool;
            set
            {
                if (value)
                    gameObject.SetActive(true);
                else
                    gameObject.SetActive(false);

                isPool = value;
            }
        }

        public CandyData data;

        Vector3 oldPos = new Vector3();

        public int ID;

        public void Pool(Vector3 _pos)
        {
            transform.position = _pos;

            transform.DOMoveX(_pos.x + Random.insideUnitCircle.x * data.SpawnRangeRdn, 1f);
            transform.DOMoveZ(_pos.z + Random.insideUnitCircle.y * data.SpawnRangeRdn, 1f);
            transform.DOMoveY(_pos.y + .5f, 1f).SetEase(Ease.InOutCirc).OnComplete(delegate {
                transform.DOMoveY(_pos.y, 1f).SetEase(data.fallingEasing);
            });
        }

        public void Push()
        {
            //transform.position = new Vector3(500, 500, 500);
            //IsPool = false;
            PhotonNetwork.Destroy(GetComponent<PhotonView>());
        }

        private void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, data.touchRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<Pinata>())
                {
                    Push();
                    //CandiesBatch.Instance.Push(ID);
                }
            }

            Collider[] attireColliders = Physics.OverlapSphere(transform.position, data.attireRadius);
            foreach (var hitCollider in attireColliders)
            {
                if (hitCollider.GetComponent<Pinata>())
                {
                    Vector3 dir = hitCollider.GetComponent<Pinata>().transform.position - transform.position;
                    dir.Normalize();

                    transform.position += dir * Time.deltaTime * data.attireSpeed * (1/Vector3.Distance(hitCollider.GetComponent<Pinata>().transform.position, transform.position));
                }
            }
        }
    }
}