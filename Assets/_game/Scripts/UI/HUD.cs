using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Romeno.WizardTest
{
    public class HUD : Widget
    {
        // REFERENCES
        [SerializeField] 
        private Image HealthBarImage;
        [SerializeField] 
        private TMP_Text SelectedSpellText;
        [SerializeField] 
        private TMP_Text MonstersLeftToWinText;

        public void SetSelectedSpellName(String selectedSpellName)
        {
            SelectedSpellText.SetText(selectedSpellName);
        }

        public void SetHealth(float healthPercentage)
        {
            HealthBarImage.fillAmount = healthPercentage;
        }

        public void SetMonstersLeftToWin(int num)
        {
            MonstersLeftToWinText.text = num.ToString();
        }

        public override WidgetType WidgetType => WidgetType.HUD;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}