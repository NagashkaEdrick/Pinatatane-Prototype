using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoving : MonoBehaviour
{
    bool b = false;
    public SimplePhysic body;
    public float f;
    float movingForce = 0;

    private void Start()
    {
        StartCoroutine(cor1());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - movingForce > 3f)
        {
            movingForce = Time.time;
            b = !b;
        }
    }

    IEnumerator cor1()
    {
        while (true)
        {
            if (b) body.AddForce(new Vector3(f, 0, 0));
            else body.AddForce(new Vector3(-f, 0, 0));
            yield return new WaitForSeconds(0.2f);
        }
    }
}
