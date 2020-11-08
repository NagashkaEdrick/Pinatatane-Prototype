using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pinatatane;

public class CameraLock : MonoBehaviour
{
    [SerializeField]
    private string inputLockCam;
    private string rotateYAxisName;
    public float YAxisLockValue = 0.5f; //Valeur Y ou la camera dois se lock
    private bool lockOnCam = false; //Lorsque la Camera est Lock
    private bool setDefault = true; //Lorsque les valeurs ont été remis à la normale

    [SerializeField]
    CinemachineFreeLook cinemachine;

    private void Start()
    {
        Initi();
    }

    private void Update()
    {
        if (Input.GetButton(inputLockCam))
            lockOnCam = true;
        else
            lockOnCam = false;

        LockCamSystem();
    }

    private void Initi()
    {
        rotateYAxisName = cinemachine.m_YAxis.m_InputAxisName;
    }

    private void LockCamSystem()
    {
        if (lockOnCam)
        {
            setDefault = false;
            cinemachine.m_YAxis.m_InputAxisName = null;
            cinemachine.m_YAxis.m_InputAxisValue = 0;
            cinemachine.m_YAxis.Value = YAxisLockValue;
        }

        else if (!setDefault && !lockOnCam)
        {
            cinemachine.m_YAxis.m_InputAxisName = rotateYAxisName;
            setDefault = true;
        }
    }
}