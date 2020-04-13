using UnityEngine;

[ExecuteInEditMode]
public class FillshkafSmall : MonoBehaviour
{
    public GameObject level0_main_item;
    public bool create_level0_array;
    public GameObject[] level0_shelf;
    GameObject[] level0_shelf_obj;
    public bool fill_level0;
    [Range(-0.05f, 0.10f)]
    public float depth_level0 = 0.03f;

    public GameObject level1_main_item;
    public bool create_level1_array;
    public GameObject[] level1_shelf;
    GameObject[] level1_shelf_obj;
    public bool fill_level1;
    [Range(-0.05f, 0.10f)]
    public float depth_level1 = 0.03f;

    public GameObject level2_main_item;
    public bool create_level2_array;
    public GameObject[] level2_shelf;
    GameObject[] level2_shelf_obj;
    public bool fill_level2;
    [Range(-0.05f, 0.10f)]
    public float depth_level2 = 0.03f;

    public GameObject level3_main_item;
    public bool create_level3_array;
    public GameObject[] level3_shelf;
    GameObject[] level3_shelf_obj;
    public bool fill_level3;
    [Range(-0.05f, 0.10f)]
    public float depth_level3 = 0.03f;

    public bool clear_items;
    float left_edge = 0.53f;
    float right_edge = -0.47f;

    private void Update()
    {
        if (fill_level0)
        {
            filling_level0();
        }
        if (fill_level1)
        {
            filling_level1();
        }
        if (fill_level2)
        {
            filling_level2();
        }
        if (fill_level3)
        {
            filling_level3();
        }


        if (create_level0_array)
        {
            create_level0();
        }
        if (create_level1_array)
        {
            create_level1();
        }
        if (create_level2_array)
        {
            create_level2();
        }
        if (create_level3_array)
        {
            create_level3();
        }

        if (clear_items)
        {
            clearing_items();
        }
    }

    private void filling_level0()
    {
        fill_level0 = false;
        float width = left_edge - right_edge;
        float step = width / (level0_shelf.Length + 1);
        float center = step;

        level0_shelf_obj = new GameObject[level0_shelf.Length];
        for (int i = 0; i < level0_shelf.Length; i++)
        {
            if (level0_shelf[i] != null)
            {
                level0_shelf_obj[i] = Instantiate(level0_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level0_shelf_obj[i].transform.parent = this.gameObject.transform;
                level0_shelf_obj[i].transform.localEulerAngles = new Vector3(90, 90, 90);
                level0_shelf_obj[i].transform.localPosition = new Vector3(left_edge - center, depth_level0, -0.086f);
            }
            center += step;
        }
    }

    private void filling_level1()
    {
        fill_level1 = false;
        float width = left_edge - right_edge;
        float step = width / (level1_shelf.Length + 1);
        float center = step;

        level1_shelf_obj = new GameObject[level1_shelf.Length];
        for (int i = 0; i < level1_shelf.Length; i++)
        {
            if (level1_shelf[i] != null)
            {
                level1_shelf_obj[i] = Instantiate(level1_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level1_shelf_obj[i].transform.parent = this.gameObject.transform;
                level1_shelf_obj[i].transform.localEulerAngles = new Vector3(90, 90, 90);
                level1_shelf_obj[i].transform.localPosition = new Vector3(left_edge - center, depth_level1, 0.125f);
            }
            center += step;
        }
    }

    private void filling_level2()
    {
        fill_level2 = false;
        float width = left_edge - right_edge;
        float step = width / (level2_shelf.Length + 1);
        float center = step;

        level2_shelf_obj = new GameObject[level2_shelf.Length];
        for (int i = 0; i < level2_shelf.Length; i++)
        {
            if (level2_shelf[i] != null)
            {
                level2_shelf_obj[i] = Instantiate(level2_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level2_shelf_obj[i].transform.parent = this.gameObject.transform;
                level2_shelf_obj[i].transform.localEulerAngles = new Vector3(90, 90, 90);
                level2_shelf_obj[i].transform.localPosition = new Vector3(left_edge - center, depth_level2, 0.35f);
            }
            center += step;
        }
    }

    private void filling_level3()
    {
        fill_level3 = false;
        float width = left_edge - right_edge;
        float step = width / (level3_shelf.Length + 1);
        float center = step;

        level3_shelf_obj = new GameObject[level3_shelf.Length];
        for (int i = 0; i < level3_shelf.Length; i++)
        {
            if (level3_shelf[i] != null)
            {
                level3_shelf_obj[i] = Instantiate(level3_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level3_shelf_obj[i].transform.parent = this.gameObject.transform;
                level3_shelf_obj[i].transform.localEulerAngles = new Vector3(90, 90, 90);
                level3_shelf_obj[i].transform.localPosition = new Vector3(left_edge - center, depth_level3, 0.57f);
            }
            center += step;
        }
    }

    private void create_level0()
    {
        create_level0_array = false;
        for (int i = 0; i < level0_shelf.Length; i++)
        {
            level0_shelf[i] = level0_main_item;
        }
    }

    private void create_level1()
    {
        create_level1_array = false;
        for (int i = 0; i < level1_shelf.Length; i++)
        {
            level1_shelf[i] = level1_main_item;
        }
    }

    private void create_level2()
    {
        create_level2_array = false;
        for (int i = 0; i < level2_shelf.Length; i++)
        {
            level2_shelf[i] = level2_main_item;
        }
    }

    private void create_level3()
    {
        create_level3_array = false;
        for (int i = 0; i < level3_shelf.Length; i++)
        {
            level3_shelf[i] = level3_main_item;
        }
    }

    private void clearing_items()
    {
        clear_items = false;
        for (int i = this.transform.childCount - 1; i > 3; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
