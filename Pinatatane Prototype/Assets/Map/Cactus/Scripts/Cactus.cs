using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane {
    public class Cactus : MonoBehaviour
    {
        [SerializeField] PinataController pinata; //pas bon, faut get le joueur instancier a la creation de la room
        [SerializeField] float damage;
        [SerializeField] float gettingAwayTime;

        Coroutine cor = null;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (cor == null) cor = StartCoroutine(BlockPlayer());
            }
        }

        IEnumerator BlockPlayer()
        {
            pinata.IsBlocked = true;
            yield return new WaitForSeconds(gettingAwayTime);
            pinata.IsBlocked = false;
            cor = null;
            yield break;
        }
    }
}
