using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using UnityEngine;
using Assets.Develop.Scripts.Components;
using Assets.Develop.Scripts.Types;

namespace Assets.Develop.Scripts.Utility
{
    public class CSVCreator
    {
        private class GameObjectInfo
        {
            public string Name;
            public uint Count;

            public GameObjectInfo(string name)
            {
                Name = name;
                Count = 1;
            }
        }

        public readonly static string format = ".csv";
        public readonly static string directoryName = "Reports";

        private string _path;
        private string _text = "";

        public static string BuildFilePath(string name) => $"{Directory.GetCurrentDirectory()}/{directoryName}/{name}{format}";

        public CSVCreator(string fileName, bool uniq = true)
        {
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);

            var date = DateTime.Now;
            var dateSubstring = $"{date.Day}-{date.Month}-{date.Year}_{date.Hour}-{date.Minute}-{date.Second}";
            var uniqSubstr = uniq ? $"_{dateSubstring}" : string.Empty;

            _path = BuildFilePath(fileName+uniqSubstr);
        }

        public void SaveFile()
        {
            if (File.Exists(_path))
                File.Delete(_path);

            using (var sw = new StreamWriter(File.Open(_path, FileMode.CreateNew), Encoding.UTF8))
            {
                sw.WriteLine(_text);
            }
        }

        public void AddShowCases(GameObject[] gameObjects)
        {
            var showcaseTrees = new List<string>();

            foreach (var gameObject in gameObjects)
                showcaseTrees.Add(ShowCaseTree(gameObject));

            while (showcaseTrees.Count != 0)
            {
                var tree = showcaseTrees[0];
                _text += $"{showcaseTrees.RemoveAll(t => t == tree)} штук:\n {tree}\n\n";
            }
        }

        public void AddObjects(ObjectTypes type)
        {
            var dict = new Dictionary<string, GameObjectInfo>();
            var gameObjects = GameObject.FindGameObjectsWithTag(type.ToString());
            if (gameObjects.Length == 0)
                return;

            AddHeader(type.GetCategory());
            foreach (var gameObject in gameObjects)
            {
                var component = gameObject.GetComponent<DNSObjectComponent>();
                if (component.Entity == null && component.RefreshData().Failure)
                    return;

                if (dict.ContainsKey(component.Entity.UniqName))
                    dict[component.Entity.UniqName].Count++;
                else
                    dict[component.Entity.UniqName] = new GameObjectInfo(component.Entity.Name);
            }

            foreach (var item in dict)
                _text += $"    {item.Value.Name}    {item.Value.Count}шт\n";

            _text += "\n";
        }

        public void AddHeader(string text) => _text += $"{text}:\n\n";

        public string GetFullPath() => _path;

        private string ShowCaseTree(GameObject root, int depth = 0)
        {
            var component = root.GetComponent<DNSObjectComponent>();
            if (component.Entity == null && component.RefreshData().Failure)
                return string.Empty;

            var text = new string(' ', depth * 4) + component.Entity.Name + '\n';

            foreach (var child in root.GetComponentsInChildren<Transform>())
                if (child.gameObject.tag == ObjectTypes.Iron.ToString() && child.parent.gameObject == root)
                    text += ShowCaseTree(child.gameObject, depth + 1);
            return text;
        }
    }
}
