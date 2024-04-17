using System.Collections.Generic;
using UnityEngine;


namespace Romeno.WizardTest
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "WizardTest/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [Tooltip("List of available enemies")]
        public List<EnemyData> Enemies;

        public EnemyData GetEnemyData(EnemyType enemyType)
        {
            return Enemies.Find((data => data.Type == enemyType));
        }
    }
}