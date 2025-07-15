using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using Engine.AxGridUnityTools.Path;

namespace Engine.AxGridUnityTools.Binders
{
    [UnityEngine.RequireComponent(typeof(UnityEngine.Animator))]
    public class BindAnimatorFloat: MonoBehaviourExtBind
    {
        public string fieldName = "";
        public string animatorFieldName = "Blend";
        public int max = int.MaxValue;
        
        
        
        
        [Bind]
        public void ModelChanged(string fieldName)
        {
            if (fieldName == this.fieldName)
            {
                int item = Settings.Model.Get<int>(fieldName, 0);
                Log.Info($"START:{item}");
                if (item < 0)
                    item = max-1;
                else if (item >= max)
                    item = 0;
                Settings.Model.SilentSet(fieldName, item);

                Transition(Animator.GetFloat(animatorFieldName), item);
            }
        }

        public void Transition(float from, float to)
        {
            Path = CPath.Create().EasingLinear(0.3f, from, to, v => Animator.SetFloat(animatorFieldName, v));
        }
    }
}