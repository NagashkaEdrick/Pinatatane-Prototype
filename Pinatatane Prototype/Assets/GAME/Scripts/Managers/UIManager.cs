using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using TMPro;

namespace Pinatatane
{
    public class UIManager : SerializedMonoBehaviour
    {
        public static UIManager Instance;

        public Dictionary<string,UIMenu> menus;

        public TextMeshProUGUI currentScore;

        public NetworkStatutText networkStatutElement;

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