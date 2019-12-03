using Assets.Develop.Scripts.Types;
using Assets.Develop.Scripts.Utility;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;

namespace Assets.Develop.Scripts.Controllers
{
    public class AppController
    {
        private static Process _process;

        public readonly string defaultShowcasesReportName = "Отчет_о_витринах";
        public readonly string defaultObjectsReportName = "Отчет_об_объектах";

        #if UNITY_EDITOR
        public bool HasUnknownObjects()
        {
            var unknownObjects = GameObject.FindGameObjectsWithTag("Unknown");
            if (unknownObjects.Length > 0)
            {
                Selection.objects = unknownObjects;
                return true;
            }

            return false;
        }
        #endif

        public void OpenReportWithName(string name) => OpenFile(CSVCreator.BuildFilePath(name));

        public void OpenFile(string path)
        {
            try
            {
                _process = Process.Start(path);
                _process.Start();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning("Произошла ошибка при открытии файла. Закройте все файла этого типа и попробуйте еще раз.");
                UnityEngine.Debug.LogWarning(ex.Message);

                if (Application.isEditor)
                    Logs.Logger.Error("Ошибка при открытии файла", ex);
            }
        }

        public void SaveShowCaseReportForBuild()
        {
            var creator = PrepareCreatorToShowCaseReport(false);
            creator.SaveFile();
        }

        public void SaveObjectsReportForBuild()
        {
            var creator = PrepareCreatorToObjectsReport(false);
            creator.SaveFile();
        }

        public void CreateShowCaseReport()
        {
            var creator = PrepareCreatorToShowCaseReport();

            creator.SaveFile();
            OpenFile(creator.GetFullPath());

            Logs.Logger.Info("Сформирован отчет о витринах");
        }

        public void CreateObjectsReport()
        {
            var creator = PrepareCreatorToObjectsReport();

            creator.SaveFile();
            OpenFile(creator.GetFullPath());

            Logs.Logger.Info("Сформирован отчет об объектах");
        }

        private CSVCreator PrepareCreatorToObjectsReport(bool uniq = true)
        {
            var creator = new CSVCreator(defaultObjectsReportName, uniq);
            creator.AddObjects(ObjectTypes.Furniture);
            creator.AddObjects(ObjectTypes.Showcase);
            creator.AddObjects(ObjectTypes.Iron);

            return creator;
        }

        private CSVCreator PrepareCreatorToShowCaseReport(bool uniq = true)
        {
            var creator = new CSVCreator(defaultShowcasesReportName, uniq);
            creator.AddShowCases(GameObject.FindGameObjectsWithTag(ObjectTypes.Showcase.ToString()));

            return creator;
        }

        #if UNITY_EDITOR
        private void ActivateUnknown() => Selection.objects = GameObject.FindGameObjectsWithTag("Unknown");
        #endif
    }
}
