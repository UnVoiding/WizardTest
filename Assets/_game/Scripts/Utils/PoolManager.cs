using Romeno.Utils;


namespace Romeno.WizardTest
{
    // Holds all different pools
    public class PoolManager : StrictSingleton<PoolManager>
    {
        public Pool<Enemy> Enemies;
        public Pool<SpellProjectile> Spells;
        public Pool<CollisionVisualEffect> CollisionVFXs;

        protected override void Setup()
        {
            Enemies = new Pool<Enemy>();
            Spells = new Pool<SpellProjectile>();
            CollisionVFXs = new Pool<CollisionVisualEffect>();
        }

        public void Init()
        {
            foreach (var enemyData in DB.I.RoundSettings.EnemySettings.Enemies)
            {
                Enemies.Allocate(enemyData.Prefab.gameObject, enemyData.InitialPoolSize);
            }
            
            foreach (var spellData in DB.I.RoundSettings.WizardSettings.AvailableSpells.Spells)
            {
                Spells.Allocate(spellData.Prefab.gameObject, spellData.InitialPoolSize);
                foreach (var collisionEffect in spellData.Prefab.TransformMotion.EffectsOnCollision)
                {
                    CollisionVFXs.Allocate(collisionEffect, spellData.InitialPoolSize);
                }
            }
        }
    }
}