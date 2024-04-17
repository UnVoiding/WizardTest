using Romeno.Utils;
using UnityEngine;


namespace Romeno.WizardTest
{
    // Holds most of the configuration for the game
    public class DB : StrictSingleton<DB>
    {
        [Tooltip("Settings pertaining to single game match")]
        public RoundSettings RoundSettings;
        
        protected override void Setup()
        {
            
        }
    }
}