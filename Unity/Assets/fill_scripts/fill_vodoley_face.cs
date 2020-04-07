using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class fill_vodoley_face : MonoBehaviour
{ 
    public GameObject[] top_shelf;
    public bool fill_top;
    public GameObject[] bottom_shelf;
    public bool fill_bottom;

    private float left_edge = 0.3f;
    private float right_edge = -0.3f;

    private void Update()
    {
        if (fill_top)
        {
            filling_top();
        }
        if(fill_bottom)
        {
            filling_bottom();
        }
    }

    private void filling_top()
    {
        fill_top = false;
        float width = left_edge - right_edge;
        float step = width / (top_shelf.Length+1);
        float center = step;

        GameObject[] top_shelf_obj = new GameObject[top_shelf.Length];
        for (int i = 0; i < top_shelf.Length; i++)
        {
            if (top_shelf[i] != null)
            {
                top_shelf_obj[i] = Instantiate(top_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                top_shelf_obj[i].transform.parent = this.gameObject.transform;
                top_shelf_obj[i].transform.localEulerAngles = new Vector3(0, 90, 90);
                top_shelf_obj[i].transform.localPosition = new Vector3(0, left_edge - center, 0.275f);      
            }
            center += step;
        }    
    }
    private void filling_bottom()
    {
        fill_bottom = false;
        float width = left_edge - right_edge;
        float step = width / (bottom_shelf.Length + 1);
        float center = step;

        GameObject[] bottom_shelf_obj = new GameObject[bottom_shelf.Length];
        for (int i = 0; i < bottom_shelf.Length; i++)
        {
            if (bottom_shelf[i] != null)
            {
                bottom_shelf_obj[i] = Instantiate(bottom_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                bottom_shelf_obj[i].transform.parent = this.gameObject.transform;
                bottom_shelf_obj[i].transform.localEulerAngles = new Vector3(0, 90, 90);
                bottom_shelf_obj[i].transform.localPosition = new Vector3(0, left_edge - center, -0.16f);
            }
            center += step;
        }
    }
}
