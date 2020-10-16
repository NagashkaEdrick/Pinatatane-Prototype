using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pinatatane
{
    public class NetworkInfoManager : UIMenu
    {
        public static NetworkInfoManager Instance;

        public TextMeshProUGUI
            playerName,
            isReady,
            score;

        bool isOpen = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (isOpen)
                    Show();
                else
                    Hide();

                isOpen = !isOpen;
            }
        }
    }
}