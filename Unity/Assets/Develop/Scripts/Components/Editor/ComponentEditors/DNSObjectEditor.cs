using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using Assets.Develop.Scripts.Components;
using Assets.Develop.Scripts.Types;

[CustomEditor(typeof(DNSObjectComponent))]
public class KnownObjectFormEditor : Editor
{
    private DNSObjectComponent _target;
    private string _errorMessage;
    private bool isEditMode = false;

    public override void OnInspectorGUI()
    {
        _target = target as DNSObjectComponent;

        if (_target.Entity == null && _target.RefreshData().Failure)
            return;

        if (isEditMode)
        {
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

            if (GUILayout.Button("Отмена"))
                isEditMode = false;

            if (GUILayout.Button("Сохранить изменения"))
            {
                var validate = _target.Validate();
                if (validate.Success)
                {
                    isEditMode = false;
                    _target.UpdateDataClick();
                }
                else
                    _errorMessage = validate.ErrorMessage;
            }

            if (_errorMessage != null)
                EditorGUILayout.LabelField($"Ошибка. {_errorMessage}");
        }
        else
        {
            EditorGUILayout.LabelField($"Тип объекта: {_target.Entity.Type.GetCategory()}");
            EditorGUILayout.LabelField($"Имя: {_target.Entity.Name}");

            if (_target.Entity.IronType != null)
                EditorGUILayout.LabelField($"Тип железа: {_target.GetIronTypeNameById(_target.Entity.IronType)}");

            if (_target.Entity.ProductType != null)
                EditorGUILayout.LabelField($"Категория товара: {_target.GetProductTypeNameById(_target.Entity.ProductType)}");

            if (_target.Entity.Description != null && _target.Entity.Description != "")
                EditorGUILayout.LabelField($"Описание: {_target.Entity.Description}");

            if (GUILayout.Button("Изменить данные"))
            {
                _target.EnableEditMode();
                isEditMode = true;
            }

            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField($"Ширина: {_target.Size.x}см");
            EditorGUILayout.LabelField($"Высота: {_target.Size.y}см");
            EditorGUILayout.LabelField($"Длина: {_target.Size.z}см");
        }
    }
}

