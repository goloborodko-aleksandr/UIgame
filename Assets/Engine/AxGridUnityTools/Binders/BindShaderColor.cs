using Engine.AxGridUnityTools.Base;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders {
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class BindShaderColor : MonoBehaviourExt {

        public string field = "Colororize";
        public string shaderColorField = "_color_col";
        public Color defaultColor = Color.white;
        
        [OnStart]
        public void Bind() {
            Model.EventManager.AddAction($"On{field}Changed", Changed);
        }

        [OnStart]
        protected void Changed() {
            GetComponent<SpriteRenderer>().materials[0].SetColor(shaderColorField, Model.Get(field, defaultColor));    
        }
        
        [OnDestroy]
        public void UnBind() {
            Model.EventManager.RemoveAction($"On{field}Changed", Changed);
        }   
    }
}