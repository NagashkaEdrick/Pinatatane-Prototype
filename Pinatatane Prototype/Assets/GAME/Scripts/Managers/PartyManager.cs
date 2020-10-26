using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class PartyManager : MonoBehaviour
    {
        /*
         * Gèrer les différentes phases de jeu et leurs transition (une genre de state machine)
         */

        public static PartyManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}