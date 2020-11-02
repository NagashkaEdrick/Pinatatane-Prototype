using Pinatatane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotationGrab : MonoBehaviour
{

    public Transform attacker;
    public SimplePhysic body;
    public float rotationForce;

    float startAngle;
    float newAngle;
    Vector3 attackerTarget;
    Vector3 attackerForward;
    Vector3 attackerTarget2;
    bool link = false;
    bool locked = false;

    private void Start()
    {
        // Calcul de l'angle initial (on ne commence pas forcement la rotation avec un angle de 0 entre l'attaquant et la cible)
        attackerTarget = transform.position - attacker.position;
        attackerForward = attacker.forward;
        attackerTarget.y = 0;
        attackerForward.y = 0;
        startAngle = Vector3.SignedAngle(attackerTarget, attackerForward, Vector3.up);
    }

    private void Update()
    {
        if (InputManagerQ.Instance.GetAxis("RotationX") != 0)
        {
            link = true;
            attackerTarget = transform.position - attacker.position;
            attackerForward = attacker.forward;
            attackerTarget.y = 0;
            attackerForward.y = 0;
            Debug.DrawRay(transform.position, attackerTarget, Color.red);
            Debug.DrawRay(attacker.position, attackerForward, Color.blue);
            newAngle = Vector3.SignedAngle(attackerTarget, attackerForward, Vector3.up) - startAngle;
            attackerTarget2 = Quaternion.Euler(0, newAngle, 0) * attackerTarget;
            transform.forward = -attacker.forward;

            /* Ce sont des forces donc + la force est faible + la target est avant le point ou elle devait etre sur le cercle autour du joueur
             * et donc + la prochaine direction tirera vers le joueur et donc + la target se rapprochera vite du joueur
             * A l'inverse plus la force est forte, plus il s'ecarte du joueur 
             * (des forces trop fortes cree des bugs car la target s'eloigne trop du cercle et sa fausse les calculs d'angle) 
             * faire sur le transform de la target transform.Translate(attackerTarget2 - attackerTarget)
             * place la target parfaitement sur le cercle autour du joueur */
            Debug.Log("Angle : " + newAngle + "    force added : " + (attackerTarget2 - attackerTarget) * rotationForce);
            body.AddDirectForce((attackerTarget2 - attackerTarget) * rotationForce);
        }
        
        if (link && !locked && InputManagerQ.Instance.GetAxis("RotationX") == 0)
        {
            locked = true;
            if (newAngle >= 0) body.AddForce(attacker.right * rotationForce);
            else body.AddForce(-attacker.right * rotationForce);
        }
    }
}
