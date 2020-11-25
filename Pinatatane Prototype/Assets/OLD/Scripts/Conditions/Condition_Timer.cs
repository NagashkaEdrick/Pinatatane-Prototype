using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace OldPinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Conditions/TimerCondition", fileName = "Timer Condition")]
    public class Condition_Timer : Condition
    {
        [SerializeField] float timerStart = 20;
        [SerializeField] float timer;
        [SerializeField] bool isFinish = false;

        [SerializeField] bool timerAsStart = false;

        public override bool TestCondition()
        {
            if (!timerAsStart)
                StartTimer();

            return isFinish;
        }

        void StartTimer()
        {
            MonoBehaviour m = FindObjectOfType<MonoBehaviour>();
            timerAsStart = true;
            m.StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            isFinish = false;
            timer = timerStart;
            while(timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            isFinish = true;
            timerAsStart = false;
            yield break;
        }

        public void Reset()
        {
            timer = 0;
            isFinish = false;
            timerAsStart = false;
        }
    }
}