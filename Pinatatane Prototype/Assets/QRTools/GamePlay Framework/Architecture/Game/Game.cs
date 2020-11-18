using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;

namespace GameplayFramework {
    /// <summary>
    /// This Class is use to manage scene and call some callbacks
    /// </summary>
    public class Game : MonobehaviourSingleton<Game>
    {
        [SerializeField, BoxGroup("Game Mode & Game State References", order: 0)] 
        GameMode m_GameMode = default;
        public GameMode GameMode { get => m_GameMode; set => m_GameMode = value; }

        [SerializeField, BoxGroup("Game Mode & Game State References", order: 0)] 
        GameState m_GameState = default;
        public GameState GameState { get => m_GameState; set => m_GameState = value; }

        //[FoldoutGroup("Game CallBacks", order: 10)]
        //public UnityEvent OnGameStart = new UnityEvent();

        [FoldoutGroup("Game CallBacks", order: 10)]
        public Action
            OnGameStartCallbacks,
            OnGameEndCallbacks,
            OnGameBreakEnter,
            OnGameBreakExit;

        [FoldoutGroup("Scenes", order: 20)]
        public SceneReference[] scenes;

        /// <summary>
        /// Call OnGameStart() on all MyMonoBehaviour.
        /// </summary>
        [ButtonGroup("Game Callbacks", order: 30)]
        public void StartGame()
        {
            OnGameStartCallbacks?.Invoke();
        }

        /// <summary>
        /// Call OnGameEnd() on all MyMonoBehaviour.
        /// </summary>
        [ButtonGroup("Game Callbacks", order: 1000)]
        public void EndGame()
        {
            OnGameEndCallbacks?.Invoke();
        }

        [ButtonGroup("Game Callbacks", order: 1000)]
        /// <summary>
        /// Call OnGameBreakEnter() on all MyMonoBehaviour.
        /// </summary>
        public bool OnBreak() => OnBreak(!m_GameState.OnBreak);

        /// <summary>
        /// Call OnGameBreakEnter() on all MyMonoBehaviour.
        /// </summary>
        public bool OnBreak(bool state)
        {
            m_GameState.OnBreak = state;

            if (state)
            {
                OnGameBreakEnter?.Invoke();
            }
            else
            {
                OnGameBreakExit?.Invoke();
            }

            return state;
        }

        /// <summary>
        /// Add a scene to the main scene in additive.
        /// </summary>
        /// <param name="_scene"></param>
        public void AddSceneToMainScene(Scene _scene)
        {
            SceneManager.LoadScene(_scene.name, LoadSceneMode.Additive);
        }
    }
}