using Engine.AxGridUnityTools.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.AxGridUnityTools.Binders {
    /// <summary>
    /// Для создания плавных прогресс баров
    /// Менять поле 0.0f - 1.0f
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class UISpriteFillAmount : MonoBehaviourExt {
        
        private Image _image;
        public string field = "FloatValue";

        [OnAwake]
        public void Init() {
            _image = GetComponent<Image>();
        }

        [OnStart]
        public void Binding() {
            Model.EventManager.AddAction($"On{field}Changed", OnChanged);
        }
        
        [OnStart]
        public void OnChanged() {
            _image.fillAmount = Model.GetFloat(field);
        }
        
        [OnDestroy]
        public void UnBinding() {
            Model.EventManager.RemoveAction($"On{field}Changed", OnChanged);
        }
        
    }
}