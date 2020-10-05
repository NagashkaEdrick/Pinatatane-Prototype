using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class InputManager : MonoBehaviour
    {
        public static float GetXAxisLeftJoystick()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public static float GetYAxisLeftJoystick()
        {
            return Input.GetAxisRaw("Vertical");
        }

        public static float GetXAxisRightJoystick()
        {
            return Input.GetAxisRaw("RotationX");
        }

        public static float GetYAxisRightJoystick()
        {
            return Input.GetAxisRaw("RotationY");
        }
    }
}