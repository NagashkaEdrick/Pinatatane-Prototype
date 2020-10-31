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

        [SerializeField, BoxGroup("Debug", order: 100), ReadOnly] float joystickValue;
        public float JoystickValue
        {
            get => joystickValue;
            set => joystickValue = value;
        }

        public override float TestAxis()
        {
            switch (axisType)
            {
                case AxisType.GETAXIS:
                    JoystickValue = Input.GetAxis(axisName);
                    break;
                case AxisType.GETAXISRAW:
                    JoystickValue = Input.GetAxisRaw(axisName);
                    break;
            }

            onJoystickMove?.Invoke(joystickValue);
            return joystickValue;
        }

        public override void TestInput()
        {
            TestAxis();
        }
    }

    public enum AxisType
    {
        GETAXIS,
        GETAXISRAW
    }

    public class FloatEvent: UnityEvent<float> { }
}