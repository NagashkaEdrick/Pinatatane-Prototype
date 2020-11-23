using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    /// <summary>
    /// Player class represent a pawn controlled by the player
    /// </summary>
    public class Player : Pawn
    {
        public PlayerData PlayerData;

        public override void OnStart()
        {
            base.OnStart();
            Controller.RegisterPawn(this);
        }

        public override void OnRegisterOnController()
        {
            base.OnRegisterOnController();
        }

        public override void OnUnregisterOnController()
        {
            base.OnUnregisterOnController();
        }
    }
}