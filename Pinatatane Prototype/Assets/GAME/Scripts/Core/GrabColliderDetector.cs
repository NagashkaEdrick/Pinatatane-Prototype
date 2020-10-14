using Pinatatane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColliderDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(IGrabable)))
        {
            Debug.Log(other.gameObject.name);
            GrabBehaviour grab = FindObjectOfType<GrabBehaviour>();
            grab.SetObjectGrabed(other.gameObject);
        }
    }
}
