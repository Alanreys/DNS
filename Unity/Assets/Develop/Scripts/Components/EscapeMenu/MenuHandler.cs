using Assets.Develop.Scripts.Controllers;
using UnityEngine;

public class MenuHandler : MonoBehaviour {

    private void Start() {}
	private void Update () {}

    private ScreenshootController _screenshootController = ControllersFactory.ScreenshootController();
    private AppController _appController = ControllersFactory.AppController();

    public void Exit() => Application.Quit();

    public void OpenScreenshotFolder() => _screenshootController.OpenFolder();

    public void CreateShowCaseReport() {
        if (Application.isEditor)
            _appController.CreateShowCaseReport();
        else
            _appController.OpenReportWithName(_appController.defaultShowcasesReportName);
    }       

    public void CreateObjectsReport() {
        if (Application.isEditor)
            _appController.CreateObjectsReport();
        else
            _appController.OpenReportWithName(_appController.defaultObjectsReportName);
    }
}
