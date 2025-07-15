using UnityEngine;

namespace Engine.AxGridUnityTools.Model
{
    public class AsyncExecutor : MonoBehaviour
    {
        public void Update()
        {
            Settings.Model?.EventManager.ExecuteAsync(deltaTime:Time.deltaTime);
        }
    }
}