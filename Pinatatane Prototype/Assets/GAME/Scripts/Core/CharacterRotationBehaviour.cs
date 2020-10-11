using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace Pinatatane {
    public class CharacterRotationBehaviour : MonoBehaviour {

        [BoxGroup("Tweaking")]
        public CharacterControllerData data;

        [BoxGroup("Fix")]
        public Transform _camera; // Reference sur la camera
        [BoxGroup("Fix")]
        public Transform cameraTarget; // Reference sur la cible de la camera
        [BoxGroup("Fix")]
        [SerializeField] public Rigidbody rigidBody;

        float rightJoyX;

        CharacterMovementBehaviour cc => PlayerManager.Instance.LocalPlayer.characterMovementBehaviour;

        // Update is called once per frame
        void FixedUpdate() {
            /* probleme la rotation se fait avant le mouvement donc la rotation de la velocité se fait ecraser par l'update du mouvement */
            PlayerRotation();
        }

        // Gere la rotation du joueur en fonction du joystick droit
        private void PlayerRotation() {
            cameraTarget.rotation = smoothRotation(cameraTarget.rotation, data.rotationAcceleration);
            cc.transform.rotation = smoothRotation(cc.transform.rotation, data.rotationAcceleration);
            cc.rigidBody.velocity = Quaternion.Euler(0, rightJoyX, 0) * cc.rigidBody.velocity;
        }

        // Realise une rotation par acceleration
        Quaternion smoothRotation(Quaternion startRotationVector, float smoothSpeed) {
            rightJoyX += InputManagerQ.Instance.GetAxis("RotationX") * data.rotationSpeed;
            rightJoyX %= 360;
            Quaternion endRotationVector = Quaternion.Euler(0, rightJoyX, 0);
            return Quaternion.Slerp(startRotationVector, endRotationVector, smoothSpeed);
        }

    }
}
