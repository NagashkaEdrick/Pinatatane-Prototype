using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            GetComponent<Rigidbody>().AddForce(new Vector3(10f, 0, 0));
    }
}
