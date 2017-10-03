using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Repository;
using log4net.Appender;
using log4net.Config;


namespace PUCIT.AIMRL.Common.Logger
{
    public class LogHandler
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(LogHandler));
        //private static readonly ILog logger = LogManager.GetLogger("DefaultAppender");

        public static void WriteLog(string userName, string msg, LogType msgType)
        {
            switch (msgType)
            {                    
                case LogType.DebugMsg:
                    logger.Debug("[" + userName + "]: " + msg);
                    break;
                case LogType.ErrorMsg:
                    logger.Error("[" + userName + "]: " + msg);
                    break;
                case LogType.InfoMsg:
                    logger.Info("[" + userName + "]: " + msg);
                    break;
                case LogType.WarningMsg:
                    logger.Warn("[" + userName + "]: " + msg);
                    break;
                case LogType.FatalErrorMsg:
                    logger.Fatal("[" + userName + "]: " + msg);
                    break;
                default:
                    break;
            }
            FlushBuffers();
        }

        public static void WriteLog(string userName, string msg, LogType msgType, Exception exc)
        {
            switch (msgType)
            {
                case LogType.DebugMsg:
                    logger.Debug("[" + userName + "]: " + msg, exc);
                    break;
                case LogType.ErrorMsg:
                    logger.Error("[" + userName + "]: " + msg, exc);
                    break;
                case LogType.InfoMsg:
                    logger.Info("[" + userName + "]: " + msg, exc);
                    break;
                case LogType.WarningMsg:
                    logger.Warn("[" + userName + "]: " + msg, exc);
                    break;
                case LogType.FatalErrorMsg:
                    logger.Fatal("[" + userName + "]: " + msg, exc);
                    break;
                default:
                    break;
            }
            FlushBuffers();
        }

        private static void FlushBuffers()
        {
            ILoggerRepository rep = LogManager.GetRepository();
            foreach (IAppender appender in rep.GetAppenders())
            {
                var buffered = appender as BufferingAppenderSkeleton;
                if (buffered != null)
                {
                    buffered.Flush();
                }
            }
        }

        public static void ConfigureLogger(String loggingConfigFilePath)
        {
            //var file = new System.IO.FileInfo("logging.config");
            var file = new System.IO.FileInfo(loggingConfigFilePath);
            log4net.Config.XmlConfigurator.Configure(file);
        }
    }
    public enum LogType
    {
        DebugMsg,
        ErrorMsg,
        InfoMsg,
        WarningMsg,
        FatalErrorMsg
    }
}
