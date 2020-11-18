using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;

namespace GameplayFramework
{
    /// <summary>
    /// This class defines rules of the game and the differents states of the game (Menu / Game / Lobby / ...)
    /// </summary>
    public class GameState : MonobehaviourSingleton<GameState>
    {
        //Rules à un moment donné du jeu
        //Etat du jeu à un moment donné

        /// <summary>
        /// If the game is on break.
        /// </summary>
        public bool OnBreak { get; set; } = false;
        
        [SerializeField] GameStateEnum m_GameState_Previous;
        public GameStateEnum GameState_Previous
        {
            get => m_GameState_Previous;
            set
            {
                m_GameState_Previous = value;
            }
        }

        [SerializeField] GameStateEnum m_GameState_Current;
        public GameStateEnum GameState_Current
        {
            get => m_GameState_Current;
            set
            {
                m_GameState_Previous = m_GameState_Current;
                m_GameState_Current = value;
            }
        }

        /// <summary>
        /// This state machine manage the changement of states
        /// </summary>
        public StateMachine<GameState> states;
    }

    public enum GameStateEnum
    {
        SplashScene = 0,
        InMenu = 1,
        InGame = 2
        //Ect
    }
}