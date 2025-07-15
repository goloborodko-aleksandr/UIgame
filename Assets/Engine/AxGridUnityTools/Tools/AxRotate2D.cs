using Engine.AxGridUnityTools.Base;
using UnityEngine;

namespace Engine.AxGridUnityTools.Tools
{
    public class AxRotate2D : MonoBehaviourExt
    {
        public float speed = 0f;
        public bool enable = true;
        
        [OnUpdate]
        private void Rotate()
        {
            if (enable)
            {
                transform.Rotate(0,0,  speed * Time.deltaTime);
            }
        }
    }
}