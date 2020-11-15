using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.Inputs;
using Photon.Pun;

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
        int cptListener = 0;

        [SerializeField] Pinata pinata;

        [BoxGroup("Inputs", order: 1)]
        [SerializeField] QInputXBOXTouch grabButton = default;
        [BoxGroup("Inputs", order: 1)]
        [SerializeField] QInputXBOXAxis grabY, grabRotX;

        private void Start()
        {
            crossHair = UIManager.Instance.crossHair;
            links = new GameObject[numberOfLink + 1];
        }

        public void OnGrab()
        {
            pinata.animatorBehaviour.SetBool("grab", true);
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
            pinata.animatorBehaviour.SetBool("grab", true);
        }

        public void GrabAction()
        {
            if (grabCoroutine == null)
            {
                grabCoroutine = StartCoroutine(StartGrab());
                pinata.movement.setMovementActive(false);
                pinata.movement.setRotationActive(false);
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
            pinata.animatorBehaviour.SetBool("grab", false);
            pinata.movement.setMovementActive(true);
            yield break;
        }

        IEnumerator LaunchGrab(Vector3 destinationPoint)
        {
            float distance = Vector3.Distance(links[0].transform.position, destinationPoint); //distance entre la main et la destination
            float lenghtBetweenLink = distance / numberOfLink;
            Collider[] grabedObjects;

            while (cptLink < numberOfLink && grabButton.IsTrigger)
            {
                yield return new WaitForSeconds(duration / numberOfLink);

                GameObject link = Instantiate(links[cptLink], links[cptLink].transform, true);
                link.name = "Maillon " + cptLink;
                link.transform.Translate((destinationPoint - link.transform.position).normalized * lenghtBetweenLink, Space.World);
                links[++cptLink] = link;

                grabedObjects = Physics.OverlapSphere(links[cptLink].transform.position, links[cptLink].GetComponent<SphereCollider>().radius);
                for (int i = 0; i < grabedObjects.Length; i++) {
                    if (grabedObjects[i].GetComponent(typeof(IGrabable))) { // Un objet a etait grab

                        pinata.movement.setRotationActive(true);

                        yield return WaitForInput(grabedObjects[i].gameObject);

                        IGrabable grabable = grabedObjects[i].GetComponent<IGrabable>();
                        //grabable.StartGrab(grabable.PhotonView.ViewID);
                        //grabable.OnGrab(grabable.PhotonView.ViewID, pinata.PhotonView.ViewID);

                        yield break;
                    }
                }
            }
            yield return RetractGrab();
        }

        IEnumerator WaitForInput(GameObject objectGrabbed) {
            float t = Time.time;
            yield return new WaitWhile(() => (Time.time - t) < 1f && grabY.JoystickValue < 0.5f
                                                                  && grabY.JoystickValue > -0.5f
                                                                  && grabRotX.JoystickValue < 0.5f
                                                                  && grabRotX.JoystickValue > -0.5f);

            // En fonction de quel façon on est sortie du while on lance differentes coroutine
            if (grabY.JoystickValue <= -0.5f && grabRotX.JoystickValue < 0.5f
                                             && grabRotX.JoystickValue > -0.5f) AttractTarget(objectGrabbed);
            else if (grabY.JoystickValue >= 0.5f && grabRotX.JoystickValue < 0.5f
                                                 && grabRotX.JoystickValue > -0.5f) GoToTarget(objectGrabbed);
            else if (grabRotX.JoystickValue <= -0.5f && grabY.JoystickValue < 0.5f
                                                     && grabY.JoystickValue > -0.5f) yield return RotateTargetLeft(objectGrabbed);
            else if (grabRotX.JoystickValue >= 0.5f && grabY.JoystickValue < 0.5f
                                                    && grabY.JoystickValue > -0.5f) yield return RotateTargetRight(objectGrabbed);
            else yield return RetractGrab();
        }

        IEnumerator RetractGrab() {
            pinata.movement.setRotationActive(true);
            while (cptLink > 0) {
                yield return new WaitForSeconds(duration / numberOfLink);
                Destroy(links[cptLink--]);
            }
        }

        void AttractTarget(GameObject target) {
            StartCoroutine(RetractGrab());
            target.GetComponent<SimplePhysic>().AddForce((pinata.transform.position - target.transform.position).normalized * attractionForce);

            //pinata.EndGrab(target.GetComponent<PhotonView>().ViewID);
        }

        void GoToTarget(GameObject target) {
            StartCoroutine(RetractGrab());
            pinata.GetComponent<SimplePhysic>().AddForce((target.transform.position - pinata.transform.position).normalized * attractionForce);

            //pinata.EndGrab(target.GetComponent<PhotonView>().ViewID);
        }

        IEnumerator RotateTargetRight(GameObject target)
        {
            GetComponent<GrabRotation>().Link(pinata.transform, target.transform);
            Debug.Log("rot droite");
            yield return new WaitWhile(() => grabRotX.JoystickValue >= 0.5f); // si le temps de rotation est timer rajouter ici
            GetComponent<GrabRotation>().ReleaseRight();

            //pinata.EndGrab(target.GetComponent<PhotonView>().ViewID);

            yield return RetractGrab();
        }

        IEnumerator RotateTargetLeft(GameObject target)
        {
            GetComponent<GrabRotation>().Link(pinata.transform, target.transform);
            yield return new WaitWhile(() => grabRotX.JoystickValue <= -0.5f); // si le temps de rotation est timer rajouter ici
            GetComponent<GrabRotation>().ReleaseLeft();

            //pinata.EndGrab(target.GetComponent<PhotonView>().ViewID);

            yield return RetractGrab();
        }

        public void SetGrabActive(bool value)
        {
            if (value && cptListener == 0) {
                cptListener++;
                grabButton.onDown.AddListener(OnGrab);
            }
            else
            {
                cptListener = 0;
                grabButton.onDown.RemoveAllListeners();
            }
        }
    }
}
