using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.AxGridUnityTools.Binders {

    
    [RequireComponent(typeof(Image))]
    public class UIFieldSkin : MonoBehaviourExtBind {
        public string mainColorField = "MainColor";
        private Image image;
        
        
        [OnAwake]
        public void Init()
        {
            image = GetComponent<Image>();
        }
        
        [OnStart]
        [Bind("On{mainColorField}Changed")]
        public void mainColorFieldChanged()
        {
            image.color = Model.Get(mainColorField, Color.white);
        }

    }
}