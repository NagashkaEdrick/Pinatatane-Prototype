﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class PartyManager : MonoBehaviour
    {
        public static PartyManager Instance;

        private void Awake()
        {
            Instance = this;

            StartCoroutine(GameDeroulement());
        }

        IEnumerator GameDeroulement()
        {
            yield break;
        }
    }
}