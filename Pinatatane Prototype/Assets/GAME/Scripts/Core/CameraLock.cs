using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLock : MonoBehaviour
{
    [SerializeField]
    private string inputLockCam;
    private string rotateYAxisName;
    public float YAxisLockValue = 0.5f; //Valeur Y ou la camera dois se lock
    private bool lockOnCam = false; //Lorsque la Camera est Lock
    private bool setDefault = true; //Lorsque les valeurs ont été remis à la normale

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
        rotateYAxisName = this.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName;
    }

    private void LockCamSystem()
    {
        if (lockOnCam)
        {
            setDefault = false;
            this.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = null;
            this.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0;
            this.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.Value = YAxisLockValue;
        }

        else if (!setDefault && !lockOnCam)
        {
            this.gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = rotateYAxisName;
            setDefault = true;
        }
    }
}