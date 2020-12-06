using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Pinatatane;
using Photon.Pun;

public class GrabbableTest : MonoBehaviour, IGrabbable
{
    [SerializeField] Transform m_Transform;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] Lasso m_GrabbedBy;    

    public Transform Transform { get => m_Transform; set => m_Transform = value; }

    public Rigidbody Rigidbody { get => m_Rigidbody; set => m_Rigidbody = value; }

    public Lasso GrabbedBy { get => m_GrabbedBy; set => m_GrabbedBy = value; }

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
