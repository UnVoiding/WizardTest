using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Romeno.WizardTest
{
    public class HealthBar : MonoBehaviour
    {
        // REFERENCES
        [SerializeField] 
        private Slider Slider;
        [SerializeField] 
        private Transform Target;
        [SerializeField]
        private TMP_Text NameText;
        
        // SETTING
        [SerializeField] 
        private Vector3 Offset;

        public void SetFillPercentage(float fill)
        {
            Slider.value = fill;
        }

        public void SetName(string name)
        {
            NameText.text = name;
        }

        public void Update()
        {
            transform.rotation = CameraManager.I.Current.transform.rotation;
            transform.position = Target.position + Offset;
        }
    }
}