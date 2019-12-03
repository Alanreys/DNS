using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Develop.Scripts.Database.Repository;
using Assets.Develop.Scripts.Components;
using Assets.Develop.Scripts.Models;
using Assets.Develop.Scripts.Types;

namespace Assets.Develop.Scripts.Controllers
{
    public class ObjectController
    {
        private readonly ObjectRepository _objectRepository = new ObjectRepository();

        public string HandleGameObjectName(string name) => Regex.Replace(name, @"(\s*\([0-9]+\))$", "");

        public void HandleAddedObject(GameObject gameObject) {

            var uniqName = HandleGameObjectName(gameObject.name);
            var obj = GetObjectByName(uniqName);

            if (obj == null && gameObject.GetComponent<UnknownObjectComponent>() == null)
            {
                UpdateObjectToUnknown(gameObject);
            }
            else
            {
                //AddSpawnerComponent(gameObject);
                AddDNSObjectComponent(gameObject, obj);
            }
        }

        public string[] GetObjectTypeNames()
        {
            var types = new List<string>();

            foreach (ObjectTypes type in Enum.GetValues(typeof(ObjectTypes)))
                types.Add(type.GetCategory());

            return types.ToArray();
        }

        public DNSEntity GetObjectByName(string uniqName) => _objectRepository.GetObjectByName(uniqName);

        public void SaveObject(GameObject gameObject, int type, string name, string description, int? ironType = null, int? productType = null)
        {
            var uniqName = HandleGameObjectName(gameObject.name);
            var obj = _objectRepository.Insert(uniqName, type, name, description, ironType, productType);

            foreach (var go in GameObject.FindGameObjectsWithTag("Unknown").Where(o => HandleGameObjectName(o.name) == uniqName))
            {
                AddDNSObjectComponent(go, obj);
                //AddSpawnerComponent(go);
            }
        }

        public void FindRenamed(GameObject gameObject) {
            if (!Enum.GetValues(typeof(ObjectTypes)).Cast<ObjectTypes>().Select(e => e.ToString()).ToList().Contains(gameObject.tag))
                return;

            var component = gameObject.GetComponent<DNSObjectComponent>();
            if (component == null || component.Entity == null)
                return;

            var newObjectName = HandleGameObjectName(gameObject.name);
            if (component.Entity.UniqName != newObjectName)
            {
                UpdateObjectToUnknown(gameObject);
                Logs.Logger.Info($"Объект {component.Entity.UniqName} переименован на {newObjectName}");
            }
        }

        public void ObjectNotFound(GameObject gameObject) {
            UpdateObjectToUnknown(gameObject);
        }

        public void UpdateObject(GameObject gameObject, DNSEntity entity)
        {
            var uniqName = HandleGameObjectName(gameObject.name);
            _objectRepository.Update(entity);

            foreach (var go in GameObject.FindGameObjectsWithTag(gameObject.tag).Where(o => HandleGameObjectName(o.name) == uniqName))
                UpdateDNSObjectComponent(gameObject, entity);
        }

        /// <summary>
        /// Метод возваращает размер объекта
        /// </summary>
        /// <param name="gameObject">Объект, у которого необходимо вычеслить размер</param>
        /// <returns>Ректор (высота, ширина, длина)</returns>
        public Vector3 GetObjectSize(GameObject gameObject)
        {
            var (min, max) = GetMinMaxVectors(gameObject);
            return max - min;
        }

        public List<GameObject> GetChildren(GameObject gameObject) 
        {
            var children = new List<GameObject>();
            foreach (Transform child in gameObject.transform)
            {
                children.Add(child.gameObject);
            }

            return children;
        }

        private (Vector3 min, Vector3 max) GetMinMaxVectors(GameObject gameObject) 
        {
            var children = GetChildren(gameObject);

            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

            if (children.Count > 0) {
                foreach (GameObject child in children) {
                    var childResult = GetMinMaxVectors(child);

                    max.x = Math.Max(childResult.max.x, max.x);
                    max.y = Math.Max(childResult.max.y, max.y);
                    max.z = Math.Max(childResult.max.z, max.z);

                    min.x = Math.Min(childResult.min.x, min.x);
                    min.y = Math.Min(childResult.min.y, min.y);
                    min.z = Math.Min(childResult.min.z, min.z);
                }
            } else {
                var renderer = gameObject.GetComponent<BoxCollider>();
                if (renderer == null)
                    gameObject.AddComponent<BoxCollider>();

                min = gameObject.GetComponent<BoxCollider>().bounds.min;
                max = gameObject.GetComponent<BoxCollider>().bounds.max;

                if (renderer == null)
                    UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<BoxCollider>(), true);
            }

            return (min, max);
        }

        private void UpdateObjectToUnknown(GameObject gameObject)
        {
            try
            {
                gameObject.tag = "Unknown";

                var objComponent = gameObject.GetComponent<DNSObjectComponent>();
                if (objComponent != null)
                    UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<DNSObjectComponent>(), true);

                if (gameObject.GetComponent<UnknownObjectComponent>() == null)
                    gameObject.AddComponent<UnknownObjectComponent>();
            }
            catch (Exception ex)
            {
                Logs.Logger.Error("UnknownObject: Ошибка добавления компонента", ex);
            }
        }

        private void AddDNSObjectComponent(GameObject gameObject, DNSEntity entity)
        {
            try
            {
                var objComponent = gameObject.GetComponent<UnknownObjectComponent>();
                if (objComponent != null)
                    UnityEngine.Object.DestroyImmediate(objComponent);

                gameObject.tag = entity.Type.ToString();
                gameObject.AddComponent<DNSObjectComponent>();
                gameObject.GetComponent<DNSObjectComponent>().Entity = entity;
            }
            catch (Exception ex)
            {
                Logs.Logger.Error("DNSObject: Ошибка добавления компонента", ex);
            }
        }

        private void AddSpawnerComponent(GameObject gameObject)
        {
            try
            {
                var objComponent = gameObject.GetComponent<ObjectSpawnerComponent>();
                if (objComponent != null)
                    UnityEngine.Object.DestroyImmediate(objComponent);

                gameObject.AddComponent<ObjectSpawnerComponent>();
            }
            catch (Exception ex)
            {
                Logs.Logger.Error("DNSObject: Ошибка добавления компонента для спавна объектов", ex);
            }
        }

        private void UpdateDNSObjectComponent(GameObject gameObject, DNSEntity entity)
        {
            try
            {
                gameObject.GetComponent<DNSObjectComponent>().Entity = entity;
                gameObject.tag = entity.Type.ToString();
            }
            catch (Exception ex)
            {
                Logs.Logger.Error("DNSObject: Ошибка обновления компонента DNSObject", ex);
            }
        }
    }
}
