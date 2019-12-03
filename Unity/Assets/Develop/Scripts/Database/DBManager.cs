using Assets.Develop.Scripts.Database.ORM;
using Assets.Develop.Scripts.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Develop.Scripts.Database
{
    public class DBManager
    {
        public void CheckTables()
        {
            CheckTable(DNSEntity.TableName(), DNSEntity.Fields);
            CheckTable(IronType.TableName(), IronType.Fields);
            CheckTable(ProductType.TableName(), ProductType.Fields);
        }

        private void CheckTable(string table, Field[] fields)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DDL(connection.CreateCommand()))
            {
                var reader = ORM.BasicQuery($"SELECT name FROM sqlite_master WHERE type=\"table\" AND name=\"{table}\"");
                if (!reader.Read())
                    ORM.Create(table, fields);
            }
        }
    }
}
