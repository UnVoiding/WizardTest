using System.Collections;
using TMPro;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class CountdownWidget : Widget
    {
        [SerializeField] 
        private TMP_Text CountdownText; 
        
        public override WidgetType WidgetType => WidgetType.CountdownWidget;

        public void StartCountdown(int seconds)
        {
            StartCoroutine(StartCountdownCoroutine(seconds));
        }

        private IEnumerator StartCountdownCoroutine(int seconds)
        {
            Time.timeScale = 0;
            
            int i = seconds;
            while (i > 0)
            {
                CountdownText.text = i.ToString(); 
                yield return new WaitForSecondsRealtime(1);
                i--;
            }
            
            Time.timeScale = 1;

            UIManager.I.HideWidget<CountdownWidget>();
        }
    }
}