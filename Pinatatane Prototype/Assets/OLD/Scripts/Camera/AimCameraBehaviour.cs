using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AimCameraBehaviour : MonoBehaviour
{

    [BoxGroup("Tweaking")]
    [SerializeField] CharacterControllerData data;

    [BoxGroup("Fix")]
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - data.cameraDistance * target.forward + data.cameraHeight * Vector3.up;
    }
}
