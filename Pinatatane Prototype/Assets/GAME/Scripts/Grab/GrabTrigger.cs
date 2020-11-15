using Pinatatane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTrigger : MonoBehaviour
{
    public GrabBehaviour Grab;

    public void TriggerGrab()
    {
        Grab.GrabAction();
    }
}
