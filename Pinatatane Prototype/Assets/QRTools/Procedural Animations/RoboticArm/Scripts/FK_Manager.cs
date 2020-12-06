using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework.ProceduralAnimation {
    public class FK_Manager : MonoBehaviour
    {
        public FK_Joint[] joints;

        public Vector3 ForwardKinematics(float[] angles)
        {
            Vector3 prevPoints = joints[0].transform.position;
            Quaternion rotation = Quaternion.identity;
            for (int i = 1; i < joints.Length; i++)
            {
                rotation *= Quaternion.AngleAxis(angles[i - 1], joints[i - 1].axis);
                Vector3 nextPoint = prevPoints + rotation * joints[i].startOffset;

                prevPoints = nextPoint;
            }
            return prevPoints;
        }

        public float DistanceFromTarget(Vector3 target, float[] angles)
        {
            Vector3 points = ForwardKinematics(angles);
            return Vector3.Distance(points, target);
        }
    }
}