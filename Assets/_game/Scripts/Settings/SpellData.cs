using System;
using UnityEngine;


namespace Romeno.WizardTest
{
    [Serializable]
    public class SpellData
    {
        [Tooltip("ID of spell type")]
        public SpellType Type;
        
        [Tooltip("Name of a spell")]
        public String Name;

        [Tooltip("Every spell has associated damage type")]
        public DamageType DamageType;
        
        public float Damage;

        [Tooltip("Prefab of a spell")]
        public SpellProjectile Prefab;

        [Tooltip("Number of spells precreated for optimization purposes")]
        public int InitialPoolSize = 4;
    }
}