using System;
using Engine.AxGridUnityTools.Base;
using SmartFormat;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders
{
    /// <summary>
    /// Биндит Model.SmartFormat к полю
    /// </summary>
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class UITextDataBindWithOverride : MonoBehaviourExt
    {
        [Header("События")]
        [Tooltip("Поля при изменении которых будет срабатывать собятие.")]
        public string[] fieldNames = new string[0];
        [Tooltip("Изменение любого поля модели")]
        public bool modelChanged = true;

        [Header("Форматироване")]
        [Tooltip("Smart.Format(format, model)")]
        public string format = "{Balance.Game}";

        [Tooltip("Взять формат перед выводом")]
        public bool isFormatField = false;
        public bool applyModelForFromatField = true;
        private UnityEngine.UI.Text uiText;

        [SerializeField, Tooltip("Если по этому ключю есть значение, заменить текст компонента на это значение")]
        private string overrideValueByModelData = null;

        [OnAwake]
        private void awake()
        {
            try
            {
                uiText = GetComponent<UnityEngine.UI.Text>();
            }
            catch (Exception e)
            {
                Log.Error($"Error get Component:{e.Message}");
            }
        }

        [OnStart]
        public virtual void start()
        {
            try
            {
                if (isFormatField)
                    if (applyModelForFromatField)
                        format = Smart.Format(Text.Text.Get(format), Settings.Model);
                    else
                        format = Text.Text.Get(format);
                if (modelChanged)
                    Settings.Model.EventManager.AddAction("ModelChanged", Changed);
                else
                    foreach (var fieldName in fieldNames)
                        Settings.Model.EventManager.AddAction($"On{fieldName}Changed", Changed);
                Changed();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        [OnDestroy]
        public void onDestroy()
        {
            try
            {
                if (modelChanged)
                    Settings.Model.EventManager.RemoveAction("ModelChanged", Changed);
                else
                    foreach (var fieldName in fieldNames)
                        Settings.Model.EventManager.RemoveAction($"On{fieldName}Changed", Changed);
            }
            catch (Exception) { }
        }


        protected void Changed()
        {
            //Log.Info($"Value of [{fieldNames.Aggregate((a,b) => a+","+b)}] changed to {Txt.Get(format, MainSettings.Client.Model)}");

            object overrideValue = string.IsNullOrEmpty(overrideValueByModelData) ? null : Model.Get(overrideValueByModelData, null);

            if (overrideValue != null)
                uiText.text = overrideValue.ToString();
            else
                uiText.text = Text.Text.Get(format, Model);
        }
    }
}