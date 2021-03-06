﻿using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using Photon.Pun;

namespace Pinatatane
{
    public class PlayerNameText : UIElement
    {
        public TextMeshProUGUI text;
        public Pinata pinata;
        public Billboard billboard;

        public override void Refresh()
        {
            base.Refresh();

            billboard.camTransform = pinata.cameraController.transform;

            if (pinata.photonView.IsMine)
            {
                Hide();
            }
        }
    }
}