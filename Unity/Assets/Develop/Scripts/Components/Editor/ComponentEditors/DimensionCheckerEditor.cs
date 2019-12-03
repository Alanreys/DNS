using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DimensionChecker))]
public class DimensionCheckerEditor : Editor
{
    private DimensionChecker _target;

    public override void OnInspectorGUI()
    {
        _target = target as DimensionChecker;
        _target.debugInfoColor = EditorGUILayout.ColorField("Debug color", _target.debugInfoColor);
        _target.drawDistanceToNearestObjects =
            EditorGUILayout.ToggleLeft("Draw distances", _target.drawDistanceToNearestObjects);
        _target.size = EditorGUILayout.Vector3Field("Size", _target.size);
        _target.CalcByMesh();

        if (_target.drawDistanceToNearestObjects)
        {
            _target.surroundingsCheckRadius =
                EditorGUILayout.FloatField("Distance check radius", _target.surroundingsCheckRadius);
            _target.drawDistanceLabel =
                EditorGUILayout.Toggle("Draw distance text", _target.drawDistanceLabel);
        }
    }
}