using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay
{
    /* CAMERA CONTROLLER :
     * La camera reste fixe derriere le joueur
     */
    public class CameraController : SerializedMonoBehaviour
    {

        public Transform target;
        public float distance, height;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position - distance * target.forward + height * Vector3.up;

            transform.LookAt(target);
        }
    }
}