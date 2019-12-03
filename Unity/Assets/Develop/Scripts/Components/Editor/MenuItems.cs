using UnityEditor;
using UnityEngine;
using Assets.Develop.Scripts.Controllers;

public class MenuItems : MonoBehaviour
{
    private const string rootItemName = "Дополнительные возможности/";

    #region Отчеты
    private const string reportsSubitem = "Отчеты/";
    [MenuItem(rootItemName + reportsSubitem + "/О витринах")]
    public static void ShowcaseReport()
    {
        AppController appController = ControllersFactory.AppController();

        if (appController.HasUnknownObjects())
            if (!EditorUtility.DisplayDialog(
                    "Информация о некоторых объектах не заполнена",
                    "Незаполненные объекты будут подсвечены. Вы можете продолжить построение отчета или заполнить всю информацию.",
                    "Продолжить",
                    "Отмена"))
            {
                return;
            }

        appController.CreateShowCaseReport();
    }

    [MenuItem(rootItemName + reportsSubitem + "/Об объектах")]
    public static void ObjectsReport()
    {
        AppController appController = ControllersFactory.AppController();

        if (appController.HasUnknownObjects())
            if (!EditorUtility.DisplayDialog(
                    "Информация о некоторых объектах не заполнена",
                    "Незаполненные объекты будут подсвечены. Вы можете продолжить построение отчета или заполнить всю информацию.",
                    "Продолжить",
                    "Отмена"))
            {
                return;
            }

        appController.CreateObjectsReport();
    }
    #endregion

    #region Подтипы
    private const string subtypesManage = "Управление типами/";

    [MenuItem(rootItemName + subtypesManage + "/Добавить тип товара")]
    public static void AddProductType()
    {
        ProductTypeWindow.ShowWindow();
    }

    [MenuItem(rootItemName + subtypesManage + "/Добавить тип железа")]
    public static void AddIronType()
    {
        IronTypeWindow.ShowWindow();
    }
    #endregion
}