using Sirenix.OdinInspector;
using UnityEngine;

namespace GameplayFramework
{
    public abstract class MyMonoBehaviour : SerializedMonoBehaviour
    {
        [SerializeField] ButtonMail buttonMail;


        #region Runtime Functions
        private void Awake() => OnAwake();        
        private void Start() => OnStart();
        private void Update() => OnUpdate();
        #endregion

        #region Callbacks Initialisation
        private void OnEnable()
        {
            if (Game.Instance != null)
            {
                Game.Instance.OnGameStartCallbacks += OnGameStart;
                Game.Instance.OnGameEndCallbacks += OnGameEnd;
                Game.Instance.OnGameBreakEnter += GameBreakEnter;
                Game.Instance.OnGameBreakExit += GameBreakExit;
            }
        }

        private void OnDisable()
        {
            if (Game.Instance != null)
            {
                Game.Instance.OnGameStartCallbacks -= OnGameStart;
                Game.Instance.OnGameEndCallbacks -= OnGameEnd;
                Game.Instance.OnGameBreakEnter -= GameBreakEnter;
                Game.Instance.OnGameBreakExit -= GameBreakExit;
            }
        }
        #endregion

        #region Runtime Callbacks
        /// /// <summary>
        /// Call at awake
        /// </summary>
        public virtual void OnAwake() { }
        /// <summary>
        /// Call at first frame
        /// </summary>
        public virtual void OnStart() { }
        /// <summary>
        /// Call Every frame
        /// </summary>
        public virtual void OnUpdate()
        {

        }
        #endregion

        #region MyMonobehaviour CallBacks
        protected abstract void OnGameStart();
        protected abstract void OnGameEnd();
        protected virtual void GameBreakEnter() { }
        protected virtual void GameBreakExit() { }
        #endregion
    }
}