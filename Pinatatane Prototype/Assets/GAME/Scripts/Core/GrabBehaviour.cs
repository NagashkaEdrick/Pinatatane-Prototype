using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class GrabBehaviour : MonoBehaviour
    {
        private Coroutine grabCor = null;

        public float duration = 1f;
        public float speed = 10f;
        public Camera _camera;
        public float aim = 0.5f;
        public Transform hand;

        public float test = 0.0f;

        Vector3 intersection = new Vector3(0, 0, 0);
        bool grab = false;

        CharacterMovementBehaviour cc => PlayerManager.Instance.LocalPlayer.characterMovementBehaviour;
        AnimatorBehaviour animator => PlayerManager.Instance.LocalPlayer.animatorBehaviour;

        private void Update() {
            Debug.DrawRay(_camera.transform.position + new Vector3(0, aim, 0), _camera.transform.forward, Color.red);
            Debug.DrawRay(cc.transform.position, cc.transform.forward, Color.yellow);
            bool b = LinesIntersection(out intersection,
                              _camera.transform.position + new Vector3(0, aim, 0), _camera.transform.forward,
                              cc.transform.position, cc.transform.forward);
            if (b) Debug.DrawLine(intersection, intersection + new Vector3(0, 1f, 0), Color.red);
            else Debug.Log("no intersection");

            if (grab && Vector3.Distance(intersection, transform.position) > test) {
                Debug.Log("go");
                //transform.position = Vector3.Lerp(transform.position, intersection, speed * Time.deltaTime);
                transform.forward = intersection - transform.position;
                transform.Translate(transform.forward * Time.deltaTime * speed);
            } else if (Vector3.Distance(intersection, transform.position) <= test) {
                Debug.Log("return");
                transform.forward = hand.position - transform.position;
                transform.Translate(transform.forward * Time.deltaTime * speed);
                //transform.position = Vector3.Lerp(transform.position, hand.position, speed * Time.deltaTime);
            } else {
                transform.position = hand.position;
            }
        }

        public void GrabAction()
        {
            if (grabCor == null)
            {
                grabCor = StartCoroutine(LaunchGrab());
            }
        }

        IEnumerator LaunchGrab()
        {
            animator.SetBool("grab", true);
            cc.setMovementActive(false);
            yield return new WaitForSeconds(animator.animator.GetCurrentAnimatorStateInfo(0).length);

            // grab
            grab = true;

            yield return new WaitForSeconds(duration);
            grab = false;
            grabCor = null;
            animator.SetBool("grab", false);
            cc.setMovementActive(true);
            yield break;
        }

        public static bool LinesIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVector1, Vector3 linePoint2, Vector3 lineVector2) 
        {

            Vector3 lineVec3 = linePoint2 - linePoint1;
            Vector3 crossVec1and2 = Vector3.Cross(lineVector1, lineVector2);
            Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVector2);

            float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

            //est coplanaire et pas parallele
            //Debug.Log(planarFactor + "  " + crossVec1and2.sqrMagnitude + "   " + Mathf.Approximately(planarFactor, 0f) + "   " + Mathf.Approximately(crossVec1and2.sqrMagnitude, 0f));
            /* Probleme: Mathf.Approximately(planarFactor, 0f) retourne false car il y a un minuscule decalage de la camera par rapport au joueur et les 2 vecteur ne sont plus coplanaire
             *           le decalage est si minime qu'on peux se passer du test, seulement si on utilise pas le slerp, le slerp fais que la camera prend du retard sur la rotation du joueur
             *           et donc les deux vecteur (forward camera, forward joueur) ne sont plus du tout coplanaire ce qui rapporhce enormement le point d'intersection du joueur
             *           ce qui provoquerai une distance de grab bien moins grande */
            if (/*Mathf.Approximately(planarFactor, 0f) &&*/ !Mathf.Approximately(crossVec1and2.sqrMagnitude, 0f))
            {
                float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
                intersection = linePoint1 + (lineVector1 * s);
                return true;
            } else 
            {
                intersection = Vector3.zero;
                return false;
            }
        }
    }
}
