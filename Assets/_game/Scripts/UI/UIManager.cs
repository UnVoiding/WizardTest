using System;
using System.Collections.Generic;
using Romeno.Utils;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class UIManager : StrictSingleton<UIManager>
    {
        // REFERENCES
        [SerializeField, Tooltip("All UI widgets or screens that is available in the game")]
        private List<Widget> Widgets;

        protected override void Setup()
        {
            
        }

        public Widget ShowWidget(WidgetType widgetType)
        {
            Widget ret = GetWidget(widgetType);
            ret.Show();
            return ret;
        }

        public T ShowWidget<T>() where T : Widget
        {
            T ret = GetWidget<T>();
            ret.Show();
            return ret;
        }

        public Widget HideWidget(WidgetType widgetType)
        {
            Widget ret = GetWidget(widgetType);
            ret.Hide();
            return ret;
        }

        public T HideWidget<T>() where T : Widget
        {
            T ret = GetWidget<T>();
            ret.Hide();
            return ret;
        }

        public Widget GetWidget(WidgetType widgetType)
        {
            return Widgets.Find(m => m.WidgetType == widgetType);
        }
        
        public T GetWidget<T>() where T : Widget
        {
            Type type = typeof(T);
            return (T)Widgets.Find(x => x != null && x.GetType() == type);
        }
    }
}