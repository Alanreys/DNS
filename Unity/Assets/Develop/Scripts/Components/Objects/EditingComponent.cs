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
    public class EditingComponent : MonoBehaviour
    {
        protected readonly ObjectController _objectController = ControllersFactory.ObjectController();
        protected readonly IronTypeController _ironTypeController = ControllersFactory.IronTypeController();
        protected readonly ProductTypeController _productTypeController = ControllersFactory.ProductTypeController();

        private string _name;
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value; }
        }

        public int Type = -1;
        public string[] TypeDropDownDataSourse = new string[] { };

        public int? IronType;
        public string[] IronTypeDropDownDataSourse;

        public int? ProductType;
        public string[] ProductTypeDropDownDataSourse;

        private Vector3 _scale;
        private Vector3 _size;
        public Vector3 Size
        {
            get
            {
                if (_size == null || _size == Vector3.zero || gameObject.transform.localScale != _scale)
                {
                    var gameObjectSize = _objectController.GetObjectSize(gameObject);
                    var xsm = (float)Math.Round(gameObjectSize.x * 100, 2);
                    var ysm = (float)Math.Round(gameObjectSize.y * 100, 2);
                    var zsm = (float)Math.Round(gameObjectSize.z * 100, 2);

                    _scale = gameObject.transform.localScale;
                    _size = new Vector3(xsm, ysm, zsm);
                }

                return _size;
            }
        }

        protected EditingComponent()
        {
            TypeDropDownDataSourse = _objectController.GetObjectTypeNames();
        }

        public void DropDownChanged()
        {
            DefaultDropDownState();
            FillDropDownDataSourse();
        }

        public MethodResult Validate() => new Validator().FiledsFilled(Name ?? "", Type, IronType, ProductType);

        protected void FillDropDownDataSourse()
        {
            if (Type == (int)ObjectTypes.Product)
            {
                if (ProductType == null)
                    ProductType = -1;
                ProductTypeDropDownDataSourse = _productTypeController.GetProductTypes().Select(t => t.Name).ToArray();
            }
            else if (Type == (int)ObjectTypes.Iron)
            {
                if (IronType == null)
                    IronType = -1; ;
                IronTypeDropDownDataSourse = _ironTypeController.GetIronTypes().Select(t => t.Name).ToArray();
            }
        }

        protected void DefaultDropDownState()
        {
            ProductType = null;
            IronType = null;

            IronTypeDropDownDataSourse = null;
            ProductTypeDropDownDataSourse = null;
        }

        protected void DefaultValues(string name, string description, int type, int? ironType, int? productType)
        {
            Name = name;
            Description = description;
            Type = type;

            if (ironType != null)
                IronType = _ironTypeController.GetDataSourseIndexByTypeId(ironType);
            else
                IronType = null;

            if (productType != null)
                ProductType = _productTypeController.GetDataSourseIndexByTypeId(productType);
            else
                ProductType = null;

            FillDropDownDataSourse();
        }
    }
}
