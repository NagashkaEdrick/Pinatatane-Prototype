using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject freeLookCamera;
    [SerializeField] GameObject aimCamera;
    [SerializeField] QInputXBOXTouch aimInput;

    private void Start()
    {
        aimInput.onDown.AddListener(OnAim);
        aimInput.onUp.AddListener(OnRealeaseAim);
        aimCamera.SetActive(false);
    }

    private void Update()
    {
        
    }

    // Lancer une fois lorsque la touche de viser est enfoncer
    private void OnAim()
    {
        freeLookCamera.SetActive(false);
        aimCamera.SetActive(true);
    }

    // Lancer une fois lorsque la touche de viser est relacher
    private void OnRealeaseAim()
    {
        aimCamera.SetActive(false);
        freeLookCamera.SetActive(true);
    }
}
