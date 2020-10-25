using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimplePhysic : MonoBehaviour
{
    #region public
    public float gravity;
    public AnimationCurve frictionCurve;
    [Range(0.0001f, 1f)] public float frictionForce;
    public Vector3 testForce;
    #endregion
    #region private
    Vector3 forceApplication;
    List<Vector3> forces;
    List<Vector3> directForces;
    List<float> forcesFrictionTime;
    Collider[] collisions;
    #endregion

    private void Start() {
        forceApplication = Vector3.zero;
        forces = new List<Vector3>();
        directForces = new List<Vector3>();
        forcesFrictionTime = new List<float>();
    }

    public void AddForce(Vector3 force) {
        // Ajoute une force à la liste des forces
        Debug.Log(Vector3.Angle(force, transform.forward));
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
        Collider collider = GetCollider("Ground");
        if (collider == null) forceApplication.y -= gravity;
        else {
            Vector3 pointofCollision = collider.ClosestPointOnBounds(transform.position);
            transform.position = pointofCollision + new Vector3(0, GetComponent<Collider>().bounds.size.y / 2, 0);
        }
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

    public Collider GetCollider(string colliderName) {
        // A terme tester avec des layers ou des tags
        var collisionTest = from collision in collisions where collision.gameObject.name == colliderName select collision;
        return collisionTest.Count() > 0? collisionTest.First(): null;
    }

    private void Update() {
        forceApplication = Vector3.zero;
        float radius = GetComponent<Collider>().bounds.size.x / 2;
        collisions = Physics.OverlapSphere(transform.position, radius);

        ApplyGravity();
        ApplyDirectForces();
        ApplyForces();
        ApplyFriction();

        directForces.Clear(); // Ce sont des forces instantanée qui disparaisse immediatement

        transform.Translate(forceApplication * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, transform.position + forceApplication, Time.deltaTime);
        #region debug
        // Dans le futur dessiner un hundler qui draw la sphere dans la scene view
        /*Debug.DrawLine(transform.position, transform.position + new Vector3(radius, 0, 0), Color.red);
        DebugColliders();*/
        #endregion
    }

}
