using UnityEngine;
using UnityEditor;
using Assets.Develop.Scripts.Components;

[CustomEditor(typeof(ObjectSpawnerComponent))]

public class ObjectSpawnerEditor : Editor
{
    private ObjectSpawnerComponent _target;
    private string _errorMessage;

    public override void OnInspectorGUI()
    {
        _target = target as ObjectSpawnerComponent;

        _target.LinkedObject = (GameObject)EditorGUILayout.ObjectField("Объект для выставления:", _target.LinkedObject, typeof(GameObject), false);


        if (GUILayout.Button("Выставить объекты"))
        {
            _target.SpawnObjectClick();
        }

        if (_errorMessage != null)
            EditorGUILayout.LabelField($"Ошибка. {_errorMessage}");
    }
}
