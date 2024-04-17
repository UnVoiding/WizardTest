using System.Collections.Generic;
using Romeno.Utils;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class RoundManager : StrictSingleton<RoundManager>
    {
        // REFERENCES
        [SerializeField] 
        private MainCharacter MainCharacterPfb;
        [SerializeField]
        private Transform PlayerStartingPosition;

        // RUNTIME 
        [HideInInspector]
        public MainCharacter MainCharacter;
        [HideInInspector]
        public RoundState RoundState;
        private int MonstersToKill;
        private List<SpellProjectile> ProjectilesFired;

        protected override void Setup()
        {
            RoundState = RoundState.NotStarted;

            EnemySpawner.InitInstanceFromExistingGameObject();
            CameraManager.InitInstanceFromExistingGameObject();

            ProjectilesFired = new List<SpellProjectile>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                RestartRound();
            }
        }

        public void StartRound()
        {
            RoundState = RoundState.InProgress;
            
            MonstersToKill = DB.I.RoundSettings.MonstersToKill;
            UIManager.I.GetWidget<HUD>().SetMonstersLeftToWin(MonstersToKill);
            
            MainCharacter = Instantiate(MainCharacterPfb, PlayerStartingPosition.position, Quaternion.identity);
            MainCharacter.transform.SetParent(transform);
            MainCharacter.Init();

            EnemySpawner.I.Init();
            EnemySpawner.I.StartSpawning();
        }

        public void RestartRound()
        {
            RoundState = RoundState.InProgress;

            MonstersToKill = DB.I.RoundSettings.MonstersToKill;
            UIManager.I.GetWidget<HUD>().SetMonstersLeftToWin(MonstersToKill);

            MainCharacter.Init();
            MainCharacter.transform.position = PlayerStartingPosition.position;
            MainCharacter.transform.rotation = Quaternion.identity;
            
            EnemySpawner.I.Restart();

            foreach (var spellProjectile in ProjectilesFired)
            {
                spellProjectile.ReturnToPool();
            }
            ProjectilesFired.Clear();
        }

        public void OnMonsterKilled()
        {
            if (MonstersToKill > 0)
            {
                MonstersToKill--;
                
                UIManager.I.GetWidget<HUD>().SetMonstersLeftToWin(MonstersToKill);

                if (MonstersToKill <= 0)
                {
                    RoundState = RoundState.Victory;
                
                    UIManager.I.ShowWidget<RoundResultScreen>();
                    
                    CameraManager.I.DirectOverviewCamera(MainCharacter.transform.position + new Vector3(5, 10, 5), MainCharacter.transform.position);
                    CameraManager.I.ActivateOverviewCamera();
                }
            }
        }

        public void OnPlayerKilled()
        {
            RoundState = RoundState.Defeat;
                
            UIManager.I.ShowWidget<RoundResultScreen>();

            CameraManager.I.DirectOverviewCamera(MainCharacter.transform.position + new Vector3(5, 10, 5), MainCharacter.transform.position);
            CameraManager.I.ActivateOverviewCamera();
        }

        public void OnSpellFired(SpellProjectile projectile)
        {
            ProjectilesFired.Add(projectile);
        }

        public void OnSpellDestroyed(SpellProjectile spellProjectile)
        {
            ProjectilesFired.Remove(spellProjectile);
        }
    }
}