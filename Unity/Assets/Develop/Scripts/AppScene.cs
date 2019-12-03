using UnityEngine;
using UnityEditor;

using Assets.Develop.Scripts.Controllers;
using Assets.Develop.Scripts.Database;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

internal class AppScene : MonoBehaviour
{
    static AppScene()
    {
        var dbManager = new DBManager();
        dbManager.CheckTables();

        CheckFirstPerson();

        #if UNITY_EDITOR
        EditorApplication.hierarchyChanged += OnHierarchyChange;
        EditorApplication.update += Update;
        #endif
    }

    private static void OnHierarchyChange()
    {
        var objectController = ControllersFactory.ObjectController();

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.tag == "Untagged")
                objectController.HandleAddedObject(obj);
            else
                objectController.FindRenamed(obj);
        }
    }

    private static void CheckFirstPerson()
    {
        var fpc = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        if (fpc != null && fpc.gameObject != null)
        {
            fpc.gameObject.tag = "Player";

            if (fpc.gameObject.GetComponent<Screenshot>() == null)
                fpc.gameObject.AddComponent<Screenshot>();

            if (fpc.gameObject.GetComponent<EscapeMenu>() == null)
                fpc.gameObject.AddComponent<EscapeMenu>();
        }
    }

    private static void Update() {}
}