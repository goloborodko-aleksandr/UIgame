using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders {

    
    [RequireComponent(typeof(UnityEngine.UI.Text))]
    public class UITextSkin : MonoBehaviourExtBind {
        public string mainColorField = "MessageColor";
        private UnityEngine.UI.Text text;
        
        
        [OnAwake]
        public void Init()
        {
            text = GetComponent<UnityEngine.UI.Text>();
        }
        
        [OnStart]
        [Bind("On{mainColorField}Changed")]
        public void mainColorFieldChanged()
        {
            text.color = Model.Get(mainColorField, new Color(255f/255f, 249f/255f, 197f/255f, 1));
        }

    }
}