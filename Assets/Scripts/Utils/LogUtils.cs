using System.Text;
using UnityEngine;

namespace Common.Utils
{
    /// <summary>
    /// ClassName: LogUtils
    /// Version:1.0
    /// Date:2020-9-23
    /// Author:JiaChunzhen
    /// </summary>
    /// <remarks>
    /// 日志打印工具类
    /// </remarks>>
    public class LogUtils
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            Debug,
            Warning,
            Error
        }

        /// <summary>
        /// 日志颜色
        /// </summary>
        public enum LogColor
        {
            Default,
            Green, //#00A000
            Blue, //#00FFFF
            Yellow, //#FFFF00
            Red //#FF0000
        }


        /// <summary>
        /// 打印日志
        /// 默认Debug
        /// 默认颜色
        /// </summary>
        /// <param name="args">日志信息，可传多参</param>
        public static void Log(params object[] args)
        {
#if DEVELOP
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                stringBuilder.Append(args[i]);
                stringBuilder.Append("  ");
            }

            Log(stringBuilder.ToString());
#endif
        }

        /// <summary>
        /// 打印日志
        /// 指定类型
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="args">日志信息，可传多参</param>
        public static void Log(LogType logType, params object[] args)
        {
#if DEVELOP
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                stringBuilder.Append(args[i]);
                stringBuilder.Append("  ");
            }

            Log(stringBuilder.ToString(), logType);
#endif
        }

        /// <summary>
        /// 打印日志
        /// 指定颜色
        /// </summary>
        /// <param name="logColor">日志颜色</param>
        /// <param name="args">日志信息，可传多参</param>
        public static void Log(LogColor logColor, params object[] args)
        {
#if DEVELOP
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                stringBuilder.Append(args[i]);
                stringBuilder.Append("  ");
            }

            Log(stringBuilder.ToString(), LogType.Debug, logColor);
#endif
        }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="logColor">日志颜色</param>
        /// <param name="args">日志信息，可传多参</param>
        public static void Log(LogType logType, LogColor logColor, params object[] args)
        {
#if DEVELOP
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                stringBuilder.Append(args[i]);
                stringBuilder.Append("  ");
            }

            Log(stringBuilder.ToString(), logType, logColor);
#endif
        }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="log">日志信息</param>
        /// <param name="logType">日志类型</param>
        private static void Log(string log, LogType logType = LogType.Debug, LogColor logColor = LogColor.Default)
        {
            switch (logColor)
            {
                case LogColor.Green:
                    log = "<color=#00A000>" + log + "</color>";
                    break;
                case LogColor.Blue:
                    log = "<color=#00FFFF>" + log + "</color>";
                    break;
                case LogColor.Yellow:
                    log = "<color=#FFFF00>" + log + "</color>";
                    break;
                case LogColor.Red:
                    log = "<color=#FF0000>" + log + "</color>";
                    break;
            }

            switch (logType)
            {
                case LogType.Debug:
                    Debug.Log(log);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(log);
                    break;
                case LogType.Error:
                    Debug.LogError(log);
                    break;
                default:
                    Debug.Log(log);
                    break;
            }
        }
    }
}