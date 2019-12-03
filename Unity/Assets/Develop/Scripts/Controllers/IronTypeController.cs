using Assets.Develop.Scripts.Database.Repository;
using Assets.Develop.Scripts.Models;
using Assets.Develop.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Develop.Scripts.Controllers
{
    public class IronTypeController
    {
        private readonly IronTypeRepository _ironTypeRepository = new IronTypeRepository();
        private static List<IronType> IronTypes = null;

        public List<IronType> GetIronTypes()
        {
            if (IronTypes == null)
                IronTypes = GetAllTypes();

            return IronTypes;
        }

        public int? GetTypeIdByDataSourseIndex(int? optionalIndex)
        {
            int index = optionalIndex ?? -1;
            if (index < 0 || index >= GetIronTypes().Count)
                return null;

            return IronTypes[index].Id;
        }

        public int GetDataSourseIndexByTypeId(int? optionalIndex) => GetIronTypes().FindIndex(o => o.Id == optionalIndex);

        public string GetTypeNameById(int? id) => GetIronTypes().Find(t => t.Id == id)?.Name;

        public void SaveType(string name)
        {
            _ironTypeRepository.Insert(name);
            RefreshTypes();
        }
        
        public List<IronType> GetAllTypes() => _ironTypeRepository.GetAll();

        public bool HasType(string name) => _ironTypeRepository.GetByName(name) != null;

        private void RefreshTypes() => IronTypes = GetAllTypes();
    }
}
