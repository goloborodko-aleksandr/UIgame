using UnityEngine;

namespace Engine.AxGridUnityTools.Tools
{
    
    public interface OKeySprite<out K>
    {
        K OKey { get; }
        Sprite OValue { get; }
    }
}