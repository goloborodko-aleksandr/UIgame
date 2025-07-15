using UnityEngine;
using log4net.Appender;
using log4net.Core;

namespace Engine.AxGridUnityTools
{
    public class UnityAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            string message = RenderLoggingEvent(loggingEvent);
            switch (loggingEvent.Level.Name)
            {
                case "FATAL":
                case "ERROR":
                    Debug.LogError($"{message}");
                    break;
                case "WARN":
                    Debug.LogWarning($"{message}");
                    break;
                case "INFO":
                    Debug.Log($"{message}");
                    break;
                case "DEBUG":
                    Debug.Log($"{message}");
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }
    }
}