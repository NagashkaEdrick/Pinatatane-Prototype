using Pinatatane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColliderDetector : MonoBehaviour
{

    public event Action<GameObject, string> OnObjectGrabed;
    GrabBehaviour grabBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(IGrabable)))
        {
            //OnObjectGrabed?.Invoke(other.gameObject, other.GetComponent<Pinata>().ID);
            FindObjectOfType<GrabBehaviour>().SetObjectGrabed(other.gameObject);

            grabBehaviour.GetGrabInfo(other.GetComponent<Pinata>().ID);
        }
    }
}
