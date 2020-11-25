using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;

namespace GameplayFramework
{
    public class CameraManager : MonobehaviourSingleton<CameraManager>
    {
        [SerializeField] Camera m_MainCamera;

        [SerializeField] CameraController m_CurrentCameraController;

        [SerializeField] Dictionary<string, CameraController> m_CameraControllers = new Dictionary<string, CameraController>();

        [SerializeField] StateMachineCameraController m_StateMachineCameraController;

        public CameraController CurrentCameraController { get => m_CurrentCameraController; set => m_CurrentCameraController = value; }
        public bool inTransition { get; protected set; }
        public Camera MainCamera { get => m_MainCamera; set => m_MainCamera = value; }

        Coroutine transitionCoroutine;

        private void Start()
        {
            m_StateMachineCameraController?.StartStateMachine(m_StateMachineCameraController.currentState, this);
        }

        private void Update()
        {
            m_StateMachineCameraController?.CheckCurrentState(this);
            m_StateMachineCameraController?.currentState?.OnCurrent(this);

            m_CurrentCameraController?.CameraUpdate();
        }

        /// <summary>
        /// Create a smooth transition from CurrentCameraController to an other.
        /// </summary>
        public void TransitionTo(CameraController to, float TimeTransition = 1f)
        {
            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(TransitionCoroutine(to, TimeTransition));
        }

        /// <summary>
        /// Create a smooth transition from CurrentCameraController to an other.
        /// </summary>
        public void TransitionTo(string to, float TimeTransition = 1f)
        {
            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(TransitionCoroutine(GetCameraHandler(to), TimeTransition));
        }

        /// <summary>
        /// Find a CameraController in m_CameraControllers
        /// </summary>
        public CameraController GetCameraHandler(string key)
        {
            m_CameraControllers.TryGetValue(key, out var value);
            return value;
        }

        /// <summary>
        /// Coroutine use for transition
        /// </summary>
        IEnumerator TransitionCoroutine(CameraController to, float TimeTransition)
        {
            inTransition = true;
            m_CurrentCameraController = to;
            Vector3 startPos = m_MainCamera.transform.position;

            float elapsedTime = 0;
            while (elapsedTime < TimeTransition)
            {
                m_MainCamera.transform.position = Vector3.Lerp(startPos, to.VirtualCameraTransform.position, (elapsedTime / TimeTransition));

                elapsedTime += Time.deltaTime;

                yield return null;
            }
            inTransition = false;

            yield break;
        }

        public void SetCameraControllersTarget(Transform newTransform)
        {
            for (int i = 0; i < m_CameraControllers.Count; i++)
            {
                m_CameraControllers.ElementAt(i).Value.TargetTransform = newTransform;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (CurrentCameraController != null && CurrentCameraController.TargetTransform != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(m_CurrentCameraController.VirtualCameraTransform.position, .5f);
                Gizmos.DrawLine(m_MainCamera.transform.position, m_CurrentCameraController.VirtualCameraTransform.position);

                Gizmos.color = Color.red;
                Gizmos.DrawLine(m_CurrentCameraController.VirtualCameraTransform.position, m_MainCamera.transform.position);
                Gizmos.DrawWireSphere(m_MainCamera.transform.position, .7f);
                Gizmos.DrawLine(m_CurrentCameraController.TargetTransform.position, m_MainCamera.transform.position);
            }
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
#endif
    }
}