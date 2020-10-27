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
    #endregion

    private void Awake() {
        self = transform;
    }

    public void AddForce(Vector3 force) {
        // Ajoute une force à la liste des forces
        if (force != Vector3.zero) {
            forces.Add(force);
            forcesFrictionTime.Add(0f);
        }
    }

    public void AddDirectForce(Vector3 force) {
        // Ajoute une force instantanee, equivalent a la modifcation direct d'une velocite de rigidbody
        if (force != Vector3.zero) directForces.Add(force);
    }

    public void ApplyGravity() {
        // Applique la force de gravite s'il n'y à pas de collision avec le sol
        float radius = GetComponent<Collider>().bounds.size.x;
        Vector3 positionAfterGravity = new Vector3(self.position.x, self.position.y - (gravity * Time.deltaTime), self.position.z);
        collisions = Physics.OverlapSphere(positionAfterGravity, radius);
        for (int i = 0; i < collisions.Length; i++) {
            //if (LayerMask.LayerToName(collisions[i].gameObject.layer) == "Ground") {
            if (collisions[i].gameObject.name.Contains("Ground")) {
                Debug.Log("Collision avec le sol je le remet a ras du sol");
                Vector3 pointofCollision = collisions[i].ClosestPointOnBounds(self.position);
                self.position = pointofCollision + new Vector3(0, GetComponent<Collider>().bounds.size.y / 2, 0);
                return;
            }
        }
        forceApplication.y -= gravity;
        Debug.Log("J'applique la gravite a : " + gameObject.name);

        /*if (collider == null) {
            forceApplication.y -= gravity;
            Debug.Log("J'applique la gravite a : " + gameObject.name);
        } else {
            Debug.Log("Collision avec le sol je le remet a ras du sol");
            Vector3 pointofCollision = collider.ClosestPointOnBounds(self.position);
            self.position = pointofCollision + new Vector3(0, GetComponent<Collider>().bounds.size.y / 2, 0);
        }*/
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
        forces.ForEach(force => forceApplication += force);
    }

    public void ApplyDirectForces() {
        directForces.ForEach(force => forceApplication += force);
    }

    public Vector3 GetVelocity() {
        Vector3 velocity = Vector3.zero;
        forces.ForEach(force => velocity += force);
        directForces.ForEach(force => velocity += force);
        return velocity;
    }

    public void DebugColliders() {
        for (int i = 0; i < collisions.Length; i++) {
            if (collisions[i].gameObject.name != gameObject.name) Debug.Log(collisions[i].gameObject.name);
        }
    }

    public Collider GetCollider(string layerName) {
        // A terme tester avec des layers ou des tags
        var collisionTest = from collision in collisions where LayerMask.LayerToName(collision.gameObject.layer) == layerName select collision;
        return collisionTest.Count() > 0? collisionTest.First(): null;
    }

    private void Update() {
        forceApplication = Vector3.zero;
        float radius = GetComponent<Collider>().bounds.size.x / 2;
        collisions = Physics.OverlapSphere(self.position, radius);

        ApplyGravity();
        ApplyDirectForces();
        ApplyForces();
        ApplyFriction();

        directForces.Clear(); // Ce sont des forces instantanée qui disparaisse immediatement

        self.Translate(forceApplication * Time.deltaTime);
        //self.position = Vector3.Lerp(self.position, self.position + forceApplication, Time.deltaTime);
        #region debug
        // Dans le futur dessiner un hundler qui draw la sphere dans la scene view
        /*DebugColliders();*/
        #endregion
    }

}
