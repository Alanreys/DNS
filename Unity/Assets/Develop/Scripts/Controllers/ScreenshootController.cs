using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace Assets.Develop.Scripts.Controllers
{
    public class ScreenshootController
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private const short messageRemoveDelay = 1500;
        private const string screenName = "screen"; // имя картинки
        private const string path = "Screenshots"; // путь для сохранения скриншотов (пустое поле сохраняет в проект)

        public bool screenshotSaving = false;

        public void OpenFolder()
        {
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            Process.Start(path);
        }

        public void CaptureScreenshot(int resolution = 3)
        {
            if (screenshotSaving) {
                screenshotSaving = false;
                _cancellationTokenSource.Cancel();
            }

            _cancellationTokenSource = new CancellationTokenSource();

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            var fileCount = 0;
            var fileName = System.IO.Directory.GetCurrentDirectory() + "\\" + path + "\\" + screenName;

            while (System.IO.File.Exists(fileName + (fileCount > 0 ? $"({fileCount.ToString()})" : string.Empty) + ".png"))
            {
                fileCount++;
            }

            fileName = fileName + (fileCount > 0 ? $"({fileCount.ToString()})" : string.Empty) + ".png";

            ScreenCapture.CaptureScreenshot(fileName, resolution);
            UnityEngine.Debug.Log($"Скриншот сохранен в: {fileName}");

            var token = _cancellationTokenSource.Token;
            Task.Run(async () =>
            {
                // Подождем, пока скрин сохранится
                await Task.Delay(200);
                screenshotSaving = true;


                await Task.Delay(messageRemoveDelay);
                token.ThrowIfCancellationRequested();
                screenshotSaving = false;
            }, token);
        }
    }
}
