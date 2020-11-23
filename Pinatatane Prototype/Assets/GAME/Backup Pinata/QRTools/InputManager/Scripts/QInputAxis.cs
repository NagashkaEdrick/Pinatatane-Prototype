using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QRTools.Inputs
{
    public class QInputAxis : QInputs
    {
        [SerializeField, BoxGroup("Input", order: 5)] string axisName;
        [BoxGroup("Input", order: 5)] public AxisType axisType = AxisType.GETAXIS;

        [BoxGroup("Events", order: 50)]
        public FloatEvent
            onJoystickMove = new FloatEvent();
        [BoxGroup("Events", order: 50)]
        public UnityEvent
            onJoystickStartMove = new UnityEvent(),
            onJoystickEndMove = new UnityEvent();

        [SerializeField, BoxGroup("Debug", order: 100), ReadOnly] float joystickValue;
        public float JoystickValue
        {
            get => joystickValue;
            set => joystickValue = value;
        }

        [Range(0,2), BoxGroup("Options", order:50)]
        public float sensibility = 1f;
        [Range(0, 1), BoxGroup("Options", order: 50)]
        public float lerpSensibility = .2f;

        [BoxGroup("Options", order: 50), ShowIf("@this.axisType == AxisType.BUTTON_AXIS")]
        public KeyCode
            positiveKey = KeyCode.Z,
            negativeKey = KeyCode.S;

        float previousJoystickValue = 0;
        float p, n;

        public override float TestAxis()
        {
            switch (axisType)
            {
                case AxisType.GETAXIS:
                    JoystickValue = Mathf.Lerp(JoystickValue, Input.GetAxis(axisName) * sensibility, lerpSensibility);
                    break;
                case AxisType.GETAXISRAW:
                    JoystickValue = Mathf.Lerp(JoystickValue, Input.GetAxisRaw(axisName) * sensibility, lerpSensibility); ;
                    break;
                case AxisType.BUTTON_AXIS:
                    JoystickValue = TestAxisWithInput() * sensibility;
                    break;
            }

            if (JoystickValue == 0 && previousJoystickValue != JoystickValue)
                onJoystickEndMove?.Invoke();

            if (JoystickValue != 0 && previousJoystickValue == 0)
                onJoystickStartMove?.Invoke();

            onJoystickMove?.Invoke(JoystickValue);
            previousJoystickValue = JoystickValue;
            return joystickValue;
        }

        public override void TestInput()
        {
            TestAxis();
        }

        float TestAxisWithInput()
        {
            float _p = Input.GetKey(positiveKey) ? 1 : 0;
            float _n = Input.GetKey(negativeKey) ? -1 : 0;

            p = Mathf.Lerp(p, _p, lerpSensibility);
            n = Mathf.Lerp(n, _n, lerpSensibility);

            return n + p;
        }
    }

    public enum AxisType
    {
        GETAXIS,
        GETAXISRAW,
        BUTTON_AXIS
    }

    public class FloatEvent: UnityEvent<float> { }
}