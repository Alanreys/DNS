using System;

namespace Assets.Develop.Scripts.Logs
{
    public class Logger
    {
        /// <summary>
        /// Сохранение доп информации
        /// </summary>
        /// <param name="message">Доп сообщение</param>
        /// <param name="ex">Исключение</param>>
        public static void Info(string message = "") => DatabaseBackgroundStream.Stream.Write(LogType.Information, message);

        /// <summary>
        /// Сохранение предупреждения
        /// </summary>
        /// <param name="message">Доп сообщение</param>
        /// <param name="ex">Исключение</param>
        public static void Warning(string message = "") => DatabaseBackgroundStream.Stream.Write(LogType.Warning, message);
        

        /// <summary>
        /// Сохранение ошибки
        /// </summary>
        /// <param name="message">Доп сообщение</param>
        /// <param name="ex">Исключение</param>
        public static void Error(string message = "", Exception ex = null) => DatabaseBackgroundStream.Stream.Write(LogType.Exception, message, ex);
    }
}
