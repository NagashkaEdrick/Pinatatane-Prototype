using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    //All Interfaces used in GameplayFramework

        /// <summary>
        /// Permet to define EveryThing As Pawn
        /// </summary>
    public interface IPawn
    {
        Transform PawnTransform { get; set; }

        Controller Controller { get; set; }

        void OnRegisterOnController();

        void OnUnregisterOnController();
    }
}