using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBehaviour : MonoBehaviour
{

    public Animator animator;

    public void Animate(string key, float value)
    {
        animator.SetFloat(key, value);
    }
}
