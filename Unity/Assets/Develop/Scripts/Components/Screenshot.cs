using UnityEngine;
using Assets.Develop.Scripts.Controllers;

public class Screenshot : MonoBehaviour {

    public int resolution = 3; // 1 = размер экрана, 2 = увеличенный в 2 раза, и т.д.
    private readonly ScreenshootController _screenshootController = ControllersFactory.ScreenshootController();

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
            _screenshootController.CaptureScreenshot(resolution);
    }

    void OnGUI()
    {
        if (_screenshootController.screenshotSaving)
        {
            var width = 100;
            var height = 50;
            var rect = new Rect((Screen.width / 2 - width / 2), (Screen.height / 2) - (height / 2) - 150, width, height);

            var style = new GUIStyle
            {
                fontSize = 25,
                fontStyle = FontStyle.Normal,
                alignment = TextAnchor.MiddleCenter
            };

            GUI.Label(rect, "Снимок экрана сохранен", style);
        }
    }
    public void OpenDialog()
    {
        _screenshootController.OpenFolder();
    }
}
