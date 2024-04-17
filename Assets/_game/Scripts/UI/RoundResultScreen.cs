using TMPro;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class RoundResultScreen : Widget
    {
        // REFERENCES
        [SerializeField] 
        private TMP_Text MessageToPlayerText;
        
        public void RestartRound()
        {
            GameManager.I.RestartRound();
        }

        public void Quit()
        {
            GameManager.I.Quit();
        }
        
        public override WidgetType WidgetType => WidgetType.RoundResultScreen;
        
        public override void Show()
        {
            base.Show();

            Time.timeScale = 0;

            if (RoundManager.I.RoundState == RoundState.Victory)
            {
                MessageToPlayerText.text = "You Won!";
            }
            else if (RoundManager.I.RoundState == RoundState.Defeat)
            {
                MessageToPlayerText.text = "You Lost :(";
            }
            else
            {
                Debug.LogError($"Round is finished but state = {RoundManager.I.RoundState}. Should be either Victory or Defeat");
            }
        }
    }
}