using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using OldPinatatane.Utilities;

namespace OldPinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Settings/Game Settings", fileName = "Game Settings")]
    public class GameSettings : ScriptableObjectSingleton<GameSettings>
    {
        [SerializeField]
        private string gameVersion = "0.0.0";
        public string GameVersion { get => gameVersion; }
    }
}