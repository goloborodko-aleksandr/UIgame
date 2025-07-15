using Engine.AxGridUnityTools.Base;

namespace Engine.AxGridUnityTools.Binders {
    
    public class BindAnimatorTrigger : MonoBehaviourExt {
        public string eventName = "";

        [OnStart]
        public void Bind() {
            Settings.Model.EventManager.AddAction<string>(eventName, Event);
        }
        
        [OnDestroy]
        public void UnBind() {
            Settings.Model.EventManager.RemoveAction<string>(eventName, Event);
        }
        
        public void Event(string triggerName) {
            if (triggerName == "Show")
                Model.EventManager.Invoke("OnWheelBonusReset");
            Animator.SetTrigger(triggerName);
        }
    }
}