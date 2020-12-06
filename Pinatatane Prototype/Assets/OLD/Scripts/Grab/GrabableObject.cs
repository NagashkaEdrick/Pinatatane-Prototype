using System.Collections;
using System.Collections.Generic;

using Photon.Pun;

using Pinatatane;

using UnityEngine;

using GameplayFramework.Network;

public class GrabableObject : MonoBehaviour, IGrabbable, IBlockable, IReceiveDamages
{
    [SerializeField] Transform m_Transform;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] Lasso m_GrabbedBy;

    public Transform Transform { get => m_Transform; set => m_Transform = value; }
    public Rigidbody Rigidbody { get => m_Rigidbody; set => m_Rigidbody = value; }
    public Lasso GrabbedBy { get => m_GrabbedBy; set => m_GrabbedBy = value; }
    public bool IsBlocked { get; set; } = false;

    public float Velocity => Rigidbody.velocity.magnitude;

    public float Health { get; set; } = 1f;

    [SerializeField] NetworkSharedTransform lagCompensation;

    public void OnStartGrabbed()
    {
        lagCompensation.OverrideNetworkSharedTransform(lagCompensation);
    }

    public void OnCurrentGrabbed()
    {
        Debug.Log("Current grab");
    }

    public void OnEndGrabbed()
    {
        Debug.Log("End grab");
    }

    public void OnBlockedEnter()
    {
        GrabbedBy?.LassoController.KillGrab();
        Debug.Log("jaoijzodajodzjaopd");
    }

    public void OnBlockedExit()
    {
    }

    public void ReceivedDamage(float damageTaken)
    {
    }

    public void OnDeath()
    {
    }
}
