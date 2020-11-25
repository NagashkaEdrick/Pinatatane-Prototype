using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorBehaviour : MonoBehaviour
{

    public Animator animator;

    public void SetFloat(string key, float value)
    {
        animator.SetFloat(key, value);
    }

    public void SetBool(string key, bool value)
    {
        animator.SetBool(key, value);
    }

    public bool IsPlaying(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
