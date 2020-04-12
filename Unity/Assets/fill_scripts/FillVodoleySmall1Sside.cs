using UnityEngine;

[ExecuteInEditMode]
public class FillVodoleySmall1Sside : MonoBehaviour
{
    public GameObject top_main_item;
    public bool create_top_array;
    public GameObject[] top_shelf;
    GameObject[] top_shelf_obj;
    public bool fill_top;
    [Range(0.0f, 0.2f)]
    public float depth_top = 0;

    public GameObject bottom_main_item;
    public bool create_bottom_array;
    public GameObject[] bottom_shelf;
    GameObject[] bottom_shelf_obj;
    public bool fill_bottom;
    [Range(-0.1f, 0.2f)]
    public float depth_bottom = -0.1f;

    public bool clear_items;

    private float left_edge = 0.47f;
    private float right_edge = -0.53f;

    private void Update()
    {
        if (fill_top)
        {
            filling_top();
        }
        if (fill_bottom)
        {
            filling_bottom();
        }
        if (create_top_array)
        {
            create_top();
        }
        if (create_bottom_array)
        {
            create_bottom();
        }
        if (clear_items)
        {
            clearing_items();
        }
    }

    private void filling_top()
    {
        fill_top = false;
        float width = left_edge - right_edge;
        float step = width / (top_shelf.Length + 1);
        float center = step;

        top_shelf_obj = new GameObject[top_shelf.Length];
        for (int i = 0; i < top_shelf.Length; i++)
        {
            if (top_shelf[i] != null)
            {
                top_shelf_obj[i] = Instantiate(top_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                top_shelf_obj[i].transform.parent = this.gameObject.transform;
                top_shelf_obj[i].transform.localEulerAngles = new Vector3(0, -107, -90);
                top_shelf_obj[i].transform.localPosition = new Vector3(depth_top, left_edge - center, (0.044f * depth_top + 0.0479f) / 0.145f);
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

        bottom_shelf_obj = new GameObject[bottom_shelf.Length];
        for (int i = 0; i < bottom_shelf.Length; i++)
        {
            if (bottom_shelf[i] != null)
            {
                bottom_shelf_obj[i] = Instantiate(bottom_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                bottom_shelf_obj[i].transform.parent = this.gameObject.transform;
                bottom_shelf_obj[i].transform.localEulerAngles = new Vector3(0, -107, -90);
                bottom_shelf_obj[i].transform.localPosition = new Vector3(depth_bottom, left_edge - center, (0.0978f * depth_bottom - 0.0224f) / 0.3186f);
            }
            center += step;
        }
    }

    private void create_top()
    {
        create_top_array = false;
        for (int i = 0; i < top_shelf.Length; i++)
        {
            top_shelf[i] = top_main_item;
        }
    }
    private void create_bottom()
    {
        create_bottom_array = false;
        for (int i = 0; i < bottom_shelf.Length; i++)
        {
            bottom_shelf[i] = bottom_main_item;
        }
    }

    private void clearing_items()
    {
        clear_items = false;
        for (int i = this.transform.childCount - 1; i > 4; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}