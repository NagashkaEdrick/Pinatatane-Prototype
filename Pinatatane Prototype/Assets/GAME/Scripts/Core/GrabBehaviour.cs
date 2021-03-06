﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*** A FAIRE :
 * - Refaire au propre toutes les references d'objet, tous les GetComponent fait en RunTime à changer
 *          -> Faire les references dans le gameManager et acceder a ses references par son instance ??
 */

namespace Pinatatane
{
    public class GrabBehaviour : MonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public float duration = 1f;
        [BoxGroup("Tweaking")]
        [Range(0f, 1f)]
        public float crossHairPosition = 0.5f;
        [BoxGroup("Tweaking")]
        public float length = 15f;
        [BoxGroup("Tweaking")]
        public int numberOfLink = 10;
        [BoxGroup("Tweaking")]
        public bool debug = false;
        [BoxGroup("Tweaking")]
        public float attractionForce = 50f;

        [BoxGroup("Fix")]
        public Camera _camera;
        [BoxGroup("Fix")]
        public Image crossHair;

        private Coroutine grabCoroutine = null;
        private GameObject[] links;
        public GameObject objectGrabed = null;

        int cptLink;

        CharacterMovementBehaviour cc => PlayerManager.Instance.LocalPlayer.characterMovementBehaviour;
        AnimatorBehaviour animator => PlayerManager.Instance.LocalPlayer.animatorBehaviour;

        [SerializeField] Pinata pinata;

        private void Start()
        {
            links = new GameObject[numberOfLink + 1];
        }

