using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    public class PiegeData : ScriptableObject
    {
        [Tooltip("Valeur d'attaque de base des éléments de LD.")]
        [SerializeField] protected int _att = 0;

        [Tooltip("Valeur minimal d'une attaque.")]
        [SerializeField] protected int _base = 0;

        [Tooltip("Vélocité minimale à dépasser pour infliger des dégâts de vélocité.")]
        [SerializeField] protected float _minVel = 0;

        [Tooltip("Multiplicateur de la vélocité.")]
        [SerializeField] protected float _c = 0;

        public float GetDamages(float velocity)
        {
            if (velocity > _minVel)
                return _att + _base + (velocity * _c);
            else
                return _att;
        }
    }
}