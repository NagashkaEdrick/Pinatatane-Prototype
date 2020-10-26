using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;
using TMPro;

namespace Pinatatane
{
    public class UIManager : SerializedMonoBehaviour
    {
        /*
         * Gestion des différents menus
         * Gestion des différents elements d'UI
         * Gestion du refresh des UI
         * Assigne les éléments d'UI aux objets instanciés
         */

        public static UIManager Instance;

        public Dictionary<string,UIMenu> menus;

        public TextMeshProUGUI currentScore;

        public NetworkStatutText networkStatutElement;

        public Image crossHair;

        private void Awake()
        {
            Instance = this;
        }

        public UIMenu FindMenu(string key)
        {
            UIMenu menuToRtn = null;
            menus.TryGetValue(key, out menuToRtn);
            return menuToRtn;
        }

        public T FindMenu<T>(string key) where T : UIMenu
        {
            UIMenu menuToRtn = null;
            menus.TryGetValue(key, out menuToRtn);
            return menuToRtn as T;
        }
    }
}