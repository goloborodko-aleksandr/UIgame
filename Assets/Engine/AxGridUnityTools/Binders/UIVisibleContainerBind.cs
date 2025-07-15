using System;
using System.Collections.Generic;
using Engine.AxGridUnityTools.Base;
using SmartFormat;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders
{
    /// <summary>
    /// Создает связь меду полем и визуализацией обекта
    /// </summary>
    public class UIVisibleContainerBind : MonoBehaviourExt
    {

        [Serializable]
        public class UIItem
        {
            public string name;
            public GameObject obj;
        }
        
        /// <summary>
        /// Ручное заполнение
        /// </summary>
        public UIItem[] items;


        public Transform[] exclude;
        
        /// <summary>
        /// Поле в модели
        /// </summary>
        public string field = "";

        public bool integer = false;
        
        private Dictionary<string, GameObject> components;

        
        /// <summary>
        /// Что показывать по дефолту
        /// </summary>
        public string show = "";
        
        [OnAwake]
        public void GetComponent()
        {
            components = new Dictionary<string, GameObject>();
            List<Transform> excludes = new List<Transform>(exclude);
            if (string.IsNullOrEmpty(field))
                field = name;

            foreach (var item in items)
                components.Add(item.name, item.obj);      
            
            foreach (var t in GetComponentsInChildren<Transform>())
                if (t != transform && t.parent == transform && !excludes.Contains(t))
                    if (!components.ContainsValue(t.gameObject))
                        components.Add(t.name, t.gameObject);
        }

        [OnStart(RunLevel.High)]
        public void Bind()
        {
            Settings.Model.EventManager.AddAction(Smart.Format("On{0}Changed", field), OnFieldChange);
        }

        [OnStart(RunLevel.Low)]
        public void OnFieldChange()
        {
            if (integer)
            {
                var ishow = Settings.Model.GetInt(field);
                show = ishow.ToString();

            }else
                show = Settings.Model.GetString(field, show);
            foreach(var kv in components)
                kv.Value.SetActive(kv.Key == show);
        }

        [OnDestroy]
        public void UnBind()
        {
            Settings.Model.EventManager.RemoveAction(Smart.Format("On{0}Changed", field), OnFieldChange);
        }
        
    }
}