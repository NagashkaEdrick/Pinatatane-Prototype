using Pinatatane;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColliderDetector : MonoBehaviour
{

    public event Action<GameObject> OnObjectGrabed;
    TestScriptGetGrab tg;
    Pinata p;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(IGrabable)))
        {
            //OnObjectGrabed?.Invoke(other.gameObject);
            FindObjectOfType<GrabBehaviour>().SetObjectGrabed(other.gameObject);

            //tg.GetGrab(p.player.);
        }
    }
}
