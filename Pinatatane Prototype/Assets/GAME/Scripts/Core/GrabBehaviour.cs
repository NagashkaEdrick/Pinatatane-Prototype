﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace Pinatatane
{
    public class GrabBehaviour : MonoBehaviour
    {
        private Coroutine grabCor = null;

        public float duration = 1f;
        public float speed = 10f;
        public Camera _camera;

        public Image crossHair;
        [Range(0f, 1f)]
        public float crossHairPosition = 0.5f;
        public float length = 15f;

        public int numberOfSegment = 10;
        private Stack<Vector3> segments = new Stack<Vector3>();
        private Stack<GameObject> maillons = new Stack<GameObject>();

        /*public GameObject maillon;*/

        int cpt; //nbre de segments deja cree

        CharacterMovementBehaviour cc => PlayerManager.Instance.LocalPlayer.characterMovementBehaviour;
        AnimatorBehaviour animator => PlayerManager.Instance.LocalPlayer.animatorBehaviour;

        private void Update() {
            crossHair.transform.position = new Vector3(crossHair.transform.position.x, crossHairPosition * Screen.height, 0);
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
            segments.Clear();
            maillons.Clear();
            segments.Push(transform.position);
            maillons.Push(transform.GetChild(0).gameObject);
            cpt = 0;

            Vector3 crossHairPos = new Vector3(crossHair.transform.position.x, crossHair.transform.position.y, 0);
            Ray ray = _camera.ScreenPointToRay(crossHairPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, length))
            {
                Debug.DrawRay(ray.origin, ray.direction * length, Color.red);
                if (Input.GetKeyDown(KeyCode.Keypad0)) Debug.Log(hit.collider.name);
            }
            else
            {
                Vector3 dest = ray.origin + ray.direction.normalized * length;
                float distance = Vector3.Distance(segments.Peek(), dest); //distance courante entre le grab et la destination
                float lenghtSegment = distance / numberOfSegment;
                while (cpt < numberOfSegment)
                {
                    yield return new WaitForSeconds(duration / numberOfSegment);
                    GameObject maillon = maillons.Peek();
                    GameObject o = Instantiate(maillon, maillon.transform, true);
                    o.transform.Translate((dest - o.transform.position).normalized * lenghtSegment, Space.World);
                    maillons.Push(o);
                    cpt++;
                }
                while (cpt > 0)
                {
                    yield return new WaitForSeconds(duration / numberOfSegment);
                    Destroy(maillons.Pop());
                    cpt--;
                }
            }

            grabCor = null;
            animator.SetBool("grab", false);
            cc.setMovementActive(true);
            yield break;
        }
    }
}
