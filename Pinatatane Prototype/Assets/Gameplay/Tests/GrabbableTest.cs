using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Pinatatane;
using Photon.Pun;

public class GrabbableTest : MonoBehaviour, IGrabbable
{
    [SerializeField] Transform m_Transform;
    public Transform Transform { get => m_Transform; set => m_Transform = value; }

    public void OnCurrentGrabbed()
    {
        Debug.Log("Start grab");
    }

    public void OnEndGrabbed()
    {
        Debug.Log("current grab");
    }

    public void OnStartGrabbed()
    {
        Debug.Log("end grab");
    }
}
