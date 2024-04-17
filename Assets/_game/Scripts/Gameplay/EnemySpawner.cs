using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Romeno.Utils;
using UnityEngine.AI;

using Random = UnityEngine.Random;


namespace Romeno.WizardTest
{
    public class EnemySpawner : StrictSingleton<EnemySpawner>
    {
        // SETTINGS
        [SerializeField, Tooltip("List of all places where enemies can spawn")] 
        private List<Transform> EnemySpawnPoints;
        
        // RUNTIME
        private List<Enemy> Enemies;
        private Coroutine SpawnCoroutine;
        private int OnlyOnstacleLayerMask;

        protected override void Setup()
        {
            Enemies = new List<Enemy>();
        }
        
        public void Init()
        {
            OnlyOnstacleLayerMask = 1 << LayerMask.NameToLayer("Obstacle");
        }

        public void StartSpawning()
        {
            SpawnCoroutine = StartCoroutine(SpawnEnemies());
        }
        
        IEnumerator SpawnEnemies()
        {            
            yield return new WaitForSeconds(DB.I.RoundSettings.SpawnerSettings.InitialSpawnDelay);

            while (true)
            {
                if (Enemies.Count < DB.I.RoundSettings.SpawnerSettings.MaxEnemiesOnLevel)
                {
                    SpawnEnemy(EnumCache.GetRandomEnemyType());
                }

                yield return new WaitForSeconds(Random.Range(DB.I.RoundSettings.SpawnerSettings.MinSpawnPeriod, DB.I.RoundSettings.SpawnerSettings.MaxSpawnPeriod));
            }
        }

        public void SpawnEnemy(EnemyType enemyType)
        {
            EnemyData data = DB.I.RoundSettings.EnemySettings.GetEnemyData(enemyType);

            Vector3 enemyPos;
            Quaternion enemyRot;
            if (Random.Range(0.0f, 1.0f) < DB.I.RoundSettings.SpawnerSettings.ChanceToSpawnFromBehind)
            {
                enemyPos = GetRandomPointBehindMainCharacter();
                // point not on NavMesh
                if (!NavMesh.SamplePosition(enemyPos, out NavMeshHit hit, 0.6f, NavMesh.AllAreas))
                {
                    enemyPos = GetRandomNotVisiblePredefinedSpawnPoint();
                }
            }
            else
            {
                enemyPos = GetRandomNotVisiblePredefinedSpawnPoint();
            }
            
            enemyRot = Quaternion.LookRotation(RoundManager.I.MainCharacter.transform.position - enemyPos);
            
            Enemy e = PoolManager.I.Enemies.Get(data.Prefab.gameObject);
            e.transform.position = enemyPos;
            e.transform.rotation = enemyRot;
            e.Init(data);
            Enemies.Add(e);
        }

        private Vector3 GetRandomPointBehindMainCharacter()
        {
            Vector3 enemyDir = Quaternion.Euler(0, Random.Range(90, 270), 0) * RoundManager.I.MainCharacter.transform.forward;
            float distance = Random.Range(DB.I.RoundSettings.SpawnerSettings.MinSpawnDistance, DB.I.RoundSettings.SpawnerSettings.MaxSpawnDistance);

            Vector3 retPos = RoundManager.I.MainCharacter.transform.position + enemyDir * distance;
            return new Vector3(retPos.x, DB.I.RoundSettings.GroundY, retPos.z);
        }
        
        private Vector3 GetRandomNotVisiblePredefinedSpawnPoint()
        {
            EnemySpawnPoints.Shuffle();
            foreach (var spawnPoint in EnemySpawnPoints)
            {
                Vector3 viewportPoint = CameraManager.I.Current.WorldToViewportPoint(spawnPoint.position);
                if (viewportPoint.x < 0 || viewportPoint.y < 0 || viewportPoint.z < 0)
                {
                    return spawnPoint.position;
                }
                else
                {
                    if (Physics.Raycast(RoundManager.I.MainCharacter.transform.position,
                            spawnPoint.position - RoundManager.I.MainCharacter.transform.position,
                            out RaycastHit hit, 
                            Mathf.Infinity,
                            OnlyOnstacleLayerMask,
                            QueryTriggerInteraction.Ignore))
                    {
                        return spawnPoint.position;
                    }
                }
            }

            Debug.LogError("Failed to find not visible predefined enemy spawn point");
            return Vector3.zero;
        }

        public void OnMonsterKilled(Enemy killed)
        {
            Enemies.Remove(killed);
        }

        public void Restart()
        {
            if (SpawnCoroutine != null)
            {
                StopCoroutine(SpawnCoroutine);
            }

            foreach (var enemy in Enemies)
            {
                PoolManager.I.Enemies.Return(enemy);
            }
            
            Enemies.Clear();
            
            StartSpawning();
        }
    }
}