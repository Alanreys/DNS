using UnityEngine;
using Assets.Develop.Scripts.Utility;
using Assets.Develop.Scripts.Controllers;

namespace Assets.Develop.Scripts.Components
{
    public class ProductTypeComponent
    {
        private readonly ProductTypeController _productTypeController = ControllersFactory.ProductTypeController();

        private string _name;
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }

        public MethodResult Validate()
        {
            var validationResult = new Validator().FiledsFilled(Name);
            if (validationResult.Failure)
                return validationResult;

            if (_productTypeController.HasType(Name))
                return MethodResult.Fail("Такая категория уже существует");

            return MethodResult.Ok();
        }
        
        public void SaveTypeClick()
        {
            _productTypeController.SaveType(Name);
            Debug.Log($"Категория {Name} успешно добавлена");
        }
    }
}