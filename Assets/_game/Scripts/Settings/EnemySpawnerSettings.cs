using UnityEngine;


namespace Romeno.WizardTest
{
    [CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "WizardTest/EnemySpawnerSettings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        [Tooltip("Max enemies on a level at every given moment")]
        public int MaxEnemiesOnLevel = 10;
        
        [Tooltip("Monsters will spawn after this number of seconds after the countdown finishes")]
        public float InitialSpawnDelay = 3;

        [Tooltip("When monsters spawn behind main character this is the minimum distance they will spawn at")]
        public float MinSpawnDistance = 10;

        [Tooltip("When monsters spawn behind main character this is the maximum distance they will spawn at")]
        public float MaxSpawnDistance = 20;

        [Tooltip("Minimum number of seconds that needs to pass after the last monster was spawned to spawn a new one")]
        public float MinSpawnPeriod = 5;
        
        [Tooltip("Maximum number of seconds that needs to pass after the last monster was spawned to spawn a new one")]
        public float MaxSpawnPeriod = 10;

        [Tooltip("Chance to spawn a monster from behind main character (if possible)")]
        public float ChanceToSpawnFromBehind = 0.33f;
    }
}