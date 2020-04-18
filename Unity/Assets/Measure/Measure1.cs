using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Measure1 : MonoBehaviour
{
    [Header("Arrows")]
    public GameObject arrowL;
    public GameObject arrowR;
    [Range(-0.15f,0.15f)]
    public float arrowScale = 0.15f;
    [Range(0,90)]
    public float arrowAngle = 0;
    public Color arrowColor;
    [Header("Text")]
    public Color textColor;
    public Text textField;
    [Range(0, 0.03f)]
    public float textScale = 0.02f;
    [Header("Canvas")]
    float distance;
    public GameObject canvas;

    private void OnDrawGizmos()
    {
        MeasureStuff();
    }

    private void OnValidate()
    {
        MeasureStuff();
    }

    void MeasureStuff()
    {
        distance = Vector3.Distance(arrowL.transform.position, arrowR.transform.position);
        textField.text = distance.ToString("N2") + "m";

        canvas.transform.position = LerpByDistance(arrowL.transform.position, arrowR.transform.position, 0.5f);

        if (arrowL != null)
        {
            arrowL.GetComponent<SpriteRenderer>().color = arrowColor;
            arrowL.transform.localScale = new Vector3(arrowScale, arrowScale, arrowScale);
            arrowL.transform.localRotation = Quaternion.Euler(arrowAngle, 0, 0);
        }
        if (arrowR != null)
        {
            arrowR.GetComponent<SpriteRenderer>().color = arrowColor;
            arrowR.transform.localScale = new Vector3(arrowScale, arrowScale, arrowScale);
            arrowR.transform.localRotation = Quaternion.Euler(arrowAngle, 0, 0);
        }

        if(textField != null)
        {
            textField.color = textColor;
            textField.transform.localScale = new Vector3(textScale, textScale, 0);

        }
    }

    Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = A + x * (B - A);
        return P;
    }

}
