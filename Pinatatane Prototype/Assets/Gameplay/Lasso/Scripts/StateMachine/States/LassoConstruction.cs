using System.Collections;
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

                Vector3 aimPoint = new Vector3(element.PinataController.Pawn.PawnTransform.position.x,
                                                element.StartPosition.position.y,
                                                element.PinataController.Pawn.PawnTransform.position.z)
                                   + element.PinataController.Pawn.PawnTransform.forward
                                   * distanceParcouru;

                if (element.debugMode) Debug.DrawRay(element.StartPosition.position, (aimPoint - element.StartPosition.position).normalized * distanceParcouru, Color.cyan, 0.1f);

                element.Lasso.LassoGraphics.Launch(aimPoint);

                if (Physics.Raycast(element.StartPosition.position, (aimPoint - element.StartPosition.position).normalized, out hit, distanceParcouru))
                {
                    if (hit.collider.TryGetComponent(typeof(IGrabbable), out var _grabbedObject))
                    {
                        if (_grabbedObject != element.Lasso.MyGraddable)
                        {
                            //Quand on touche un objet.
                            element.Lasso.CurrenObjectGrabbed = _grabbedObject as IGrabbable;
                            element.Lasso.LassoGraphics.LassoDebug(Color.red);
                            element.Lasso.CurrenObjectGrabbed.GrabbedBy = element.Lasso;

                            if (element.debugMode) Debug.Log("<color=yellow>Lasso:</color> Le joueur attrape " + ((MonoBehaviour)_grabbedObject).name + ".");

                            element.isConstructed = true;
                            yield break;
                        }
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
        }
    }
}