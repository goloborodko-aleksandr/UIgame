namespace Engine.AxGridUnityTools.Model
{
    public interface IEventManagerInvoke
    {
        void Invoke(string eventName, params object[] args);
    }
}