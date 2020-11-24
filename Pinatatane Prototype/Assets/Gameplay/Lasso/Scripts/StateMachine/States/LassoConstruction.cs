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
        Ray ray;

        float distanceParcouru;
        LassoController lassoController;

        /** Variable avec maillons **/
        private GameObject[] links;
        int cptLink;

        public override void OnEnter(LassoController element)
        {
            if(element.debugMode) Debug.Log("<color=yellow>Lasso:</color> Construction Du Lasso...");

            lassoController = element;
            lassoConstrucionCoroutine = StartCoroutine(StartConstruction(element));
            /** Avec maillon (ne pas decommenter tant que la deconstruction n'est pas faite) **/
            //Initialise(element);

            base.OnEnter(element);
        }

        IEnumerator StartConstruction(LassoController element)
        {
            //Construction du raycast avec le temps
            while (distanceParcouru < element.Lasso.LassoData.constructionDistance)
            {
                distanceParcouru += element.Lasso.LassoData.constructionDistance * Time.deltaTime / element.Lasso.LassoData.constructionTime;

                if (Physics.Raycast(element.StartPosition.position, element.StartPosition.forward, out hit, distanceParcouru))
                {
                    if (hit.collider.TryGetComponent(typeof(IGrabbable), out var _grabbedObject))
                    {
                        //Quand on touche un objet.
                        element.Lasso.CurrenObjectGrabbed = _grabbedObject as IGrabbable;
                        if (element.debugMode) Debug.Log("<color=yellow>Lasso:</color> Le joueur attrape " + ((MonoBehaviour)_grabbedObject).name + ".");
                        break;
                    }
                    //Et si le joueur touche un objet qui ne peut pas se faire grabber ?
                }

                yield return null;
            }

            if (element.debugMode) Debug.Log("<color=yellow>Lasso: </color> Le joueur n'a rien attrapé.");

            yield break;
        }

        public override void OnExit(LassoController element)
        {
            if(lassoConstrucionCoroutine != null) StopCoroutine(lassoConstrucionCoroutine);
            distanceParcouru = 0;
        }

        private void OnDrawGizmos()
        {
            if (lassoController != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lassoController.StartPosition.position, lassoController.StartPosition.position + lassoController.StartPosition.forward * distanceParcouru);
            }
        }

        /** Construction du lasso avec les maillons **/

        void Initialise(LassoController element)
        {
            cptLink = 0;
            links = new GameObject[element.Lasso.LassoData.numberOfLink + 1];
            links[cptLink] = element.Maillon;

            Vector3 crossHairPos = new Vector3(element.PinataController.Pinata.CrossHair.transform.position.x, element.PinataController.Pinata.CrossHair.transform.position.y, 0);
            Ray ray = element.PinataController.Pinata.MainCamera.ScreenPointToRay(crossHairPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, element.Lasso.LassoData.constructionDistance))
            {
                StartCoroutine(Launch(hit.point, element));
            }
            else
            {
                StartCoroutine(Launch(ray.origin + ray.direction.normalized * element.Lasso.LassoData.constructionDistance, element));
            }
        }

        IEnumerator Launch(Vector3 destinationPoint, LassoController element)
        {
            float distance = Vector3.Distance(links[0].transform.position, destinationPoint); //distance entre la main et la destination
            float lenghtBetweenLink = distance / element.Lasso.LassoData.numberOfLink;
            Collider[] grabedObjects;

            while (cptLink < element.Lasso.LassoData.numberOfLink)
            {
                yield return new WaitForSeconds(element.Lasso.LassoData.constructionTime / element.Lasso.LassoData.numberOfLink);

                GameObject link = Instantiate(links[cptLink], links[cptLink].transform, true);
                link.name = "Maillon " + cptLink;
                link.transform.Translate((destinationPoint - link.transform.position).normalized * lenghtBetweenLink, Space.World);
                links[++cptLink] = link;

                // faire un sphereCast a la place
                grabedObjects = Physics.OverlapSphere(links[cptLink].transform.position, links[cptLink].GetComponent<SphereCollider>().radius);
                for (int i = 0; i < grabedObjects.Length; i++)
                {
                    if (grabedObjects[i].GetComponent(typeof(IGrabbable)))
                    { // Un objet a etait grab
                        Debug.Log("<color=yellow>Lasso:</color> Le joueur attrape " + (grabedObjects[i].name + "."));

                        // Passage a la state suivante

                        yield break;
                    }
                }
            }
            // On est arriver a la fin du lasso
        }


    }
}