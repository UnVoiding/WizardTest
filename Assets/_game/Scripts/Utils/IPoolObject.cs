using UnityEngine;


namespace Romeno.WizardTest
{
    // pooled object
    public interface IPooledObject
    {
        GameObject Prefab { get; set; }
        GameObject GameObject { get; }
        void OnCreate();
        void OnGetFromPool();
        void OnReturnToPool();
    }
}