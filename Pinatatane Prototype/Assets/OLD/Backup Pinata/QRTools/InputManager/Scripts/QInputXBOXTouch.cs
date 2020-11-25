using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Inputs/XBOX/Buttons", fileName = "New XBOX Button")]
    public class QInputXBOXTouch : QInputsTouch
    {
        [BoxGroup("Input", order: 5)]
        public XboxButtons xboxButtons = XboxButtons.A;

        [ShowIf("@this.xboxButtons == XboxButtons.RIGHT_TRIGGER || this.xboxButtons == XboxButtons.LEFT_TRIGGER"), BoxGroup("Options", order: 25)]
        public float triggerSensibility = .95f;


        public override void TestInput()
        {
            if (!IsActive)
                return;

            switch (xboxButtons)
            {
                case XboxButtons.A:
                    TestButton(KeyCode.JoystickButton0);
                    break;
                case XboxButtons.X:
                    TestButton(KeyCode.JoystickButton2);
                    break;
                case XboxButtons.Y:
                    TestButton(KeyCode.JoystickButton3);
                    break;
                case XboxButtons.B:
                    TestButton(KeyCode.JoystickButton4);
                    break;
                case XboxButtons.RIGHT_BUMPER:
                    TestButton(KeyCode.JoystickButton5);
                    break;
                case XboxButtons.LEFT_BUMPER:
                    TestButton(KeyCode.JoystickButton4);
                    break;
                case XboxButtons.RIGHT_STICK:
                    TestButton(KeyCode.JoystickButton9);
                    break;
                case XboxButtons.LEFT_STICK:
                    TestButton(KeyCode.JoystickButton8);
                    break;
                case XboxButtons.OPTIONS:
                    TestButton(KeyCode.JoystickButton7);
                    break;
                case XboxButtons.BACK:
                    TestButton(KeyCode.JoystickButton6);
                    break;
                case XboxButtons.RIGHT_TRIGGER:
                    TestTrigger("RightTrigger");
                    break;
                case XboxButtons.LEFT_TRIGGER:
                    TestTrigger("LeftTrigger");
                    break;
            }
        }

        void TestTrigger(string axisName)
        {
            if (Input.GetAxis(axisName) > triggerSensibility)
            {
                onCurrent?.Invoke();

                if (!IsTrigger)
                {
                    onDown?.Invoke();
                    IsTrigger = true;
                }
            }

            if (Input.GetAxis(axisName) < 1 - triggerSensibility && IsTrigger)
            {
                onUp?.Invoke();
                IsTrigger = false;
            }
        }

        void TestButton(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                onDown?.Invoke();
            }

            if (Input.GetKey(key))
            {
                onCurrent?.Invoke();
                IsTrigger = true;
            }

            if (Input.GetKeyUp(key))
            {
                onUp?.Invoke();
                IsTrigger = false;
            }
        }
    }

    public enum XboxButtons
    {
        A,
        X,
        Y,
        B,
        RIGHT_BUMPER,
        LEFT_BUMPER,
        RIGHT_STICK,
        LEFT_STICK,
        OPTIONS,
        BACK,
        RIGHT_TRIGGER,
        LEFT_TRIGGER
    }
}