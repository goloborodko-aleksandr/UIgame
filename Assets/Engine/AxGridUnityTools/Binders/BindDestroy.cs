using Engine.AxGridUnityTools.Base;

namespace Engine.AxGridUnityTools.Binders {
    
    public class BindDestroy : MonoBehaviourExt {

        public string field = "Destroy";

        [OnStart]
        public void Init() {
            Model.EventManager.AddAction($"On{field}", Changed);
        }

        [OnDestroy]
        public void UnBind() {
            Model.EventManager.RemoveAction($"On{field}", Changed);
        }

        private void Changed() {
            Destroy(gameObject);
        }
    }
}