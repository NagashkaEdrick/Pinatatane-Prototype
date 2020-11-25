using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class RotationConstraint : MonoBehaviour
    {
        [Header("Contraintes")]
        [SerializeField] bool x;
        [SerializeField] bool y;
        [SerializeField] bool z;

        [SerializeField] Vector3 contraint;

        private void Update()
        {
            if (x)
                transform.rotation = Quaternion.Euler(contraint.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            if (y)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, contraint.y, transform.rotation.eulerAngles.z);
            if (z)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, contraint.z);
        }
    }
}