using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class IK_Chain : MonoBehaviour
{
    public int chainLenght = 2;
    public Transform target;
    public Transform pole;

    public int Iteration = 10;
    public float delta = 0.001f;
    public float snapBackStrenght = 1f;

    protected float[] BonesLenght;
    protected float CompleteLenght;
    protected Transform[] Bones;
    protected Vector3[] Positions;
    protected Vector3[] StartDirectionSucc;
    protected Quaternion[] StartRotationBone;
    protected Quaternion StartRotationTarget;
    protected Quaternion StartRotationRoot;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        Bones = new Transform[chainLenght + 1];
        Positions = new Vector3[chainLenght + 1];
        BonesLenght = new float[chainLenght];
        StartDirectionSucc = new Vector3[chainLenght + 1];
        StartRotationBone = new Quaternion[chainLenght + 1];

        if(target == null)
        {
            target = new GameObject(gameObject.name + " Target").transform;
            target.position = transform.position;
        }

        StartRotationTarget = target.rotation;
        CompleteLenght = 0;

        var current = transform;
        for (var i = Bones.Length - 1; i >= 0; i--)
        {
            Bones[i] = current;
            StartRotationBone[i] = current.rotation;

            if (i == Bones.Length - 1)
            {
                StartDirectionSucc[i] = target.position - current.position;
            }
            else
            {
                StartDirectionSucc[i] = Bones[i + 1].position - current.position;
                BonesLenght[i] = StartDirectionSucc[i].magnitude;
                CompleteLenght += BonesLenght[i];
            }

            current = current.parent;
        }
    }

    private void LateUpdate()
    {
        ResolveIK();
    }

    private void ResolveIK()
    {
        if (target == null)
            return;

        if (Bones.Length != chainLenght)
            Init();

        for (int i = 0; i < Bones.Length; i++)
        {
            Positions[i] = Bones[i].position;
        }

        var rootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        var rootRotDiff = rootRot * Quaternion.Inverse(StartRotationRoot);

        if ((target.position - Bones[0].position).sqrMagnitude >= CompleteLenght * CompleteLenght)
        {
            var direction = (target.position - Positions[0]).normalized;

            for (int i = 1; i < Positions.Length; i++)
            {
                Positions[i] = Positions[i - 1] + direction * BonesLenght[i - 1];
            }
        }
        else
        {
            for (int iteration = 0; iteration < Iteration; iteration++)
            {
                for (int i = Positions.Length - 1; i > 0; i--)
                {
                    if (i == Positions.Length - 1)
                        Positions[i] = target.position;
                    else
                        Positions[i] = Positions[i + 1] + (Positions[i] - Positions[i + 1]).normalized * BonesLenght[i];
                }

                for (int i = 1; i < Positions.Length; i++)
                {
                    Positions[i] = Positions[i - 1] + (Positions[i] - Positions[i - 1]).normalized * BonesLenght[i - 1];
                }

                if ((Positions[Positions.Length - 1] - target.position).sqrMagnitude < delta * delta)
                    break;
            }
        }

        if(pole != null)
        {
            for (int i = 1; i < Positions.Length - 1; i++)
            {
                var plane = new Plane(Positions[i + 1] - Positions[i - 1], Positions[i - 1]);
                var projectedPole = plane.ClosestPointOnPlane(pole.position);
                var projectedBone = plane.ClosestPointOnPlane(Positions[i]);
                var angle = Vector3.SignedAngle(projectedBone - Positions[i - 1], projectedPole - Positions[i - 1], plane.normal);
                Positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (Positions[i] - Positions[i - 1]) + Positions[i - 1];
            }
        }

        for (int i = 0; i < Positions.Length; i++)
        {
            if (i == Positions.Length - 1)
                Bones[i].rotation = target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBone[i];
            else
                Bones[i].rotation = Quaternion.FromToRotation(StartDirectionSucc[i], Positions[i + 1] - Positions[i]) * StartRotationBone[i];

            Bones[i].position = Vector3.Lerp(Bones[i].position, Positions[i], .2f);
        }
    }

    private void OnDrawGizmos()
    {
        var current = transform;
        for (int i = 0; i < chainLenght && current != null && current.parent != null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, Vector3.Distance(current.parent.position, current.position), scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * .5f, Vector3.one);
            current = current.parent;
        }

        Gizmos.color = Color.red;
        if (target != null)
            Gizmos.DrawWireCube(target.position, Vector3.one / 2f);
        Gizmos.color = Color.yellow;
        if (pole != null)
            Gizmos.DrawWireCube(pole.position, Vector3.one / 2f);
    }
}
