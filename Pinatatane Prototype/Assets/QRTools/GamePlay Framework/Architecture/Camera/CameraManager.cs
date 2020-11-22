using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;

namespace GameplayFramework
{
    public class CameraManager : MonobehaviourSingleton<CameraManager>
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] CameraController m_CurrentCameraController;
        public CameraController CurrentCameraController { get => m_CurrentCameraController; set => m_CurrentCameraController = value; }

        [SerializeField] Dictionary<string, CameraController> m_cameraControllers = new Dictionary<string, CameraController>();

        public bool inTransition { get; protected set; }
        Coroutine transitionCoroutine;

        [SerializeField] StateMachineCameraController m_StateMachineCameraController;

        private void Start()
        {
            m_StateMachineCameraController.StartStateMachine(m_StateMachineCameraController.currentState, this);
        }

        private void Update()
        {
            m_StateMachineCameraController?.CheckCurrentState(this);
            m_StateMachineCameraController?.currentState?.OnCurrent(this);

            m_CurrentCameraController?.CameraUpdate();

            if (Input.GetKeyDown(KeyCode.A))
                TransitionTo(GetCameraHandler("AimHandler"), 3f);

            if (Input.GetKeyDown(KeyCode.Z))
                TransitionTo(GetCameraHandler("FreeLookHandler"), 3f);
        }

        public void TransitionTo(CameraController to, float TimeTransition)
        {
            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(TransitionCoroutine(to, TimeTransition));
        }

        public void TransitionTo(string to, float TimeTransition)
        {
            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(TransitionCoroutine(GetCameraHandler(to), TimeTransition));
        }

        IEnumerator TransitionCoroutine(CameraController to, float TimeTransition)
        {
            inTransition = true;
            m_CurrentCameraController = to;
            Vector3 startPos = mainCamera.transform.position;

            float elapsedTime = 0;
            while (elapsedTime < TimeTransition)
            {
                mainCamera.transform.position = Vector3.Lerp(startPos, to.CameraTransform.position, (elapsedTime / TimeTransition));

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            inTransition = false;

            yield break;
        }

        public CameraController GetCameraHandler(string key)
        {
            m_cameraControllers.TryGetValue(key, out var value);
            return value;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(m_CurrentCameraController.CameraTransform.position, .5f);
            Gizmos.DrawLine(mainCamera.transform.position, m_CurrentCameraController.CameraTransform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_CurrentCameraController.CameraTransform.position, mainCamera.transform.position);
            Gizmos.DrawWireSphere(mainCamera.transform.position, .7f);
            Gizmos.DrawLine(m_CurrentCameraController.TargetTransform.position, mainCamera.transform.position);
        }

        [Button]
        void CreateVirtualCamera()
        {
            GameObject root = new GameObject("New Camera Handler");
            root.transform.parent = transform;
            GameObject controller = new GameObject("Controller");
            controller.transform.parent = root.transform;
            GameObject virtualCam = new GameObject("Virtual Camera");
            virtualCam.transform.parent = root.transform;
        }
    }
}