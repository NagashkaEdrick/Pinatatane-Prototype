using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework.Singletons;

namespace GameplayFramework
{
    public class UIManager : MonobehaviourSingleton<UIManager>
    {
        public Dictionary<string, UIMenu> menus = new Dictionary<string, UIMenu>();

        public UIMenu GetMenu(string menuName)
        {
            UIMenu _menu = null;
            menus.TryGetValue(menuName, out _menu);
            return _menu;
        }
    }
}