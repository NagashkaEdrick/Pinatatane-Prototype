using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

using GameplayFramework.Singletons;

namespace GameplayFramework
{
    public class CameraManager : MonobehaviourSingleton<CameraManager>
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] CameraController m_CurrentCameraController;

        [SerializeField] Dictionary<string, CameraController> m_cameraHandlers = new Dictionary<string, CameraController>();

        public bool inTransition { get; protected set; }
        Coroutine transitionCoroutine;

        private void Update()
        {
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

        IEnumerator TransitionCoroutine(CameraController to, float TimeTransition)
        {
            inTransition = true;
            m_CurrentCameraController = to;

            Vector3 startPos = mainCamera.transform.position;

            float elapsedTime = 0;
            while (elapsedTime < TimeTransition)
            {
                mainCamera.transform.position = Vector3.Lerp(startPos, to.CameraTransform.position, (elapsedTime / TimeTransition));
                //mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, to.CameraTransform.rotation, (elapsedTime / TimeTransition));

                //mainCamera.transform.DORotate(m_CurrentCameraController.CameraTransform.rotation.eulerAngles, TimeTransition);
                //mainCamera.transform.DOMove(m_CurrentCameraController.CameraTransform.position, TimeTransition);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            Debug.Log(elapsedTime);
            inTransition = false;

            yield break;
        }

        CameraController GetCameraHandler(string key)
        {
            m_cameraHandlers.TryGetValue(key, out var value);
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
    }
}