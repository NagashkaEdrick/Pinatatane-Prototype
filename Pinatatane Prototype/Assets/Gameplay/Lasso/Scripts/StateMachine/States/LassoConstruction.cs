﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// Cette state gère le déploiement du lasso.
    /// </summary>
    public class LassoConstruction : State<LassoController>
    {
        Coroutine lassoConstrucionCoroutine;
        RaycastHit hit;
        Ray ray;

        float distanceParcouru;
        LassoController lassoController;

        public override void OnEnter(LassoController element)
        {
            if(element.debugMode) Debug.Log("<color=yellow>Lasso:</color> Construction Du Lasso...");

            lassoController = element;
            lassoConstrucionCoroutine = StartCoroutine(StartConstruction(element));

            base.OnEnter(element);
        }

        IEnumerator StartConstruction(LassoController element)
        {
            //Construction du raycast avec le temps
            while (distanceParcouru < element.Lasso.LassoData.constructionDistance)
            {
                distanceParcouru += element.Lasso.LassoData.constructionDistance * Time.deltaTime / element.Lasso.LassoData.constructionTime;

                element.Lasso.LassoGraphics.Launch(element.StartPosition.position + element.StartPosition.forward * distanceParcouru);

                if (Physics.Raycast(element.StartPosition.position, element.StartPosition.forward, out hit, distanceParcouru))
                {
                    if (hit.collider.TryGetComponent(typeof(IGrabbable), out var _grabbedObject))
                    {
                        //Quand on touche un objet.
                        element.Lasso.CurrenObjectGrabbed = _grabbedObject as IGrabbable;
                        element.Lasso.LassoGraphics.LassoDebug(Color.red);

                        if (element.debugMode) Debug.Log("<color=yellow>Lasso:</color> Le joueur attrape " + ((MonoBehaviour)_grabbedObject).name + ".");

                        element.isConstructed = true;
                        yield break;
                    }
                }
                
                yield return null;
            }

            yield return new WaitForSeconds(.2f);

            element.isConstructed = true;
            if (element.debugMode) Debug.Log("<color=yellow>Lasso: </color> Le joueur n'a rien attrapé.");

            yield break;
        }

        public override void OnExit(LassoController element)
        {
            if(lassoConstrucionCoroutine != null) StopCoroutine(lassoConstrucionCoroutine);
            distanceParcouru = 0;

            //element.isConstructed = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (lassoController != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lassoController.StartPosition.position, lassoController.StartPosition.position + lassoController.StartPosition.forward * distanceParcouru);
            }
        }
#endif
    }
}