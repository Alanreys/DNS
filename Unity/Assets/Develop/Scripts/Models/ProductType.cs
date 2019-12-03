using Assets.Develop.Scripts.Database;
using System.Data;

namespace Assets.Develop.Scripts.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Field[] Fields { get; } = new Field[] {
            new Field("Id", "INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE"),
            new Field("Name", "TEXT NOT NULL UNIQUE"),
        };

        public ProductType() { }
        public ProductType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static string TableName()
        {
            return "ProductTypes";
        }

        public static ProductType FromReader(IDataReader reader)
        {
            return new ProductType
            {
                Id = Db.ReadField<int>(reader, "Id"),
                Name = Db.ReadField<string>(reader, "Name"),
            };
        }
    }
}
