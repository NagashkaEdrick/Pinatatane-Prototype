using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;
using GameplayFramework.Network;

using QRTools.Inputs;

using Photon.Pun;

namespace GameplayFramework
{
    public class InputManager : MonobehaviourSingleton<InputManager>
    {
        [FoldoutGroup("Player Inputs")]
        public bool GameplayBatch = true;

        [FoldoutGroup("Player Inputs")]
        public QInputAxis
            moveX,
            moveY;

        [FoldoutGroup("Player Inputs")]
        public QInputXBOXTouch
            aimButton,
            grabButton;

        [FoldoutGroup("Camera Inputs")]
        public QInputAxis
            cameraRotX,
            cameraRotY;

        public override void OnUpdate()
        {
            if (GameplayBatch)
            {
                moveX.TestInput();
                moveY.TestInput();

                aimButton.TestInput();
                grabButton.TestInput();

                cameraRotX.TestInput();
                cameraRotY.TestInput();
            }
        }
    }
}