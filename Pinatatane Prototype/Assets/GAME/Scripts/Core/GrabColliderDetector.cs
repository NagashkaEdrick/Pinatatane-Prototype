using Pinatatane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColliderDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        FindObjectOfType<GrabBehaviour>().SetObjectGrabed(other.gameObject);
    }
}
