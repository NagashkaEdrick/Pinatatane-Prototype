using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Pinatatane
{
    public class AllPlayersAreReady : MonoBehaviourCondition
    {
        public override bool IsValidate()
        {
            if (PlayerManager.Instance.pinatas.Count >= 2 && PlayerManager.Instance.pinatas != null)
            {
                for (int i = 0; i < PlayerManager.Instance.pinatas.Count; i++)
                {
                    /*if (!PlayerManager.Instance.pinatas[i].isReady)
                        return false;*/
                }
            }
            return true;
        }
    }
}