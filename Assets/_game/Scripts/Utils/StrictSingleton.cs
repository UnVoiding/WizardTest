using System.Linq;
using UnityEngine;


namespace Romeno.Utils
{
    public abstract class StrictSingleton<T> : MonoBehaviour where T : StrictSingleton<T>
    {
        protected static T Instance;
        public static bool SingletonInitialized = false;

        // you should create instance yourself when using StrictSingleton
        // StrictSingleton is just a hollow shell of a regular Singleton
        // it provides only a fancy way of accessing the Singleton's object
        // using familiar Instance method but you control how and when it is initialized.
        // Alternatively you can use other Init methods below
        // which will create the instance for you. 
        public static void InitInstance(T inst)
        {
            if (SingletonInitialized) return;

            Instance = inst;
            Instance.name = $"[{typeof(T).Name}]";
            SingletonInitialized = true;
            Instance.Setup();
            
            Debug.Log($"StrictSingleton {typeof(T).Name} was initialized");
        }

        private static string GetGenericClassName()
        {
            return  $"StrictSingleton<{typeof(StrictSingleton<T>).GenericTypeArguments.First().Name}>" ;
        }
        
        public static void InitInstanceFromEmptyGameObject()
        {
            if (SingletonInitialized) return;

            var go = new GameObject();
            InitInstance(go.AddComponent<T>());
        }

        public static void InitInstanceFromExistingGameObject()
        {
            if (SingletonInitialized) return;
            
            InitInstance(FindObjectOfType<T>());
        }

        public static void InitInstanceFromPrefab(T prefab)
        {
            if (SingletonInitialized) return;
            
            InitInstance(Instantiate(prefab));
        }

        protected abstract void Setup();

        public static T I
        {
            get
            {
                if (!SingletonInitialized)
                {
                    Debug.LogError($"{typeof(T).FullName}.Instance called before Init. You should call Init manually for StrictSingleton then access Instance");
                    return null;
                }
                else
                {
                    return Instance;
                }
            }
        }

        public static void ResetInstance()
        {
            Instance = null;
            SingletonInitialized = false;
        }

        private void OnDestroy()
        {
            Debug.Log($"StrictSingleton {typeof(T).FullName} was destroyed");

            ResetInstance();
        }
    }
}
