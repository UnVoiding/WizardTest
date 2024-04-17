using TMPro;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class PreStartScreen : Widget
    {
        // REFERENCES
        [SerializeField] 
        private TMP_Text MessageToPlayerText;

        void Awake()
        {
            MessageToPlayerText.text = $"Destroy {DB.I.RoundSettings.MonstersToKill} monsters to win!";
        }
        
        public void StartRound()
        {
            GameManager.I.StartRound();
        }
        
        public override WidgetType WidgetType => WidgetType.PreStartScreen;
    }
}