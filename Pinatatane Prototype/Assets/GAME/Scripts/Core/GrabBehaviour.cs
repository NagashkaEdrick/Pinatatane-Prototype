using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            if(objectGrabed != null)
            {
                objectGrabed.GetComponent<Rigidbody>().AddForce((links[cptLink].transform.position - objectGrabed.transform.position) * attractionForce);
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
                // a terme, separer en 2 script le mouvement et la rotation et desactiver le mouvement durant toutes la durée du dash et la rotation uniquement durant l'aller
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
            links[0].AddComponent<GrabColliderDetector>();
            while (cptLink < numberOfLink && InputManagerQ.Instance.GetTrigger("RightTrigger"))
            {
                yield return new WaitForSeconds(duration / numberOfLink);
                GameObject link = Instantiate(links[cptLink], links[cptLink].transform, true);
                link.name = "Maillon " + cptLink;
                Destroy(links[cptLink].GetComponent<SphereCollider>());
                Destroy(links[cptLink].GetComponent<GrabColliderDetector>());
                link.transform.Translate((destinationPoint - link.transform.position).normalized * lenghtBetweenLink, Space.World);
                links[++cptLink] = link;
            }
            yield return RetractGrab();
            /*GrabColliderDetector detector = links[cptLink].GetComponent<GrabColliderDetector>();
            detector.OnObjectGrabed += OnObjectGrabedAction;*/
        }

        private void OnObjectGrabedAction(GameObject objectGrabed)
        {
            Debug.Log(objectGrabed.name);
            StartCoroutine(RetractGrab());
        }

        IEnumerator RetractGrab()
        {
            cc.setRotationActive(true);
            while (cptLink > 0)
            {
                yield return new WaitForSeconds(duration / numberOfLink);
                links[cptLink].transform.position = links[cptLink - 1].transform.position;
                links[cptLink].transform.SetParent(links[cptLink].transform.parent.transform.parent);
                Destroy(links[cptLink - 1]);
                links[cptLink - 1] = links[cptLink];
                links[cptLink--] = null;
            }
            Destroy(links[0].GetComponent<GrabColliderDetector>());
            objectGrabed = null;
        }

        public void SetObjectGrabed(GameObject o)
        {
            objectGrabed = o;
        }
    }
}
