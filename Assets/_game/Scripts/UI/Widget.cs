using UnityEngine;


namespace Romeno.WizardTest
{
    public abstract class Widget : MonoBehaviour
    {
        public abstract WidgetType WidgetType { get; }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}