using System.Collections.Generic;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class Pool<PooledObject> where PooledObject: IPooledObject
    {
        private Dictionary<GameObject, List<PooledObject>> PoolsByPrefab = new();

        public void Allocate(GameObject prefab, int size)
        {
            var pool = GetPool(prefab);

            int toCreate = 0;
            if (pool.Count <= size)
            {
                toCreate = size - pool.Count; 
                for (int i = 0; i < toCreate; i++)
                {
                    CreatePooledObject(pool, prefab);
                }
            }
        }
        
        public PooledObject Get(GameObject prefab)
        {
            var pool = GetPool(prefab);
            var pooledObject = GetPooledObject(pool, prefab);
            pooledObject.OnGetFromPool();
            return pooledObject;
        }

        public void Return(PooledObject poolObject)
        {
            poolObject.OnReturnToPool();

            var pool = GetPool(poolObject.Prefab);
            pool.Add(poolObject);
            poolObject.GameObject.transform.SetParent(PoolManager.I.transform);
        }

        private List<PooledObject> GetPool(GameObject prefab)
        {
            if (!PoolsByPrefab.ContainsKey(prefab))
            {
                PoolsByPrefab[prefab] = new List<PooledObject>();
            }

            return PoolsByPrefab[prefab];
        }

        private void CreatePooledObject(List<PooledObject> pool, GameObject prefab)
        {
            var instance = GameObject.Instantiate (prefab, PoolManager.I.transform);
            var poolObject = instance.GetComponent<PooledObject>();
            poolObject.Prefab = prefab;
            
            pool.Add(poolObject);
            
            poolObject.OnCreate();
        }

        private PooledObject GetPooledObject(List<PooledObject> pool, GameObject prefab)
        {
            if (pool.Count <= 0)
            {
                CreatePooledObject(pool, prefab);
            }

            var pooledObject = pool[pool.Count - 1];
            
            pool.Remove(pooledObject);
            
            return pooledObject;
        }
    }
}