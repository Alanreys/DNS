using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using Assets.Develop.Scripts.Controllers;
using Assets.Develop.Scripts.Models;
using Assets.Develop.Scripts.Utility;
using Assets.Develop.Scripts.Types;

namespace Assets.Develop.Scripts.Components
{
    public class UnknownObjectComponent : EditingComponent
    {
        private UnknownObjectComponent()
        {
            DefaultDropDownState();
        }

        public void SaveDataClick() {
            var productTypeId = _productTypeController.GetTypeIdByDataSourseIndex(ProductType);
            var ironTypeId = _ironTypeController.GetTypeIdByDataSourseIndex(IronType);

            _objectController.SaveObject(gameObject, Type, Name?.Trim(), Description?.Trim(), ironTypeId, productTypeId);
        }
    }
}
