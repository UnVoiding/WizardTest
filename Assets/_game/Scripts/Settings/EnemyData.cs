using System;
using System.Collections.Generic;
using UnityEngine;


namespace Romeno.WizardTest
{
    [Serializable]
    public class EnemyData
    {
        [Serializable]
        public class Resistance
        {
            [SerializeField]
            public DamageType DamageType;

            [SerializeField, Range(0.0f, 1.0f), Tooltip("Percentage of resistance to damage type. 0 - 0%, 1 - 100%")] 
            public float Amount;
        }
        
        [Tooltip("ID of an enemy type")]
        public EnemyType Type;
        
        [Tooltip("Monster name")]
        public String Name;

        [Tooltip("Prefab of a monster")]
        public Enemy Prefab;

        [Tooltip("Damage dealt to a player when approaching at close range")]
        public float Damage;

        public float Health;

        public float MoveSpeed = 3.5f;

        [Tooltip("When being at close range damage is dealt at these intervals (seconds)")]
        public float AttackPeriod;

        [Tooltip("After an attack a monster will Wait this number of seconds")]
        public float ReturnToChaseStateTime;

        [Tooltip("List of resistances to all damage types")]
        public List<Resistance> Resistances;

        [Tooltip("Number of enemies precreated for optimization purposes")]
        public int InitialPoolSize = 3;

        public float GetResistance(DamageType damageType)
        {
            return Resistances.Find((m) => m.DamageType == damageType).Amount;
        }
    }
}