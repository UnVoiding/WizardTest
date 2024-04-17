using System;
using Random = UnityEngine.Random;


namespace Romeno.WizardTest
{
    public static class EnumCache
    {
        private static Array EnemyTypeValues;
        private static Array SpellTypeValues;
        
        public static EnemyType GetRandomEnemyType()
        {
            if (EnemyTypeValues == null)
            {
                EnemyTypeValues = Enum.GetValues(typeof(EnemyType));
            }

            return (EnemyType) EnemyTypeValues.GetValue(Random.Range(1, EnemyTypeValues.Length));
        }

        public static SpellType GetSpellTypeIndexCyclical(SpellType current, int offset)
        {
            if (SpellTypeValues == null)
            {
                SpellTypeValues = Enum.GetValues(typeof(SpellType));
            }

            int spellCount = SpellTypeValues.Length - 1; 
            int newIndex = (spellCount + (int) current + offset % spellCount - 1) % spellCount + 1;
            return (SpellType) newIndex;
        }
    }
}