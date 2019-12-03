using UnityEngine;
using UnityEditor;
using Assets.Develop.Scripts.Components;

[CustomEditor(typeof(UnknownObjectComponent))]
public class UnknownObjectFormEditor : Editor
{
    private UnknownObjectComponent _target;
    private string _errorMessage;

    public override void OnInspectorGUI()
    {
        _target = target as UnknownObjectComponent;

        var selectedType = EditorGUILayout.Popup("Тип объекта", _target.Type, _target.TypeDropDownDataSourse);
        if (selectedType != _target.Type)
        {
            _target.Type = selectedType;
            _target.DropDownChanged();
        }

        if (_target.IronType != null)
            _target.IronType = EditorGUILayout.Popup("Тип железа", _target.IronType ?? -1, _target.IronTypeDropDownDataSourse);

        if (_target.ProductType != null)
            _target.ProductType = EditorGUILayout.Popup("Категория товара", _target.ProductType ?? -1, _target.ProductTypeDropDownDataSourse);

        _target.Name = EditorGUILayout.TextField("Имя", _target.Name);
        _target.Description = EditorGUILayout.TextArea(_target.Description, GUILayout.Height(60));
        if (GUILayout.Button("Сохранить объект"))
        {
            var validate = _target.Validate();
            if (validate.Success) 
                _target.SaveDataClick();
            else
                _errorMessage = validate.ErrorMessage;
        }

        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField($"Ширина: {_target.Size.x}см");
        EditorGUILayout.LabelField($"Высота: {_target.Size.y}см");
        EditorGUILayout.LabelField($"Длина: {_target.Size.z}см");

        if (_errorMessage != null)
            EditorGUILayout.LabelField($"Ошибка. {_errorMessage}");
    }
}

