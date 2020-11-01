﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class SimplePhysic : MonoBehaviour
{
    #region public
    public float gravity;
    public AnimationCurve frictionCurve;
    [Range(0.0001f, 1f)] public float frictionForce;
    public bool debug;
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

    private void Awake()
    {
        self = transform;
        box = GetComponent<Collider>();
    }

    public void AddForce(Vector3 force)
    {
        // Ajoute une force (relative aux coordonnee World) à la liste des forces
        if (force != Vector3.zero)
        {
            forces.Add(force);
            forcesFrictionTime.Add(0f);
        }
    }

    public void AddDirectForce(Vector3 force)
    {
        // Ajoute une force instantanee (relative aux coordonnee World), equivalent a la modifcation direct d'une velocite de rigidbody
        if (force != Vector3.zero) directForces.Add(force);
    }

    public void ApplyGravity()
    {
        // Applique la force de gravite s'il n'y a pas de collision avec le sol

        //Test de collision avec gravite appliquer
        Vector3 gravity = new Vector3(0, -this.gravity, 0);
        Collider collider = GetCollider(gravity, "Ground");
        if (collider != null)
        {
            //Si collision avec le sol il y a on remet l'objet a ras du sol
            Vector3 pointofCollision = collider.ClosestPointOnBounds(self.position);
            self.position = pointofCollision + new Vector3(0, box.bounds.extents.y, 0);
            return;
        }
        //Sinon s'il n'y a pas de collision avec le sol on applique la gravite
        forceApplication += gravity;
    }

    public void ApplyFriction()
    {
        // Reduit les forces en fonction de la courbe de force de friction
        for (int i = 0; i < forces.Count; i++)
        {
            forces[i] *= frictionCurve.Evaluate(forcesFrictionTime[i]);
            forcesFrictionTime[i] += (frictionForce * Time.deltaTime);
            if (forces[i] == Vector3.zero)
            {
                forces.RemoveAt(i);
                forcesFrictionTime.RemoveAt(i);
            }
        }
    }

    public void ApplyForces()
    {
        for (int i = 0; i < forces.Count; i++)
        {
            Collider collider = GetCollider(forces[i], "Other");
            if (collider != null && collider.gameObject.name != gameObject.name)
            {
                Debug.Log(gameObject.name + " aura collision avec " + collider.gameObject.name + ", force non appliquer");
                forces.RemoveAt(i);
                // Calcul de la nouvelle position en fonction de la force qui s'exerce
                /*Vector3 pointofCollision = collider.ClosestPointOnBounds(self.position);
                self.position = pointofCollision - (self.InverseTransformDirection(forces[i]).normalized * box.bounds.extents.y);*/
            }
            else forceApplication += transform.InverseTransformVector(forces[i]);
        }

    }

    public void ApplyDirectForces()
    {
        for (int i = 0; i < directForces.Count; i++)
        {
            Collider collider = GetCollider(directForces[i], "Other");
            if (collider != null)
            {
                Debug.Log(gameObject.name + " aura collision avec " + collider.gameObject.name + ", force non appliquer");
                directForces.RemoveAt(i);
                // Calcul de la nouvelle position en fonction de la force qui s'exerce
                /*Vector3 pointofCollision = collider.ClosestPointOnBounds(self.position);
                Debug.DrawLine(pointofCollision, pointofCollision + new Vector3(0, 0.1f, 0), Color.red);
                //Debug.Break();
                self.position = pointofCollision - (self.InverseTransformDirection(directForces[i]).normalized * box.bounds.extents.y);*/
            }
            else forceApplication += transform.InverseTransformVector(directForces[i]);
        }
    }

    public Vector3 GetVelocity()
    {
        Vector3 velocity = Vector3.zero;
        forces.ForEach(force => velocity += force);
        directForces.ForEach(force => velocity += force);
        return velocity;
    }

    public Collider GetCollider(Vector3 force, string layerName = "")
    {
        // Test s'il y aura une collision si une force est appliquer
        collisions = Physics.OverlapBox(self.position + (force * Time.deltaTime), box.bounds.extents, Quaternion.identity);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (gameObject.name != collisions[i].gameObject.name && (layerName == string.Empty || LayerMask.LayerToName(collisions[i].gameObject.layer) == layerName)) return collisions[i];
        }
        return null;
    }

    private void Update()
    {
        forceApplication = Vector3.zero;

        // Application de toute les forces et test de leurs collisions après leurs applications
        ApplyGravity();
        ApplyDirectForces();
        ApplyForces();

        if (debug)
        {
            Debug.Log("Velocity : " + forceApplication);
            Debug.Log("Direct Forces : ");
            for (int i = 0; i < directForces.Count; i++)
            {
                Debug.Log("  " + directForces[i]);
            }
            Debug.Log("Forces : ");
            for (int i = 0; i < forces.Count; i++)
            {
                Debug.Log("  " + forces[i]);
            }
        }


        // Reduction des forces pour la prochaine frame
        ApplyFriction();



        directForces.Clear(); // Ce sont des forces instantanée qui disparaisse immediatement

        self.Translate(forceApplication * Time.deltaTime);
    }

}

