using UnityEngine;


namespace Romeno.WizardTest
{
    [CreateAssetMenu(fileName = "WizardSettings", menuName = "WizardTest/WizardSettings")]
    public class WizardSettings : ScriptableObject
    {
        [Tooltip("Maximum & starting health of main character")]
        public float MaxHealth;

        [Tooltip("Damage to main character is reduced by the defence value: finalDmg = dmg - defence")]
        public int Defence = 1;

        [Tooltip("Speed at which main character moves")]
        public float MoveSpeed;

        [Tooltip("Wizard can cast spells as often as this number of seconds")]
        public float AttackPeriod = 1;

        [Tooltip("Speed at which main character turns")]
        public float TurnRate;

        [Tooltip("All available spells")]
        public SpellSettings AvailableSpells;
    }
}