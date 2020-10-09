using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pinatatane/Character Controller Data", fileName = "Character Controller Data")]
public class CharacterControllerData : ScriptableObject
{
    [Range(0f, 1f)]
    public float rotationAcceleration = 0.5f;

    public float rotationSpeed = 2;
    public float movementSpeed = 500;
    public float dashSpeed = 10;
    public float cameraHeight = 1;
    public float cameraDistance = 7;
}
