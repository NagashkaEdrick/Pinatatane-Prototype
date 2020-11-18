using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OldPinatatane
{
    public class PinataUI : MonoBehaviour
    {
        /*
         * Gère l'UI de la pinata
         * Amener à disparaitre
         */

        [FoldoutGroup("References", order: 0)]
        public UIElement[] playerUIelements;
        [FoldoutGroup("References", order: 0)]
        public TextMeshProUGUI playerName;

        public void InitPlayerUI()
        {
            for (int i = 0; i < playerUIelements.Length; i++)
                playerUIelements[i].Refresh();
        }
    }
}