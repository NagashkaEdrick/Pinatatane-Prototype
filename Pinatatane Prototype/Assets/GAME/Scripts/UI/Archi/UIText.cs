using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Pinatatane
{
    public class UIText : UIElement
    {
        public TextMeshProUGUI text;

        public string SetText(string _text) => text.text = _text;

        public string SetText(string _text, Color _color)
        {
            text.text = _text;
            text.color = _color;
            return _text;
        }
    }
}