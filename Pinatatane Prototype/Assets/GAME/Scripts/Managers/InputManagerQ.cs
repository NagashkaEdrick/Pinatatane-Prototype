using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class InputManagerQ : SerializedMonoBehaviour
    {
        //Systeme Input avec Action call par string.

        public static InputManagerQ Instance;

        [Title("Inputs")]
        public InputAction[] inputs;
        [Title("Axis")]
        public AxisAction[] axis;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].CheckInput();
            }

            for (int i = 0; i < axis.Length; i++)
            {
                axis[i].CheckInput();
            }
        }

        public QInput GetInput(string _inputName)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i].inputName == _inputName)
                    return inputs[i];
            }

            return null;
        }

        Coroutine cor;
        public void Dash()
        {
            cor = StartCoroutine(DashCor());
        }

        IEnumerator DashCor()
        {
            yield break;
        }
    }

    public abstract class QInput
    {
        public string inputName;
        public bool isActive = true;

        public abstract void CheckInput();
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