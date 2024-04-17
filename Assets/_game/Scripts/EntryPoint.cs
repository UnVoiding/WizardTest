using System.Collections;
using Romeno.Utils;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class EntryPoint : StrictSingleton<EntryPoint>
    {
        // REFERENCES
        [SerializeField] 
        private DB DBPrefab; 
        [SerializeField] 
        private UIManager UIManagerPrefab; 

        private void Start()
        {
            InitInstance(this);
            DontDestroyOnLoad(gameObject);
        }
        
        protected override void Setup()
        {
            DB.InitInstanceFromPrefab(DBPrefab);
            GameManager.InitInstanceFromEmptyGameObject();
            PoolManager.InitInstanceFromEmptyGameObject();
            UIManager.InitInstanceFromPrefab(UIManagerPrefab);
            RoundManager.InitInstanceFromExistingGameObject();
            
            GameManager.I.StartGame();
        }
    }
}