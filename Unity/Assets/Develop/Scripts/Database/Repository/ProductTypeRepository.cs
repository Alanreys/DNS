using System.Collections.Generic;
using System.Linq;
using Assets.Develop.Scripts.Database.ORM;
using Assets.Develop.Scripts.Logs;
using Assets.Develop.Scripts.Models;

namespace Assets.Develop.Scripts.Database.Repository
{
    public class ProductTypeRepository
    {
        public ProductType GetByName(string name)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var reader = ORM.Select(ProductType.Fields.Select(f => f.Name), ProductType.TableName(), new Where[] { new Where("Name", "=", name) });

                if (reader.Read())
                    return ProductType.FromReader(reader);

                return null;
            }
        }

        public List<ProductType> GetAll()
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                var types = new List<ProductType>();
                var reader = ORM.Select(ProductType.Fields.Select(f => f.Name), ProductType.TableName());

                while (reader.Read())
                    types.Add(ProductType.FromReader(reader));

                return types;
            }
        }

        public void Insert(string name)
        {
            using (var connection = Db.CreateConnection())
            using (var ORM = new DML(connection.CreateCommand()))
            {
                ORM.Insert(new Field[] { new Field("Name", name) }, ProductType.TableName(), new Where[] { });
                Logger.Info($"Создана новая категория: {name}");
            }
        }
    }
}
