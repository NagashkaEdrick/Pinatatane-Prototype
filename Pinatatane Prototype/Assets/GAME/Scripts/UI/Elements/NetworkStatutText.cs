﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OldPinatatane
{
    public class NetworkStatutText : UIText
    {

        public void Online()
        {
            SetText("Online", Color.green);
        }

        public void OffLine()
        {
            SetText("Offline", Color.red);
        }
    }
}