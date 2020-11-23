using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

public class PoolObjectA : MonoBehaviour, IPoolable
{
    public bool IsPool { get; set; }

    public void OnPool()
    {
    }

    public void OnPush()
    {
    }
}
