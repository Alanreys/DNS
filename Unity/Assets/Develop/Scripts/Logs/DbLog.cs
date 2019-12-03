using System;
using System.Data;
using System.IO;
using Mono.Data.SqliteClient;

namespace Assets.Develop.Scripts.Logs
{
    public class DbLog
    {
        public static string APP_DIR = "";
        private static readonly string DB_DIR = "/Develop/Logs";
        private static readonly string DB_NAME = "Logs.db";
        private static readonly string DB_PATH = $"{DB_DIR}/{DB_NAME}";
        private static readonly string DB_CONNECTION_SUBSTR = "URI=file:";

        /// <summary>
        /// Сохранения лога в базу данных c восстановлением
        /// Если при вставке выпало исключение, проверим существование таблицы и восстановим в случае отсутсвия
        /// </summary>
        /// <param name="type">Тип лога</param>
        /// <param name="message">Cообщение</param>
        /// <param name="ex">Стэк вызова</param>
        public static void Save(LogType type, string message, string stackTrace)
        {
            using (var connection = CreateConnection())
            using (var cmd = connection.CreateCommand())
            {
                try
                {
                    cmd.CommandText = @"INSERT INTO Logs (Date, Type, Message, StackTrace) 
                                VALUES (datetime('now'), @Type, @Message, @StackTrace)";


                    cmd.Parameters.Add(new SqliteParameter("@Type", type));
                    cmd.Parameters.Add(new SqliteParameter("@Message", message));
                    cmd.Parameters.Add(new SqliteParameter("@StackTrace", stackTrace));

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (LogTableExists())
                    {
                        throw ex;
                    }
                    else
                    {
                        CreateLogTable();
                        Save(type, message, stackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// Удаление логов старше одного месяца, с восстановлением
        /// Если при вставке удалении исключение, проверим существование таблицы и восстановим в случае отсутсвия
        /// </summary>
        public static void DeleteOldLogs()
        {
            using (var connection = CreateConnection())
            using (var cmd = connection.CreateCommand())
            {
                try
                {
                    cmd.CommandText = @"DELETE FROM Logs WHERE Date < datetime('now', '-1 month')";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (LogTableExists())
                    {
                        throw ex;
                    }
                    else
                    {
                        CreateLogTable();
                        DeleteOldLogs();
                    }
                }
            }
        }

        private static SqliteConnection CreateConnection()
        {
            try
            {
                var connection  = new SqliteConnection(DB_CONNECTION_SUBSTR + APP_DIR + DB_PATH);
                connection.Open();

                return connection;
            }
            catch (Exception ex)
            {
                var DBDirPath = APP_DIR + DB_DIR;

                if (!Directory.Exists(DBDirPath))
                {
                    Directory.CreateDirectory(DBDirPath);
                    return CreateConnection();
                }
                else
                {
                    throw ex;
                }
            }
        }

        private static bool LogTableExists()
        {
            using (var connection = CreateConnection())
            using (var cmd = connection.CreateCommand())
            {
                try
                {
                    cmd.CommandText = $"SELECT * FROM sqlite_master WHERE type=\"table\" AND name=\"Logs\"";
                    IDataReader reader = cmd.ExecuteReader();
                    return reader.Read();
                }
                catch
                {
                    return false;
                }
            }
        }

        private static void CreateLogTable()
        {
            try
            {
                using (var connection = CreateConnection())
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"CREATE TABLE Logs (
                                            Id            INTEGER     PRIMARY KEY AUTOINCREMENT,
                                            Date          DATE        NOT NULL,
                                            Type          INTEGER     NOT NULL,
                                            Message       NTEXT       NOT NULL,
                                            StackTrace    NTEXT       NOT NULL
                                        );";

                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }
    }
}
