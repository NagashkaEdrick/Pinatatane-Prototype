using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pinatatane;


public class TestNormal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.DrawLine(collision.GetContact(0).point, collision.GetContact(0).point + collision.GetContact(0).normal, Color.cyan);
        Debug.DrawRay(collision.GetContact(0).point, collision.GetContact(0).normal, Color.red);
        Debug.Log("normal: " + collision.GetContact(0).normal);

        collision.collider.gameObject.GetComponent<Pinata>().Rigidbody.AddForce(-collision.GetContact(0).normal * 1000);
    }
}
