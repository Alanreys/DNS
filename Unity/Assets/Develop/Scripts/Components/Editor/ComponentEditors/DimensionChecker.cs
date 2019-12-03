using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DimensionChecker : MonoBehaviour
{
    public float surroundingsCheckRadius = 10f;
    public bool drawDistanceToNearestObjects = false;
    public bool drawDistanceLabel = false;
    public Color debugInfoColor = Color.red;
    public Vector3 size = new Vector3(0, 0, 0);

    public void DisplayDistanceToSurroundings()
    {
        Gizmos.color = debugInfoColor;
        foreach (var col in Physics.OverlapSphere(transform.position, surroundingsCheckRadius))
        {
            Gizmos.DrawLine(transform.position, col.transform.position);
            if (drawDistanceLabel)
            {
                DrawColoredLabel(col.transform.position, Vector3.Distance(transform.position, col.transform.position).ToString());
            }
        }
    }

    public void CalcByMesh()
    {
        var mesh = this.GetComponent<MeshCollider>();
        if (mesh != null)
            size = mesh.bounds.size;
    }

    private void OnDrawGizmosSelected()
    {
        if (drawDistanceToNearestObjects)
        {
            DisplayDistanceToSurroundings();
        }
    }

    private void DrawColoredLabel(Vector3 pos, string text)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = debugInfoColor;

        #if UNITY_EDITOR
        Handles.Label(pos, text, style);
        #endif
    }
}