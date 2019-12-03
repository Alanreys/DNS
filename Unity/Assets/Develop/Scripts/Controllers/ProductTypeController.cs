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
    public class ProductTypeController
    {
        private readonly ProductTypeRepository _productTypeRepository = new ProductTypeRepository();
        private static List<ProductType> ProductTypes = null;

        public List<ProductType> GetProductTypes()
        {
            if (ProductTypes == null)
                ProductTypes = GetAllTypes();

            return ProductTypes;
        }

        public int? GetTypeIdByDataSourseIndex(int? optionalIndex)
        {
            int index = optionalIndex ?? -1;
            if (index < 0 || index >= GetProductTypes().Count)
                return null;

            return ProductTypes[index].Id;
        }

        public int GetDataSourseIndexByTypeId(int? optionalIndex) => GetProductTypes().FindIndex(o => o.Id == optionalIndex);

        public string GetTypeNameById(int? id) => GetProductTypes().Find(t => t.Id == id)?.Name;

        public void SaveType(string name)
        {
            _productTypeRepository.Insert(name);
            RefreshTypes();
        }
        
        public List<ProductType> GetAllTypes() => _productTypeRepository.GetAll();

        public bool HasType(string name) => _productTypeRepository.GetByName(name) != null;

        private void RefreshTypes() => ProductTypes = GetAllTypes();
    }
}
