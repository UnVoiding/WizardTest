using UnityEngine;


namespace Romeno.WizardTest
{
    public class CollisionVisualEffect : MonoBehaviour, IPooledObject
    {
        // empty class made to distinguish the effects spawned by RFX1_TransformMotion for the PoolManager purposes 
        public GameObject Prefab { get; set; }

        public GameObject GameObject => gameObject;

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
    }
}