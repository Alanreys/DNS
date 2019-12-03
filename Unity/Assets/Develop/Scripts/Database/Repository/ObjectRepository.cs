using Assets.Develop.Scripts.Database.ORM;
using Assets.Develop.Scripts.Logs;
using Assets.Develop.Scripts.Models;
using System;
using System.Linq;

namespace Assets.Develop.Scripts.Database.Repository
{
    public class ObjectRepository
    {
        public DNSEntity GetObjectByName(string uniqName)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var reader = ORM.Select(DNSEntity.Fields.Select(f => f.Name), DNSEntity.TableName(), new Where[] { new Where("UniqName", "=", uniqName) });
                if (reader.Read())
                     return DNSEntity.FromReader(reader);

                return null;
            }
        }

        public DNSEntity Insert(string uniqName, int type, string name, string description, int? ironType, int? productType)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var fields = new Field[]
                {
                    new Field("UniqName", uniqName),
                    new Field("Type", type.ToString()),
                    new Field("Name", name),
                    new Field("Description", description),
                    new Field("IronType", ironType?.ToString()),
                    new Field("ProductType", productType?.ToString())
                };

                ORM.Insert(fields, DNSEntity.TableName(), new Where[] {});
                Logger.Info($"Добавлен новый объект {uniqName}");

                var reader = ORM.Select(DNSEntity.Fields.Select(f => f.Name), DNSEntity.TableName(), new Where[] { new Where("UniqName", "=", uniqName) });
                if (reader.Read())
                    return DNSEntity.FromReader(reader);



                return null;
            }
        }

        public void Update(DNSEntity entity)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var fields = new Field[]
                {
                    new Field("Type", ((int)entity.Type).ToString()),
                    new Field("Name", entity.Name),
                    new Field("Description", entity.Description),
                    new Field("IronType", entity.IronType?.ToString()),
                    new Field("ProductType", entity.ProductType?.ToString()),
                };

                ORM.Update(fields, DNSEntity.TableName(), new Where[] { new Where("Id", "=", entity.Id.ToString()) });
                Logger.Info($"Обновлена информация об объекте {entity.UniqName}");
            }
        }
    }
}