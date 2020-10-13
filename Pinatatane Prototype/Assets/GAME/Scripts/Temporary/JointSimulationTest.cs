using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class JointSimulationTest : MonoBehaviour
{
    public AnimationCurve myCurve;
    public float maxAmplitude = 90f;

    public Transform[] mySpheres;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
            RefreshOndulation();
    }

    void RefreshOndulation()
    {
        for (int i = 0; i < mySpheres.Length; i++)
        {
            float amplitude = myCurve.Evaluate(((float)i) / (float)mySpheres.Length);
            mySpheres[i].localEulerAngles = Vector3.right * maxAmplitude * amplitude;
            //mySpheres[i].Rotate(Vector3.right, maxAmplitude * amplitude, Space.Self);
        }
    }
}
