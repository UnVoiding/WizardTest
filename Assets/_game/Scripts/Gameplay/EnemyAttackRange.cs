using UnityEngine;


namespace Romeno.WizardTest
{
    public class EnemyAttackRange : MonoBehaviour
    {
        // REFERENCES
        [SerializeField] 
        private Enemy Enemy;

        // RUNTIME
        private float LastAttackTime;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("MainCharacter"))
            {
                if (Time.time - LastAttackTime > Enemy.Data.AttackPeriod)
                {
                    LastAttackTime = Time.time;
                    
                    Enemy.AttackMainCharacter();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("MainCharacter"))
            {
                if (Time.time - LastAttackTime > Enemy.Data.AttackPeriod)
                {
                    LastAttackTime = Time.time;
                    
                    Enemy.AttackMainCharacter();
                }
            }
        }
    }
}