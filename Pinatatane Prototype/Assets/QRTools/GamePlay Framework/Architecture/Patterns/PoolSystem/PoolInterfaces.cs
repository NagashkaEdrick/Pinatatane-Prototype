using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    /// <summary>
    /// Interface to tag an object if it's can be pooled
    /// </summary>
    public interface IPoolable
    {
        bool IsPool { get; set; }

        void OnPool();
        void OnPush();
    }
}