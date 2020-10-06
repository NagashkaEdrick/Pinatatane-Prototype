using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace Pinatatane {
    public class InputManagerQ : SerializedMonoBehaviour {
        //Systeme Input avec Action call par string.

        public static InputManagerQ Instance;

        [Title("Inputs")]
        public InputAction[] inputs;
        [Title("Axis")]
        public AxisAction[] axis;
        public TriggerAction[] triggers;

        private void Awake() {
            Instance = this;
        }

        private void Update() {
            if (inputs != null) {
                for (int i = 0; i < inputs.Length; i++) {
                    inputs[i].CheckInput();
                }
            }

            if (axis != null) {
                for (int i = 0; i < axis.Length; i++) {
                    axis[i].CheckInput();
                }
            }
        }

        private void FixedUpdate()
        {
            if (triggers != null)
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    triggers[i].CheckInput();
                }
            }
        }

        public QInput GetInput(string _inputName) {
            for (int i = 0; i < inputs.Length; i++) {
                if (inputs[i].inputName == _inputName)
                    return inputs[i];
            }

            return null;
        }

        public float GetAxis(string axisName) {
            return Input.GetAxisRaw(axisName);
        }

        public bool GetTrigger(string axisName) {
            return Input.GetAxisRaw(axisName) >= 1;
        }
    }

    public abstract class QInput {
        public string inputName;
        public bool isActive = true;

        public abstract void CheckInput();
    }

    [System.Serializable]
    public class TriggerAction : QInput 
    {
        public UnityEvent onTrigger;
        public string axisName;
        private bool isClicked = false;
        public float sensibility = 1f;

        public override void CheckInput() {
            if (Input.GetAxisRaw(axisName) == sensibility && !isClicked) {
                onTrigger?.Invoke();
                isClicked = true;
            }
            if (Input.GetAxisRaw(axisName) == 1 - sensibility && isClicked) {
                isClicked = false;
            }
        }
    }

    [System.Serializable]
    public class InputAction : QInput
    {
        [TitleGroup("Details")]
        public KeyCode key;
        [TitleGroup("Details")]
        public InputType inputType = InputType.DOWN;
        [TitleGroup("Details")]
        public UnityEvent onInput;

        public override void CheckInput()
        {
            if (!isActive) return;

            switch (inputType)
            {
                case InputType.DOWN:
                    if (Input.GetKeyDown(key))
                        onInput?.Invoke();
                        break;
                case InputType.UP:
                    if (Input.GetKeyUp(key))
                        onInput?.Invoke();
                    break;
                case InputType.CURRENT:
                    if (Input.GetKey(key))
                        onInput?.Invoke();
                    break;
            }
        }
    }

    public enum InputType { DOWN, UP, CURRENT}

    public class AxisAction : QInput
    {
        [TitleGroup("Details")]
        public string axisName;
        [TitleGroup("Details")]
        public AxisType axisType = AxisType.SIMPLE;
        [TitleGroup("Details")]
        public UnityEventFloat onAxisMove;

        public override void CheckInput()
        {
            if (!isActive) return;

            switch (axisType)
            {
                case AxisType.SIMPLE:
                    onAxisMove?.Invoke(Input.GetAxis(axisName));
                    break;
                case AxisType.RAW:
                    onAxisMove?.Invoke(Input.GetAxisRaw(axisName));
                    break;
            }
        }
    }

    public enum AxisType { SIMPLE, RAW}

    public class UnityEventBool : UnityEvent<bool> { }
    public class UnityEventFloat : UnityEvent<float> { }
}