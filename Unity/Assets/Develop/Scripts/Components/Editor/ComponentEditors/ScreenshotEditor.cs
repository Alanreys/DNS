using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Screenshot))]
public class ScreenshotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Screenshot screenshot = (Screenshot)target;
        if (GUILayout.Button("Открыть папку со скриншотами"))
        {
            screenshot.OpenDialog();
        }
    }
}
