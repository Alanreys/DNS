using System;
using System.Data;

namespace Assets.Develop.Scripts.Database.ORM
{
    public class SQLiteORM: IDisposable
    {
        protected IDbCommand _command;

        public IDataReader BasicQuery(string query)
        {
            _command.CommandText = query;
            IDataReader reader = _command.ExecuteReader();

            return reader;
        }

        public SQLiteORM(IDbCommand command)
        {
            _command = command;
        }

        ~SQLiteORM()
        {
            Dispose();
        }

        public void Dispose()
        {
            _command.Dispose();
            _command = null;
        }
    }
}
