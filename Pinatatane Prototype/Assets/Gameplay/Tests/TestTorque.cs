using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTorque : MonoBehaviour
{
    public Rigidbody body;
    public Transform p;
    Vector3 c;
    public float f;
    public float f2;

    // Start is called before the first frame update
    void Start()
    {
        c = body.centerOfMass;
        //body.centerOfMass = new Vector3(c.x - f2, c.y, c.z);
        body.centerOfMass = new Vector3(p.position.x, c.y, p.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        body.centerOfMass = new Vector3(p.position.x, c.y, p.position.z);
        body.AddTorque(Vector3.up * f);
    }
}
