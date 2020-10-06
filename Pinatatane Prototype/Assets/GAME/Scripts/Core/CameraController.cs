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
        public Vector3 translation;
        public Quaternion rotation;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
            transform.position += translation;
            transform.rotation *= rotation;
        }
    }
}