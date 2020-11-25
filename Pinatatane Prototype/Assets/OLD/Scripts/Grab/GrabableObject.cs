using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Pinatatane;
using UnityEngine;

public class GrabableObject : MonoBehaviour, IGrabbable
{
    [SerializeField] Transform m_Transform;
    [SerializeField] Rigidbody m_Rigidbody;

    public Transform Transform { get => m_Transform; set => m_Transform = value; }
    public Rigidbody Rigidbody { get => m_Rigidbody; set => m_Rigidbody = value; }

    public void OnStartGrabbed()
    {
        Debug.Log("Start grab");
    }

    public void OnCurrentGrabbed()
    {
        Debug.Log("Current grab");
    }

    public void OnEndGrabbed()
    {
        Debug.Log("End grab");
    }

}
