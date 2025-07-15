using Engine.AxGridUnityTools.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Engine.AxGridUnityTools.Binders
{
    
    [RequireComponent(typeof(Button))]
    public class UIButtonLongPressDataBind : MonoBehaviourExt
    {
        private Button button;
        /// <summary>
        /// Имя кнопки (если пустое берется из имени объекта)
        /// </summary>
        public string buttonName = "";


        private string enableField = "";

        /// <summary>
        /// Включена по умолчанию
        /// </summary>
        public bool defaultEnable = true;

        /// <summary>
        /// Поле из модели где взять настройку клавиатуры
        /// </summary>
        public string keyField = "";
		
        /// <summary>
        /// Кнопка клавиатуры (заполнится из модели если там есть)
        /// </summary>
        public string key = "";

        public float longPressWait = 1.2f;
        
        private EventTrigger et;
        private bool longPressDown = false;
        private float longPressTime = 0.0f;
        private bool enable = true;

        private UIButtonDataBind normalButton;
        
        [OnAwake]
        public void awake()
        {
            button = GetComponent<Button>();
            normalButton = GetComponent<UIButtonDataBind>();
            if (string.IsNullOrEmpty(buttonName))
                buttonName = name;
            enableField = $"Btn{name}Enable";
            et = gameObject.AddComponent<EventTrigger>();
            var entryDown = new EventTrigger.Entry {eventID = EventTriggerType.PointerDown};
            entryDown.callback.AddListener(OnPointDown);
            et.triggers.Add(entryDown);

            var entryUp = new EventTrigger.Entry {eventID = EventTriggerType.PointerUp};
            entryUp.callback.AddListener(OnPointUp);
            et.triggers.Add(entryUp);
            
            var entryExit = new EventTrigger.Entry {eventID = EventTriggerType.PointerExit};
            entryUp.callback.AddListener(OnPointUp);
            et.triggers.Add(entryExit);
        }

        private void OnPointDown(BaseEventData bd)
        {
            longPressTime = longPressWait;
            longPressDown = true;
        }
        
        [OnEnable]
        [OnDisable]
        void OnHide()
        {
            longPressDown = false;
        }
        
        private void OnPointUp(BaseEventData bd)
        {
            longPressDown = false;
        }

        public void OnClick()
        {
            if (!enable) return;
            if (!button.interactable || !isActiveAndEnabled) return;
            if (normalButton!=null) normalButton.CancelClick();
            Settings.Fsm?.Invoke("OnBtn", buttonName);
            Settings.Model?.EventManager.Invoke($"On{buttonName}Click");
        }
        
        [OnStart]
        public void Init()
        {
            Settings.Model.EventManager.AddAction($"On{enableField}Changed", OnItemEnable);
            if (keyField == "")
                keyField = $"{name}LongKey";
            if (keyField != "")
            {
                key = Model.GetString(keyField, key);
                Model.EventManager.AddAction($"On{keyField}Changed", OnKeyChanged);
            }
        }

        protected void OnKeyChanged()
        {
            key = Model.GetString(keyField);
        }
            

        [OnDestroy]
        public void UnBind() {
            Settings.Model.EventManager.RemoveAction($"On{enableField}Changed", OnItemEnable);
            Settings.Model.EventManager.RemoveAction($"On{keyField}Changed", OnKeyChanged);
        }

        [OnStart]
        public void OnItemEnable()
        {
            enable = Settings.Model.GetBool(enableField, defaultEnable);
        }


        [OnUpdate]
        public void CheckLongPress()
        {
            if (key != "" && Input.GetKeyUp(key) && button.interactable)
                OnClick();
                
            
            if (!longPressDown) return;
            longPressTime -= Time.deltaTime;
            if (!(longPressTime <= 0)) return;
            longPressDown = false;
            OnClick();
        }
    }
}