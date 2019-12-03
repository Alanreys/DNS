using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Assets.Develop.Scripts.Types;
using Assets.Develop.Scripts.Database;

namespace Assets.Develop.Scripts.Models
{
    public class DNSEntity
    {
        public int Id { get; set; }
        public string UniqName { get; set; }
        public ObjectTypes Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? IronType { get; set; }
        public int? ProductType { get; set; }

        public static Field[] Fields { get; } = new Field[] {
            new Field("Id", "INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE"),
            new Field("UniqName", "TEXT NOT NULL UNIQUE"),
            new Field("Type", "INTEGER NOT NULL"),
            new Field("Name", "TEXT NOT NULL"),
            new Field("Description", "TEXT"),
            new Field("IronType", "INTEGER"),
            new Field("ProductType", "INTEGER")
        }; 

        public DNSEntity() { }

        public DNSEntity(int id, string uniqName, ObjectTypes type, string name, string description, int? ironType, int? productType)
        {
            Id = id;
            UniqName = uniqName;
            Type = type;
            Name = name;
            Description = description;
            IronType = ironType;
            ProductType = productType;
        }

        public static string TableName()
        {
            return "Objects";
        }

        public static DNSEntity FromReader(IDataReader reader)
        {
            return new DNSEntity
            {
                Id = Db.ReadField<int>(reader, "Id"),
                Type = (ObjectTypes)Db.ReadField<int>(reader, "Type"),
                Name = Db.ReadField<string>(reader, "Name"),
                UniqName = Db.ReadField<string>(reader, "UniqName"),
                Description = Db.ReadField<string>(reader, "Description"),
                IronType = Db.ReadField<int?>(reader, "IronType"),
                ProductType = Db.ReadField<int?>(reader, "ProductType")
            };
        }
    }
}
