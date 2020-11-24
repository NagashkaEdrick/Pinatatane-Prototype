using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class LassoGraphics : MonoBehaviour
    {
        public Transform from;
        public Transform to;

        [SerializeField] Transform jointsParent;
        [SerializeField] AnimationCurve lerpCurve;
        [SerializeField] float lerpCoef = .10f;

        [SerializeField] List<Joint> joints = new List<Joint>();

        private void Update()
        {
            DrawLasso(from.position, to.position);
        }

        public void Launch(Vector3 toPos)
        {
            to.position = toPos;
        }

        public void Retract()
        {
            to.position = from.position;
        }

        public void DrawLasso(Vector3 from, Vector3 to)
        {
            if (from == to)
                for (int i = 0; i < joints.Count; i++)
                    joints[i].transform.position = Vector3.Lerp(joints[i].transform.position, to, .2f);
            else
            {
                float d = Vector3.Distance(from, to);
                float a = d / (joints.Count - 1);

                for (int i = 0; i < joints.Count; i++)
                {
                    joints[i].transform.position = Vector3.Lerp(joints[i].transform.position, from + (to - from).normalized * a * i, lerpCurve.Evaluate(i * (1f / joints.Count)));
                }
            }
        }

        public void LassoDebug(Color c)
        {
            for (int i = 0; i < joints.Count; i++)
            {
                joints[i].GetComponent<MeshRenderer>().material.color = c;
            }
        }

        [Button("Initialize Joints")]
        void Initialize()
        {
            joints.Clear();
            for (int i = 0; i < jointsParent.childCount; i++)
            {
                joints.Add(jointsParent.GetChild(i).GetComponent<Joint>());
            }
        }
    }
}