using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class LassoConstruction : State<LassoController>
    {
        Coroutine lassoConstrucionCoroutine;
        RaycastHit hit;
        Ray ray;

        float distanceParcouru;
        LassoController lassoController;

        public override void OnEnter(LassoController element)
        {
            Debug.Log("Lasso : Construction Du Lasso");

            lassoController = element;
            lassoConstrucionCoroutine = StartCoroutine(StartConstruction(element));

            base.OnEnter(element);
        }

        IEnumerator StartConstruction(LassoController element)
        {
            while (distanceParcouru < element.LassoData.constructionDistance)
            {
                distanceParcouru += element.LassoData.constructionDistance * Time.deltaTime / element.LassoData.constructionTime;

                if (Physics.Raycast(element.StartPosition.position, element.StartPosition.forward, out hit, distanceParcouru))
                {
                    if (hit.collider.TryGetComponent(typeof(IGrabbable), out var _grabbedObject))
                    {
                        //On touche un object !!!

                        element.CurrenObjectGrabbed = _grabbedObject as IGrabbable;
                        break;
                    }
                }

                yield return null;
            }

            //On ne touche rien

            yield break;
        }

        public override void OnExit(LassoController element)
        {
            if(lassoConstrucionCoroutine != null) StopCoroutine(lassoConstrucionCoroutine);
            distanceParcouru = 0;
            Debug.Log("exit");
        }

        private void OnDrawGizmos()
        {
            if (lassoController != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lassoController.StartPosition.position, lassoController.StartPosition.position + lassoController.StartPosition.forward * distanceParcouru);
            }
        }
    }
}