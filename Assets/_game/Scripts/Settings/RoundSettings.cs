using UnityEngine;


namespace Romeno.WizardTest
{
    [CreateAssetMenu(fileName = "RoundSettings", menuName = "WizardTest/RoundSettings")]
    public class RoundSettings : ScriptableObject
    {
        [Tooltip("Wizard settings such as health, defence, available spells")]
        public WizardSettings WizardSettings;

        [Tooltip("Enemy spawner settings such as delays, spawning speed, and maximum enemies on the level")]
        public EnemySpawnerSettings SpawnerSettings;

        [Tooltip("List of enemy types and their settings")]
        public EnemySettings EnemySettings;
        
        [Tooltip("Monsters player needs to kill to win a round")]
        public int MonstersToKill;
        
        [Tooltip("At the start of a round will be countdown. After countdown player will be given control")]
        public int StartRoundCountdownTime;

        [Tooltip("Y of ground on the level")]
        public float GroundY = 0.5f;
    }
}