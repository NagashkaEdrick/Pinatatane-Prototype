using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class PartyManager : MonoBehaviour
    {
        public static PartyManager Instance;

        public StateMachine stateMachine;

        [SerializeField] State[] gameStates;

        public Condition_Timer timerCondition;

        private void Awake()
        {
            Instance = this;

            timerCondition.Reset();

            stateMachine = new StateMachine(gameStates);
        }

        [Button]
        void TestCurrentCondition()
        {
            stateMachine.TestCurrentStateCondition();
        }
    }
}