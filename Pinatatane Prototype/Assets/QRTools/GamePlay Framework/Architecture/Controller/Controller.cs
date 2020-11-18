using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Sirenix.OdinInspector;

namespace GameplayFramework
{
    /// <summary>
    /// This Class is used for all objects can be controlled.
    /// </summary>
    public class Controller : MyMonoBehaviour
    {
        [SerializeField, ReadOnly, BoxGroup("Pawn(s) Register", order: 0)] 
        protected IPawn m_pawn;
        public IPawn Pawn { get => m_pawn; set => m_pawn = value; }

        [SerializeField, ReadOnly, BoxGroup("Pawn(s) Register", order: 0)] 
        protected List<IPawn> m_pawns = new List<IPawn>();
        public List<IPawn> Pawns { get => m_pawns; set => m_pawns = value; }

        [BoxGroup("Options", order: 10)]
        public bool IsActive = true; //True -> Control(); False -> !Control();

        public override void OnUpdate()
        {
            if (IsActive && m_pawn != null) Control(m_pawn);

            if (IsActive && m_pawns.Count > 0)
                for (int i = 0; i < m_pawns.Count; i++)
                    Control(m_pawns[i]);
        }

        /// <summary>
        /// This Function is used in Update, used to code the controller
        /// </summary>
        /// <param name="pawn"></param>
        public virtual void Control(IPawn pawn)
        {
            
        }

        /// <summary>
        /// Add a simple pawn, for a player for exemple
        /// </summary>
        /// <param name="_pawn"></param>
        /// <returns></returns>
        public IPawn RegisterPawn(IPawn _pawn)
        {
            m_pawn = _pawn;
            m_pawn?.OnRegisterOnController();
            return _pawn;
        }

        /// <summary>
        /// Remove the current pawn
        /// </summary>
        public void UnregisterPawn()
        {
            m_pawn.OnUnregisterOnController();
            m_pawn = null;
        }

        /// <summary>
        /// Add many pawns to the controllers, for IA Agents for exemple
        /// </summary>
        /// <param name="_pawns"></param>
        /// <returns></returns>
        public IPawn[] RegisterPawns(IPawn[] _pawns)
        {
            for (int i = 0; i < _pawns.Length; i++)
            {
                m_pawns.Add(_pawns[i]);
                _pawns[i].OnRegisterOnController();
            }

            return _pawns;
        }

        /// <summary>
        /// Remove many pawns
        /// </summary>
        /// <param name="_pawns"></param>
        public void UnregisterPawns(IPawn[] _pawns)
        {
            for (int i = 0; i < _pawns.Length; i++)
            {
                _pawns[i].OnUnregisterOnController();
                m_pawns.Remove(_pawns[i]);
            }
        }
        
        protected override void OnGameStart() { }

        protected override void OnGameEnd() { }
    }
}