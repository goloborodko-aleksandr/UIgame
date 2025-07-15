

using Engine.AxGridUnityTools;
using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;

namespace AppZino.Tools.Binders
{
    [UnityEngine.RequireComponent(typeof(UnityEngine.Animator))]
    public class BindAnimatorBoolean : MonoBehaviourExtBind
    {
        public string fieldName = "";
        public string animatorFieldName = "Enable";
        
        [Bind]
        public void ModelChanged(string fieldName)
        {
            if (fieldName == this.fieldName)
                Animator.SetBool(animatorFieldName, Settings.Model.GetBool(fieldName));
        }
    }
}