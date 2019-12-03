#if UNITY_EDITOR
using Assets.Develop.Scripts.Controllers;
using System.IO;
using Assets.Develop.Scripts.Utility;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Assets.Develop.Scripts
{
    public class BuildConfig : IPreprocessBuildWithReport
    {
        int IOrderedCallback.callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var appControler = ControllersFactory.AppController();

            appControler.SaveShowCaseReportForBuild();
            appControler.SaveObjectsReportForBuild();

            string sourceFolder = Path.Combine("", $"{CSVCreator.directoryName}");
            string targetFolder = Path.Combine(Path.GetDirectoryName(report.summary.outputPath), $"{CSVCreator.directoryName}");

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);
            else
            {
                Directory.Delete(targetFolder, true);
                Directory.CreateDirectory(targetFolder);
            }

            File.Copy($"{sourceFolder}/{appControler.defaultShowcasesReportName}{CSVCreator.format}",
                $"{targetFolder}/{appControler.defaultShowcasesReportName}{CSVCreator.format}");

            File.Copy($"{sourceFolder}/{appControler.defaultObjectsReportName}{CSVCreator.format}",
                $"{targetFolder}/{appControler.defaultObjectsReportName}{CSVCreator.format}");
        }
    }
}
#endif