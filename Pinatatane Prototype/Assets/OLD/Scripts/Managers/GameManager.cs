using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameSettings gameSettings;

        private void Awake()
        {
            Instance = this;
        }
    }
}