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
    public class DNSObjectComponent : EditingComponent
    {
        public DNSEntity Entity;

        public MethodResult RefreshData() {
            var refreshedEntity = _objectController.GetObjectByName(_objectController.HandleGameObjectName(gameObject.name));
            if (refreshedEntity == null)
            {
                _objectController.ObjectNotFound(gameObject);
                return MethodResult.Fail("Объект не найден");
            }
            else
            {
                Entity = _objectController.GetObjectByName(_objectController.HandleGameObjectName(gameObject.name));
                return MethodResult.Ok();
            }
        }

        public string GetIronTypeNameById(int? id) => _ironTypeController.GetTypeNameById(id);

        public string GetProductTypeNameById(int? id) => _productTypeController.GetTypeNameById(id);

        public void UpdateDataClick()
        {
            UpdateEntity();
            _objectController.UpdateObject(gameObject, Entity);
        }

        public void EnableEditMode() => DefaultValues(Entity.Name, Entity.Description, (int)Entity.Type, Entity.IronType, Entity.ProductType);

        private void UpdateEntity()
        {
            var productTypeId = _productTypeController.GetTypeIdByDataSourseIndex(ProductType);
            var ironTypeId = _ironTypeController.GetTypeIdByDataSourseIndex(IronType);

            Entity = new DNSEntity(Entity.Id, Entity.UniqName, (ObjectTypes)Type, Name, Description, ironTypeId, productTypeId);
        }
    }
}
