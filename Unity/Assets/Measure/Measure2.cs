using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Measure2 : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public bool calculation;

    private void Update()
    {
        if (calculation)
        {
            Distance();
        }
    }

    void Distance()
    {
        calculation = false;
        Debug.Log("Distance is " + Vector3.Distance(obj1.transform.position, obj2.transform.position).ToString() + " m");
    }
}
