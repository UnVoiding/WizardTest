using System.Collections.Generic;
using UnityEngine;


namespace Romeno.WizardTest
{
    [CreateAssetMenu(fileName = "SpellSettings", menuName = "WizardTest/SpellSettings")]
    public class SpellSettings : ScriptableObject
    {
        [Tooltip("List of available spells")]
        public List<SpellData> Spells;
        
        public SpellData GetSpellData(SpellType spellType)
        {
            return Spells.Find((data => data.Type == spellType));
        }
    }
}