using UnityEngine;
using Assets.Develop.Scripts.Utility;
using Assets.Develop.Scripts.Controllers;

namespace Assets.Develop.Scripts.Components
{
    public class IronTypeComponent
    {
        private readonly IronTypeController _ironTypeController = ControllersFactory.IronTypeController();

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

            if (_ironTypeController.HasType(Name))
                return MethodResult.Fail("Такой тип железа уже существует");

            return MethodResult.Ok();
        }
        
        public void SaveTypeClick()
        {
            _ironTypeController.SaveType(Name);
            Debug.Log($"Тип железа {Name} успешно добавлен");
        }
    }
}