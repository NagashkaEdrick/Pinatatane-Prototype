using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GrabableObject : MonoBehaviour, IGrabable
{
    [SerializeField] PhotonView photonView = default;
    public PhotonView PhotonView { get => photonView; set => photonView = value; }
    [SerializeField] bool canBeGrabbed = true;
    public bool CanBeGrabbed { get => canBeGrabbed; set => canBeGrabbed = value; }

    public void EndGrab(int _cible)
    {
    }

    public void OnGrab(int _cible, int _attaquant)
    {
    }

    public void StartGrab(int _cible)
    {
    }
}
