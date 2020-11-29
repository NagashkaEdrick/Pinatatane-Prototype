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

        [FoldoutGroup("Player Axis")]
        public QInputAxis
            moveX,
            moveY,
            instructionJoystick;

        [FoldoutGroup("Player Inputs")]
        public QInputXBOXTouch
            aimButton,
            grabButton,
            dashButton;

        [FoldoutGroup("Camera Inputs")]
        public QInputAxis
            cameraRotX,
            cameraRotY;

        //public bool useNetworkCommands = false;

        public override void OnUpdate()
        {
            if (GameplayBatch)
            {
                TestCommands();
            }
        }

        void TestCommands()
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