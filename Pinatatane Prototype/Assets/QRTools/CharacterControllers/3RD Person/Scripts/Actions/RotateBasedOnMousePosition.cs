using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/PlayerControllers/Actions/RotateBasedOnMousePosition", fileName = "RotateBasedOnMousePosition")]
    public class RotateBasedOnMousePosition : Behaviour<Transform>
    {
        public QInputMouse mouseInput = default;
        public float speed = 8;

        public override void Execute(Transform element)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetdir = hit.point - element.position;
                targetdir.Normalize();
                targetdir.y = 0;

                Quaternion tr = Quaternion.LookRotation(targetdir);
                Quaternion targetrot = Quaternion.Slerp(
                    element.transform.rotation,
                    tr,
                    Time.deltaTime * speed);

                element.transform.rotation = targetrot;
            }
        }

        public override void Init(Transform element)
        {

        }
    }
}