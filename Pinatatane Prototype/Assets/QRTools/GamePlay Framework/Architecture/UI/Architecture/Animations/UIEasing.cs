using System.Collections;
using System.Collections.Generic;
using System;

using Sirenix.OdinInspector;

using UnityEngine;

using DG.Tweening;

namespace GameplayFramework
{
    public class UIEasing : UIAnimation
    {
        [SerializeField] RectTransform rectTransform;

        //Position, Scale, Rotation
        [SerializeField] UIAnimTransform animTransform;
        //X Y
        [SerializeField] UIAnimDir animDir;
        //EASING
        [SerializeField] private Ease ease = Ease.InOutSine;

        [Button]
        protected override void Animation()
        {
            Debug.Log("easing");
        }
    }

    [Flags]
    public enum UIAnimTransform
    {
        POSITION = 1,
        ROTATION = 2,
        SCALE = 4
    }

    [Flags]
    public enum UIAnimDir
    {
        UP = 1,
        DOWN = 2,
        RIGHT = 4,
        LEFT = 8
    }
}