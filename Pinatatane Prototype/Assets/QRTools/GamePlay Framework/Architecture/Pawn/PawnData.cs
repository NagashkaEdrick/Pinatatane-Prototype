using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameplayFramework
{
    [System.Serializable]
    public class PawnData
    {

    }

    [System.Serializable]
    public class PlayerData : PawnData
    {
        public float speed = 5f;
    }
}