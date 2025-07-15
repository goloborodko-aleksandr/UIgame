using System;
using System.IO;
using log4net;
using log4net.Config;
using UnityEngine;

namespace Engine.AxGridUnityTools
{
    public static class LoggingConfiguration
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Configure()
        {
            XmlConfigurator.Configure(new FileInfo($"{Application.dataPath}/log4net.config"));
        }
    }
    public class Log
    {
        public static ILog logger = LogManager.GetLogger(nameof(Log));

        public static void Debug(string message)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(message);
        }
        
        public static void Info(string message)
        {
            if (logger.IsInfoEnabled)
                logger.Info(message);
        }
        
        public static void Warn(string message)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(message);
        }
        
        public static void Error(string message)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message);
        }
        
        public static void Error(Exception ex)
        {
            if (logger.IsErrorEnabled)
                logger.Error(ex);
        }
        
        public static void Error(Exception ex, string message)
        {
            if (logger.IsErrorEnabled)
                logger.Error($"{message}: {ex.Message}\n{ex.StackTrace}");
        }
    }
}