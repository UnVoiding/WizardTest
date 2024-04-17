using System.Collections;
using UnityEngine;
using UnityEngine.AI;


namespace Romeno.WizardTest
{
    public class Enemy : MonoBehaviour, IPooledObject
    {
        // REFERENCES
        [SerializeField]
        private NavMeshAgent NavMeshAgent;
        [SerializeField]
        private HealthBar HealthBar;

        // RUNTIME
        public EnemyData Data;
        private EnemyBehaviourState State;

        private float CurrentHealth;

        private Coroutine ReturnToChaseCoroutine;

        public void Init(EnemyData data)
        {
            Data = data;

            CurrentHealth = Data.Health;
            HealthBar.SetFillPercentage(CurrentHealth / Data.Health);
            HealthBar.SetName(Data.Name);
            State = EnemyBehaviourState.Chase;
            NavMeshAgent.speed = Data.MoveSpeed;
        }
        
        void Update()
        {
            if (State == EnemyBehaviourState.Chase)
            {
                if (NavMeshAgent != null)
                {
                    NavMeshAgent.isStopped = false;
                    NavMeshAgent.destination = RoundManager.I.MainCharacter.transform.position;
                }
            }
            else
            {
                if (NavMeshAgent != null)
                {
                    NavMeshAgent.isStopped = true;
                    NavMeshAgent.ResetPath();
                }
            }
        }

        public GameObject Prefab { get; set; }

        public GameObject GameObject
        {
            get { return gameObject; }
        }
        
        public void OnCreate()
        {
            gameObject.SetActive(false);
        }

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
            gameObject.transform.SetParent(RoundManager.I.transform);
        }

        public void OnReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public void HandleSpellHit(SpellProjectile spellProjectile)
        {
            float damage = spellProjectile.Data.Damage * (1 - Data.GetResistance(spellProjectile.Data.DamageType));
            Debug.Log("Damage: " + damage);

            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                HandleDeath();
            }
            else
            {
                HealthBar.SetFillPercentage(CurrentHealth / Data.Health);
            }
        }

        private void HandleDeath()
        {
            PoolManager.I.Enemies.Return(this);

            RoundManager.I.OnMonsterKilled();
            EnemySpawner.I.OnMonsterKilled(this);
        }

        public void AttackMainCharacter()
        {
            State = EnemyBehaviourState.Wait;
            RoundManager.I.MainCharacter.TakeDamage(Data.Damage);
            if (ReturnToChaseCoroutine != null)
            {
                StopCoroutine(ReturnToChaseCoroutine);
            }
            ReturnToChaseCoroutine = StartCoroutine(ReturnToChaseState());
        }

        private IEnumerator ReturnToChaseState()
        {
            yield return new WaitForSeconds(Data.ReturnToChaseStateTime);

            State = EnemyBehaviourState.Chase;
        }
    }
}