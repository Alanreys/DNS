using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Develop.Scripts.Logs
{
    public class DatabaseBackgroundStream
    {
        /// <summary>
        /// Интервал проверки заполненности очереди
        /// </summary>
        private const short CHECK_LOG_QUEUE_INTERVAL = 1000;


        /// <summary>
        /// Очередь логов
        /// </summary>
        private static ConcurrentQueue<Action> _actionQueue = new ConcurrentQueue<Action>();

        private static DatabaseBackgroundStream _stream = null;
        public static DatabaseBackgroundStream Stream
        {
            get
            {
                if (_stream == null)
                    _stream = new DatabaseBackgroundStream();

                return _stream;
            }
        }

        private DatabaseBackgroundStream()
        {
            DbLog.APP_DIR = Application.dataPath;
            DbLog.DeleteOldLogs();
            Task.Run(async () =>
            {
                while (true)
                {
                    DequeueAllActions();
                    await Task.Delay(CHECK_LOG_QUEUE_INTERVAL);
                }
            });
        }

        /// <summary>
        /// Добавление лога в очередь
        /// </summary>
        /// <param name="type">Тип лога</param>
        /// <param name="message">Доп сообщение</param>
        /// <param name="ex">Исключение</param>
        public void Write(LogType type, string message = "", Exception ex = null)
        {
            var exMessage = "";
            var exStack = "";

            if (ex != null)
            {
                exMessage = ex.Message;
                exStack = ToStringWithInnerExceptions(ex);
            }

            try
            {
                var callback = new Action(() => DbLog.Save(type, $"{message}\n{exMessage}", exStack));
                _actionQueue.Enqueue(callback);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw e;
            }
        }

        private void DequeueAllActions()
        {
            try
            {
                Action writeLogAction = null;
                while (_actionQueue.TryDequeue(out writeLogAction))
                {
                    writeLogAction?.Invoke();
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw e;
            }
        }

        private string ToStringWithInnerExceptions(Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append(ex.ToString());

            var inner = ex.InnerException;
            while (inner != null)
            {
                sb.Append("\n===INNER EXCEPTION===\n");
                sb.Append(inner.ToString());
                inner = inner.InnerException;
            }

            return sb.ToString();
        }
    }
}
