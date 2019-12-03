using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Assets.Develop.Scripts.Database.ORM
{
    public class DML : SQLiteORM
    {
        public DML(IDbCommand command) : base(command) { }

        public IDataReader Update(Field[] fields, string table, Where[] conditions = null)
        {
            var query = $"UPDATE {table} SET ";

            _command.Parameters.Clear();
            foreach (var field in fields)
                if (field.Value != null)
                {
                    query += $"{field.Name} = @{field.Name},";

                    var parameter = _command.CreateParameter();
                    parameter.ParameterName = "@" + field.Name;
                    parameter.Value = field.Value;

                    _command.Parameters.Add(parameter);
                }
            
            query = query.TrimEnd(',');

            query += GetWhereStrByConditions(conditions);

            _command.CommandText = query;
            return _command.ExecuteReader();
        }

        public IDataReader Insert(Field[] fields, string table, Where[] conditions = null)
        {
            var query = $"INSERT INTO {table} ";

            var fieldsStr = "(";
            var values = "(";

            _command.Parameters.Clear();
            foreach (var field in fields)
            {
                if (field.Value != null)
                {
                    fieldsStr += field.Name + ",";
                    values += "@" + field.Name + ",";

                    var parameter = _command.CreateParameter();
                    parameter.ParameterName = "@" + field.Name;
                    parameter.Value = field.Value;

                    _command.Parameters.Add(parameter);
                }
            }

            fieldsStr = fieldsStr.TrimEnd(',') + ")";
            values = values.TrimEnd(',') + ")";

            query += $"{fieldsStr} VALUES {values}";

            query += GetWhereStrByConditions(conditions);

            _command.CommandText = query;
            return _command.ExecuteReader();
        }

        public IDataReader Select(IEnumerable<string> fields, string table, Where[] conditions = null)
        {
            return Select(fields.ToArray(), table, conditions);
        }

        public IDataReader Select(string[] fields, string table, Where[] conditions = null)
        {
            var query = "SELECT ";
            _command.Parameters.Clear();

            fields.ToList().ForEach(field => query += field + ",");
            query = query.Remove(query.Length - 1);

            query += " FROM " + table;
            query += GetWhereStrByConditions(conditions);

            _command.CommandText = query;
            return _command.ExecuteReader();
        }

        private string GetWhereStrByConditions(Where[] conditions)
        {
            var result = "";

            if (conditions != null && conditions.Length != 0)
            {
                result += " WHERE ";

                foreach (var condition in conditions)
                {
                    result += condition.ToString();

                    var parameter = _command.CreateParameter();
                    parameter.ParameterName = "@" + condition.Field;
                    parameter.Value = condition.Value;

                    _command.Parameters.Add(parameter);
                }
                result.Remove(result.Length - 3);
            }

            return result;
        }
    }
}
