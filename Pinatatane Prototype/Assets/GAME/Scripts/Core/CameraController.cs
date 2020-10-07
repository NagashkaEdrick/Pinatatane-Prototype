using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    /* CAMERA CONTROLLER :
     * La camera reste fixe derriere le joueur
     */
    public class CameraController : SerializedMonoBehaviour
    {
        [BoxGroup("Tweaking")]
        public FloatValue distance, height;

        [BoxGroup("Fix")]
        public Transform target;

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position - distance.value * target.forward + height.value * Vector3.up;

            transform.LookAt(target);
        }
    }
}