using UnityEditor;
using UnityEngine;

using Assets.Develop.Scripts.Components;

public class ProductTypeWindow : EditorWindow
{
    private ProductTypeComponent _target = new ProductTypeComponent();
    private string _errorMessage;

    public static void ShowWindow()
    {
        var window = GetWindowWithRect<ProductTypeWindow>(new Rect(300, 500, 400, 100));
        window.titleContent = new GUIContent("Категория");
        window.Show();
    }

    void OnGUI()
    {
        _target.Name = EditorGUILayout.TextField("Название", _target.Name);

        if (GUILayout.Button("Сохранить категорию"))
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
