using Assets.Develop.Scripts.Database;
using System.Data;

namespace Assets.Develop.Scripts.Models
{
    public class IronType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static Field[] Fields { get; } = new Field[] {
            new Field("Id", "INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE"),
            new Field("Name", "TEXT NOT NULL UNIQUE"),
        };

        public IronType() { }
        public IronType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static string TableName()
        {
            return "IronTypes";
        }

        public static IronType FromReader(IDataReader reader)
        {
            return new IronType
            {
                Id = Db.ReadField<int>(reader, "Id"),
                Name = Db.ReadField<string>(reader, "Name"),
            };
        }
    }
}
