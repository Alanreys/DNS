using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mono.Data.SqliteClient;
using System.Data;
using System.Globalization;
using Assets.Develop.Scripts.Models;
using System.IO;
using UnityEngine;

namespace Assets.Develop.Scripts.Database
{
    public class Where
    {
        public string Field;
        public string Sign;
        public string Value;

        public Where(string field, string sign, string value)
        {
            Field = field;
            Sign = sign;
            Value = value;
        }
        public override string ToString()
        {
            return " " + Field + " " + Sign + " @" + Field + " ";
        }
    }

    public class Field
    {
        public string Name;
        public object Value;

        public Field(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class Db
    {
        public static SqliteConnection CreateConnection()
        {
            string DB_PATH = Application.dataPath + "/Develop/Plugins/db.db";
            string DB_CONNECTION_STR = "URI=file:" + DB_PATH;

            try
            {
                var conn = new SqliteConnection(DB_CONNECTION_STR);
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                Logs.Logger.Error("Ошибка соеденения с основной базой данных", ex);
                throw;
            }
        }

        public static T ReadField<T>(IDataReader reader, string fieldName)
        {
            var value = reader[fieldName];
            var type = typeof(T);

            if (value == null)
                return (T)value;

            if (value.GetType() == typeof(DBNull))
                return default(T);

            if (type == typeof(Guid))
                return (T)(object)new Guid(ReadField<byte[]>(reader, fieldName));

            if (type == typeof(Int32))
                return (T)(object)Convert.ToInt32(value, CultureInfo.InvariantCulture);

            if (type == typeof(Int64))
                return (T)(object)Convert.ToInt64(value, CultureInfo.InvariantCulture);

            if (type == typeof(DateTime))
            {
                if (value.GetType() == typeof(DateTime))
                    return (T)value;

                return (T)(object)DateTime.Parse(value as string);
            }

            if (type == typeof(bool))
            {
                if (value.GetType() == typeof(byte[]))
                    return (T)(object)BitConverter.ToBoolean((byte[])value, 0);

                if (value.GetType() == typeof(decimal))
                    return (T)(object)System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);

                if (value.GetType() == typeof(string))
                {
                    string str = (string)value;
                    bool res;

                    if (!bool.TryParse(str, out res))
                    {
                        res = str == "1";
                    }

                    return (T)(object)res;
                }
            }

            return (T)value;
        }
    }
}
