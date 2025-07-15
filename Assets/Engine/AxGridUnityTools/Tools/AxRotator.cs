using Engine.AxGridUnityTools.Base;
using UnityEngine;

namespace Engine.AxGridUnityTools.Tools
{
    public class AxRotator : MonoBehaviourExt
    {
        public Vector3 spped = Vector3.zero;
        public bool enable = true;

        [OnUpdate]
        public void Rotate()
        {
            if (enable)
            {
                this.transform.Rotate(spped * Time.deltaTime);
            }
        }
    }
}