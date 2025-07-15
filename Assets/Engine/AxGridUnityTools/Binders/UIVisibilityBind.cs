using Engine.AxGridUnityTools.Base;
using SmartFormat;

namespace Engine.AxGridUnityTools.Binders
{
    public class UIVisibilityBind: MonoBehaviourExtBind
    {
        public bool defaultValue = true;
        public bool invert = false;
        public string field = "Visible";
        
        [OnStart(RunLevel.High)]
        public void Bind()
        {
            Settings.Model.EventManager.AddAction(Smart.Format("On{0}Changed", field), OnFieldChange);
        }

        [OnStart(RunLevel.Low)]
        public void OnFieldChange()
        {
            var val = Settings.Model.GetBool(field, defaultValue);
            gameObject.SetActive(invert ? !val : val);
        }

        [OnDestroy]
        public void UnBind()
        {
            Settings.Model.EventManager.RemoveAction(Smart.Format("On{0}Changed", field), OnFieldChange);
        }
       
    }
}