using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class GrabRotation : MonoBehaviour
    {
        public float rotationForce;

        Transform attacker;
        Transform target;
        SimplePhysic targetBody;

        float startAngle;
        float newAngle;
        Vector3 attackerTarget;
        Vector3 attackerForward;
        Vector3 attackerTarget2;

        bool linkFinished = false;

        public void Link(Transform attacker, Transform target)
        {
            this.attacker = attacker;
            this.target = target;
            targetBody = this.target.GetComponent<SimplePhysic>();

            // Calcul de l'angle initial (on ne commence pas forcement la rotation avec un angle de 0 entre l'attaquant et la cible)
            attackerTarget = transform.position - attacker.position;
            attackerForward = attacker.forward;
            attackerTarget.y = 0;
            attackerForward.y = 0;
            startAngle = Vector3.SignedAngle(attackerTarget, attackerForward, Vector3.up);
            Debug.Log("start : " + startAngle);
            startAngle = 0; //bug avec le start angle obliger de le forcer a 0

            linkFinished = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (linkFinished)
            {
                attackerTarget = target.position - attacker.position;
                attackerForward = attacker.forward;
                attackerTarget.y = 0;
                attackerForward.y = 0;
                newAngle = Vector3.SignedAngle(attackerTarget, attackerForward, Vector3.up) - startAngle;
                attackerTarget2 = Quaternion.Euler(0, newAngle, 0) * attackerTarget;
                target.forward = -attacker.forward;

                /* Ce sont des forces donc + la force est faible + la target est avant le point ou elle devait etre sur le cercle autour du joueur
                 * et donc + la prochaine direction tirera vers le joueur et donc + la target se rapprochera vite du joueur
                 * A l'inverse plus la force est forte, plus il s'ecarte du joueur 
                 * (des forces trop fortes cree des bugs car la target s'eloigne trop du cercle et sa fausse les calculs d'angle) 
                 * faire sur le transform de la target transform.Translate(attackerTarget2 - attackerTarget)
                 * place la target parfaitement sur le cercle autour du joueur */
                targetBody.AddDirectForce((attackerTarget2 - attackerTarget) * rotationForce);
            }
        }

        public void ReleaseRight()
        {
            linkFinished = false;
            targetBody.AddForce(attacker.right * rotationForce);
        }

        public void ReleaseLeft()
        {
            linkFinished = false;
            targetBody.AddForce(-attacker.right * rotationForce);
        }
    }
}