        private void Update() {
            crossHair.transform.position = new Vector3(crossHair.transform.position.x, crossHairPosition * Screen.height, 0);
            if (debug)
            {
                Vector3 crossHairPos = new Vector3(crossHair.transform.position.x, crossHair.transform.position.y, 0);
                Ray ray = _camera.ScreenPointToRay(crossHairPos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, length))
                {
                    Debug.DrawRay(ray.origin, ray.direction * length, Color.red);
                    Debug.DrawRay(transform.position, hit.point - transform.position, Color.blue);
                    if (Input.GetKeyDown(KeyCode.Keypad0)) Debug.Log(hit.collider.name);
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * length, Color.yellow);
                    Vector3 dest = ray.origin + ray.direction.normalized * length;
                    Debug.DrawRay(transform.position, dest - transform.position, Color.blue);
                }
            }
        }

        public void GrabAnime()
        {
            animator.SetBool("grab", true);
        }

        public void GrabAction()
        {
            if (grabCoroutine == null)
            {
                grabCoroutine = StartCoroutine(StartGrab());
                // A terme, separer en 2 script le mouvement et la rotation et desactiver le mouvement durant toutes la durée du dash et la rotation uniquement durant l'aller
                cc.setMovementActive(false);
                cc.setRotationActive(false);
            }
        }

        IEnumerator StartGrab()
        {
            cptLink = 0;
            links[cptLink] = transform.GetChild(0).gameObject;

            Vector3 crossHairPos = new Vector3(crossHair.transform.position.x, crossHair.transform.position.y, 0);
            Ray ray = _camera.ScreenPointToRay(crossHairPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, length))
            {
                yield return LaunchGrab(hit.point);
            }
            else
            {
                yield return LaunchGrab(ray.origin + ray.direction.normalized * length);
            }
            grabCoroutine = null;
            animator.SetBool("grab", false);
            cc.setMovementActive(true);
            yield break;
        }

        IEnumerator LaunchGrab(Vector3 destinationPoint)
        {
            float distance = Vector3.Distance(links[0].transform.position, destinationPoint); //distance entre la main et la destination
            float lenghtBetweenLink = distance / numberOfLink;
            Collider[] grabedObjects;

            while (cptLink < numberOfLink && InputManagerQ.Instance.GetTrigger("RightTrigger"))
            {
                yield return new WaitForSeconds(duration / numberOfLink);

                GameObject link = Instantiate(links[cptLink], links[cptLink].transform, true);
                link.name = "Maillon " + cptLink;
                link.transform.Translate((destinationPoint - link.transform.position).normalized * lenghtBetweenLink, Space.World);
                links[++cptLink] = link;

                grabedObjects = Physics.OverlapSphere(links[cptLink].transform.position, links[cptLink].GetComponent<SphereCollider>().radius);
                for (int i = 0; i < grabedObjects.Length; i++) {
                    if (grabedObjects[i].GetComponent(typeof(IGrabable))) { // Un objet a etait grab
                        Debug.Log(grabedObjects[i].gameObject.name);
                        yield return WaitForInput(grabedObjects[i].gameObject);
                        // Tester le type de la cible
                        if (grabedObjects[i].gameObject.GetComponent<Pinata>())
                        {
                            GetGrabInfo(grabedObjects[i].gameObject.GetComponent<Pinata>().ID, pinata.ID);
                        }
                        yield break;
                    }
                }
            }
            yield return RetractGrab();
        }

        IEnumerator WaitForInput(GameObject objectGrabbed) {
            float t = Time.time;
            yield return new WaitWhile(() => ((Time.time - t) < 1f && InputManagerQ.Instance.GetAxis("Vertical") < 0.5f
                                                                   && InputManagerQ.Instance.GetAxis("Vertical") > -0.5f
                                                                   && InputManagerQ.Instance.GetAxis("RotationX") < 0.5f
                                                                   && InputManagerQ.Instance.GetAxis("RotationX") > -0.5f));
            // En fonction de quel façon on est sortie du while on lance differentes coroutine
            if (InputManagerQ.Instance.GetAxis("Vertical") <= -0.5f && InputManagerQ.Instance.GetAxis("RotationX") < 0.5f
                                                                    && InputManagerQ.Instance.GetAxis("RotationX") > -0.5f) yield return AttractTarget(objectGrabbed);
            else if (InputManagerQ.Instance.GetAxis("Vertical") >= 0.5f && InputManagerQ.Instance.GetAxis("RotationX") < 0.5f
                                                                        && InputManagerQ.Instance.GetAxis("RotationX") > -0.5f) yield return GoToTarget(objectGrabbed);
            else if (InputManagerQ.Instance.GetAxis("RotationX") <= -0.5f && InputManagerQ.Instance.GetAxis("Vertical") < 0.5f
                                                                           && InputManagerQ.Instance.GetAxis("Vertical") > -0.5f) Debug.Log("On tourne la cible vers la gauche");
            else if (InputManagerQ.Instance.GetAxis("RotationX") >= 0.5f && InputManagerQ.Instance.GetAxis("Vertical") < 0.5f
                                                                          && InputManagerQ.Instance.GetAxis("Vertical") > -0.5f) Debug.Log("On tourne la cible vers la droite");
            else yield return RetractGrab();
        }

        IEnumerator RetractGrab() {
            cc.setRotationActive(true);
            while (cptLink > 0) {
                yield return new WaitForSeconds(duration / numberOfLink);
                Destroy(links[cptLink--]);
            }
        }

        IEnumerator AttractTarget(GameObject target) {
            cc.setRotationActive(true);
            while (cptLink > 0) {
                Vector3 grabHeadPosiiton = links[cptLink].transform.position; 
                Destroy(links[cptLink--]);
                target.GetComponent<SimplePhysic>().AddForce((grabHeadPosiiton - target.transform.position).normalized * attractionForce);
                yield return new WaitForSeconds(duration / numberOfLink);
            }
        }

        IEnumerator GoToTarget(GameObject target) {
            // to do
            yield return RetractGrab();
        }

        public void SetObjectGrabed(GameObject o)
        {
            objectGrabed = o;
        }

        public void GetGrabInfo(string targetId, string attackerId)
        {
            pinata.Grab(targetId, attackerId);
        }
    }
}
