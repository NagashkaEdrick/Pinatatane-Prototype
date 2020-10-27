﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimplePhysic : MonoBehaviour
{
    #region public
    public float gravity;
    public AnimationCurve frictionCurve;
    [Range(0.0001f, 1f)] public float frictionForce;
    #endregion
    #region private
    Transform self;
    Vector3 forceApplication = Vector3.zero;
    List<Vector3> forces = new List<Vector3>();
    List<Vector3> directForces = new List<Vector3>();
    List<float> forcesFrictionTime = new List<float>();
    Collider[] collisions;
    Collider box;
    #endregion

    private void Awake() {
        self = transform;
        box = GetComponent<Collider>();
    }

    public void AddForce(Vector3 force) {
        // Ajoute une force (relative aux coordonnee World) à la liste des forces
        if (force != Vector3.zero) {
            forces.Add(force);
            forcesFrictionTime.Add(0f);
        }
    }

    public void AddDirectForce(Vector3 force) {
        // Ajoute une force instantanee (relative aux coordonnee World), equivalent a la modifcation direct d'une velocite de rigidbody
        if (force != Vector3.zero) directForces.Add(force);
    }

    public void ApplyGravity() {
        // Applique la force de gravite s'il n'y a pas de collision avec le sol

        //Test de collision avec gravite appliquer
        Vector3 gravity = new Vector3(0, -this.gravity, 0);
        Collider collider = GetCollider(gravity, "Ground");
        if (collider != null) {
            //Si collision avec le sol il y a on remet l'objet a ras du sol
            Vector3 pointofCollision = collider.ClosestPointOnBounds(self.position);
            self.position = pointofCollision + new Vector3(0, box.bounds.extents.y, 0);
            return;
        }
        //Sinon s'il n'y a pas de collision avec le sol on applique la gravite
        forceApplication += gravity;
    }

    public void ApplyFriction() {
        // Reduit les forces en fonction de la courbe de force de friction
        for (int i = 0; i < forces.Count; i++) {
            forces[i] *= frictionCurve.Evaluate(forcesFrictionTime[i]);
            forcesFrictionTime[i] += (frictionForce * Time.deltaTime);
            if (forces[i] == Vector3.zero) {
                forces.RemoveAt(i);
                forcesFrictionTime.RemoveAt(i);
            }
        }
    }

    public void ApplyForces() {
        forces.ForEach(force => forceApplication += transform.InverseTransformVector(force));
        // A finir faire comme avec la gravite
        /*for (int i = 0; i < forces.Count; i++) {
            Collider collider = GetCollider(self.InverseTransformVector(forces[i]));
            if (collider != null) {
                Vector3 pointofCollision = collider.ClosestPointOnBounds(self.position);
                self.position = pointofCollision + new Vector3(0, box.bounds.extents.y, 0);
                return;
            }
        }*/
    }

    public void ApplyDirectForces() {
        directForces.ForEach(force => forceApplication += transform.InverseTransformVector(force));
    }

    public Vector3 GetVelocity() {
        Vector3 velocity = Vector3.zero;
        forces.ForEach(force => velocity += force);
        directForces.ForEach(force => velocity += force);
        return velocity;
    }

    public Collider GetCollider(Vector3 force, string layerName = "") {
        // Test s'il y aura une collision si une force est appliquer
        float radius = box.bounds.extents.x;
        collisions = Physics.OverlapSphere(self.position + (force * Time.deltaTime), radius);

        for (int i = 0; i < collisions.Length; i++) {
            if (layerName == string.Empty || LayerMask.LayerToName(collisions[i].gameObject.layer) == layerName) return collisions[i];
        }
        return null;
    }

    private void Update() {
        forceApplication = Vector3.zero;

        // Application de toute les forces et test de leurs collisions après leurs applications
        ApplyGravity();
        ApplyDirectForces();
        ApplyForces();

        // Reduction des forces pour la prochaine frame
        ApplyFriction();

        directForces.Clear(); // Ce sont des forces instantanée qui disparaisse immediatement

        self.Translate(forceApplication * Time.deltaTime);
    }

}
