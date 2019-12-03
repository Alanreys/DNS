using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Develop.Scripts.Database.ORM;
using Assets.Develop.Scripts.Logs;
using Assets.Develop.Scripts.Models;

namespace Assets.Develop.Scripts.Database.Repository
{
    public class IronTypeRepository
    {
        public IronType GetByName(string name)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var reader = ORM.Select(IronType.Fields.Select(f => f.Name), IronType.TableName(), new Where[] { new Where("Name", "=", name) });

                if (reader.Read())
                    return IronType.FromReader(reader);

                return null;
            }
        }

        public List<IronType> GetAll()
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var types = new List<IronType>();
                var reader = ORM.Select(IronType.Fields.Select(f => f.Name), IronType.TableName());

                while (reader.Read())
                    types.Add(IronType.FromReader(reader));

                return types;
            }
        }

        public void Insert(string name) 
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                ORM.Insert(new Field[] { new Field("Name", name) }, IronType.TableName(), new Where[] { });
                Logger.Info($"Создан новый тип железа: {name}");
            }
        }
    }
}
