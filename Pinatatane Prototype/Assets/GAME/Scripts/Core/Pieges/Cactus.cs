using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OldPinatatane
{
    public class Cactus : PiegeBase<CactusData>
    {
        public Vector3 size;
        public float radius; 

        private void Update()
        {
            Collider[] c = Physics.OverlapBox(transform.position, size);

            foreach(Collider p in c)
            {
                if (p.GetComponent<Pinata>())
                    Effect(p.GetComponent<Pinata>());
            }
        }

        public override void Effect(Pinata p)
        {
            if (p.canBeGrabbed)
            {
                p.HP -= piegeData.GetDamages(p.velocity);
                NetworkDebugger.Instance.Debug(($"{0} à pris {1} damages.", p.player.NickName, piegeData.GetDamages(p.velocity)), DebugType.LOCAL);
                Debug.Log(($"{0} à pris {1} damages.", p.player.NickName, piegeData.GetDamages(p.velocity)));

                StartCoroutine(StopMoving(p));
                FeedBackObject();
            }
        }

        IEnumerator StopMoving(Pinata p)
        {
            p.isStatic = true;
            p.canBeGrabbed = false;
            yield return new WaitForSeconds(piegeData.immobilisationTimer);
            p.isStatic = false;
            yield return new WaitForSeconds(1f);
            p.canBeGrabbed = true;
            yield break;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, size);
        }

        public override void FeedBackObject()
        {
        }
    }
}