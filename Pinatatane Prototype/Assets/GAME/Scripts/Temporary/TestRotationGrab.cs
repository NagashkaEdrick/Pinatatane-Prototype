using Pinatatane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationGrab : MonoBehaviour
{

    public CharacterMovementBehaviour c;
    public Rigidbody r;
    float lastRotation = 0f;
    float ro;
    float tan;
    Coroutine cor = null;

    private void Update()
    {
        if (cor == null && InputManagerQ.Instance.GetAxis("RotationX") != 0) cor = StartCoroutine(rotateCor());

    }

    IEnumerator rotateCor()
    {
        Debug.Log("Start");
        while (InputManagerQ.Instance.GetAxis("RotationX") != 0)
        {
            yield return new WaitForSeconds(0.02f);
            lastRotation %= 360;
            ro = c.getRotationAngle();
            transform.RotateAround(c.transform.position, Vector3.up, ro - lastRotation);
            float tmp = Mathf.Tan((ro - lastRotation) * Mathf.Deg2Rad);
            if (tmp != 0) tan = tmp;
            lastRotation = ro;
        }
        cor = null;
        Debug.Log("Stop " + tan);
        r.AddForce(tan * (c.transform.right * 10000 * c.data.rotationAcceleration));
    }
}
