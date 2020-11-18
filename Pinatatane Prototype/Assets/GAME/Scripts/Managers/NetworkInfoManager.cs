using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OldPinatatane
{
    public class NetworkInfoManager : UIMenu
    {
        /*
         * Ne sera pas dans le build final
         * Sert de debug
         */ 

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

        public void Debug() //Uniquement le joueur local recoit ce debug
        {

        }

        public void DebugNetwork() //Tous les joueurs recoivent ce debug
        {

        }
    }
}