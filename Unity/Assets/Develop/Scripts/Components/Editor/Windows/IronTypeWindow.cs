using UnityEditor;
using UnityEngine;

using Assets.Develop.Scripts.Components;

public class IronTypeWindow : EditorWindow
{
    private IronTypeComponent _target = new IronTypeComponent();
    private string _errorMessage;

    public static void ShowWindow()
    {
        var window = GetWindowWithRect<IronTypeWindow>(new Rect(300, 500, 400, 100));
        window.titleContent = new GUIContent("Железо");
        window.Show();
    }

    void OnGUI()
    {
        _target.Name = EditorGUILayout.TextField("Название", _target.Name);

        if (GUILayout.Button("Сохранить тип железа"))
        {
            var validate = _target.Validate();
            if (validate.Success)
            {
                _target.SaveTypeClick();
                Close();
            }
            else
                _errorMessage = validate.ErrorMessage;
        }

        if (_errorMessage != null)
            EditorGUILayout.LabelField($"Ошибка. {_errorMessage}");
    }
}
