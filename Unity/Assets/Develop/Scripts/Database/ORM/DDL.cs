using System.Data;


namespace Assets.Develop.Scripts.Database.ORM
{
    public class DDL: SQLiteORM
    {
        public DDL(IDbCommand command) : base(command) { }

        public void Create(string table, Field[] fields)
        {
            var query = $"CREATE TABLE \"{table}\"(";

            foreach (var field in fields)
                query += $"\"{field.Name}\" {field.Value},";

            query = query.TrimEnd(',') + ")";

            _command.CommandText = query;
            _command.ExecuteReader();
        }
    }
}
