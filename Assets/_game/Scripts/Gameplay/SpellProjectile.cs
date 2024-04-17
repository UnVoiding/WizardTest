using UnityEngine;


namespace Romeno.WizardTest
{
    public class SpellProjectile : MonoBehaviour, IPooledObject
    {
        // REFERENCES
        [SerializeField] 
        public RFX1_TransformMotion TransformMotion;

        // RUNTIME
        [HideInInspector] 
        public SpellData Data;

        private void Awake()
        {
            TransformMotion.CollisionEnter += OnCollision;
        }

        public void Init(SpellData data)
        {
            Data = data;

            gameObject.SetActive(true);
            
            RoundManager.I.OnSpellFired(this);
        }

        public GameObject Prefab { get; set; }

        public GameObject GameObject => gameObject;

        public void OnCreate()
        {
            gameObject.SetActive(false);
        }

        public void OnGetFromPool()
        {
            TransformMotion.PreInit();
            gameObject.transform.SetParent(RoundManager.I.transform);
        }

        public void OnReturnToPool()
        {
            gameObject.SetActive(false);
        }

        private void OnCollision(object sender, RFX1_TransformMotion.RFX1_CollisionInfo collisionInfo)
        {
            EnemyCollisionLink link = collisionInfo.Hit.collider.GetComponent<EnemyCollisionLink>();
            if (link != null)
            {
                link.Enemy.HandleSpellHit(this);
            }
        }

        private void OnDestroy()
        {
            if (TransformMotion != null)
            {
                TransformMotion.CollisionEnter -= OnCollision;
            }
        }

        public void OnSpellDestroyed()
        {
            RoundManager.I.OnSpellDestroyed(this);
            PoolManager.I.Spells.Return(this);
        }

        public void ReturnToPool()
        {
            foreach (var collisionVFX in TransformMotion.CollidedVFXs)
            {
                PoolManager.I.CollisionVFXs.Return(collisionVFX);
            }
            
            PoolManager.I.Spells.Return(this);
        }
    }
}