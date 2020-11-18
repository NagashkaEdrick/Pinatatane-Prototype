using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Inputs/XBOX/Axis", fileName = "New XBOX Axis")]
    public class QInputXBOXAxis : QInputAxis
    {

    }

    public enum XBOXAxisType
    {
        RIGHT_JOYSTICK_X,
        RIGHT_JOYSTICK_Y,
        LEFT_JOYSTICK_X,
        LEFT_JOYSTICK_Y,
        RIGHT_TRIGGER,
        LEFT_TRIGGER,
        DIRECTIONAL_PAS_Y,
        DIRECTIONAL_PAD_Y
    }
}