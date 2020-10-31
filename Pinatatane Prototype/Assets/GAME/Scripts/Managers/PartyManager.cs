using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class PartyManager : MonoBehaviourStateMachine
    {
        /*
         * Gèrer les différentes phases de jeu et leurs transition (une genre de state machine)
         */

        public static PartyManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void OnJoinGame()
        {
            if (PlayerManager.Instance.hostID != 0)
                StartStateMachine();
        }
    }
}