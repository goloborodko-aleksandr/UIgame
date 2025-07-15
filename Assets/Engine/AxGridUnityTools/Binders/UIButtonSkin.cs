using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.AxGridUnityTools.Binders
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class UIButtonSkin : MonoBehaviourExtBind
    {
        public string colorBlockField = "ColorBlock";
        public string mainColorField = "MainColor";
        
        private Button button;
        private Image image;

        [OnAwake]
        public void Init()
        {
            button = GetComponent<Button>();
            image = GetComponent<Image>();
        }

        [OnStart]
        public void colorBlockFieldChanged()
        {
            button.colors = Model.Get(colorBlockField, new ColorBlock
            {
                normalColor = Color.white,
                highlightedColor = Color.white,
                pressedColor = new Color(0.9f,0.9f,0.9f,1.0f),
                disabledColor = new Color(0.7f,0.7f,0.7f,1.0f),
                colorMultiplier = 1.0f,
                fadeDuration = 0.1f
            });
        }
        
        [OnStart]
        [Bind("On{mainColorField}Changed")]
        public void mainColorFieldChanged()
        {
            image.color = Model.Get(mainColorField, Color.white);
        }
    }
}