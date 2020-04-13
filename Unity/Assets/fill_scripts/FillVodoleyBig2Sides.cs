using UnityEngine;
[ExecuteInEditMode]
public class FillVodoleyBig2Sides : MonoBehaviour
{
    public GameObject level0_main_item;
    public bool create_level0_array;
    public GameObject[] level0_shelf;
    GameObject[] level0_shelf_obj;
    public bool fill_level0;
    [Range(-0.37f, -0.03f)]
    public float depth_level0 = -0.25f;

    public GameObject level1_main_item;
    public bool create_level1_array;
    public GameObject[] level1_shelf;
    GameObject[] level1_shelf_obj;
    public bool fill_level1;
    [Range(-0.37f, -0.04f)]
    public float depth_level1 = -0.215f;

    public GameObject level2_main_item;
    public bool create_level2_array;
    public GameObject[] level2_shelf;
    GameObject[] level2_shelf_obj;
    public bool fill_level2;
    [Range(-0.27f, -0.04f)]
    public float depth_level2 = -0.16f;

    public GameObject level3_main_item;
    public bool create_level3_array;
    public GameObject[] level3_shelf;
    GameObject[] level3_shelf_obj;
    public bool fill_level3;
    [Range(-0.27f, -0.02f)]
    public float depth_level3 = -0.18f;

    public GameObject level4_main_item;
    public bool create_level4_array;
    public GameObject[] level4_shelf;
    GameObject[] level4_shelf_obj;
    public bool fill_level4;
    [Range(0.3f, 0.365f)]
    public float depth_level4 = 0.345f;

    public GameObject level5_main_item;
    public bool create_level5_array;
    public GameObject[] level5_shelf;
    GameObject[] level5_shelf_obj;
    public bool fill_level5;
    [Range(0.17f, 0.23f)]
    public float depth_level5 = 0.2f;

    public GameObject level6_main_item;
    public bool create_level6_array;
    public GameObject[] level6_shelf;
    GameObject[] level6_shelf_obj;
    public bool fill_level6;
    [Range(0.02f, 0.1f)]
    public float depth_level6 = 0.066f;

    public GameObject level7_main_item;
    public bool create_level7_array;
    public GameObject[] level7_shelf;
    GameObject[] level7_shelf_obj;
    public bool fill_level7;
    [Range(0.02f, 0.135f)]
    public float depth_level7 = 0.07f;

    public bool clear_items;
    float left_edge = 0.5f;
    float right_edge = -0.5f;

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
        if (fill_level4)
        {
            filling_level4();
        }
        if (fill_level5)
        {
            filling_level5();
        }
        if (fill_level6)
        {
            filling_level6();
        }
        if (fill_level7)
        {
            filling_level7();
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
        if (create_level4_array)
        {
            create_level4();
        }
        if (create_level5_array)
        {
            create_level5();
        }
        if (create_level6_array)
        {
            create_level6();
        }
        if (create_level7_array)
        {
            create_level7();
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
                level0_shelf_obj[i].transform.localEulerAngles = new Vector3(180, 73.45f, 90);
                level0_shelf_obj[i].transform.localPosition = new Vector3(depth_level0, left_edge - center, (0.093f * depth_level0 - 0.1341f) / 0.315f);
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
                level1_shelf_obj[i].transform.localEulerAngles = new Vector3(0, -90, -90);
                level1_shelf_obj[i].transform.localPosition = new Vector3(depth_level1, left_edge - center, -0.105f);
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
                level2_shelf_obj[i].transform.localEulerAngles = new Vector3(180, 73.45f, 90);
                level2_shelf_obj[i].transform.localPosition = new Vector3(depth_level2, left_edge - center, (0.0606f * depth_level2 + 0.0687f) / 0.2038f);
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
                level3_shelf_obj[i].transform.localEulerAngles = new Vector3(0, -90, -90);
                level3_shelf_obj[i].transform.localPosition = new Vector3(depth_level3, left_edge - center, 0.665f);
            }
            center += step;
        }
    }

    private void filling_level4()
    {
        fill_level4 = false;
        float width = left_edge - right_edge;
        float step = width / (level4_shelf.Length + 1);
        float center = step;

        level4_shelf_obj = new GameObject[level4_shelf.Length];
        for (int i = 0; i < level4_shelf.Length; i++)
        {
            if (level4_shelf[i] != null)
            {
                level4_shelf_obj[i] = Instantiate(level4_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level4_shelf_obj[i].transform.parent = this.gameObject.transform;
                level4_shelf_obj[i].transform.localEulerAngles = new Vector3(180, -90, -90);
                level4_shelf_obj[i].transform.localPosition = new Vector3(depth_level4, left_edge - center, -0.32f);
            }
            center += step;
        }
    }

    private void filling_level5()
    {
        fill_level5 = false;
        float width = left_edge - right_edge;
        float step = width / (level5_shelf.Length + 1);
        float center = step;

        level5_shelf_obj = new GameObject[level5_shelf.Length];
        for (int i = 0; i < level5_shelf.Length; i++)
        {
            if (level5_shelf[i] != null)
            {
                level5_shelf_obj[i] = Instantiate(level5_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level5_shelf_obj[i].transform.parent = this.gameObject.transform;
                level5_shelf_obj[i].transform.localEulerAngles = new Vector3(180, -90, -90);
                level5_shelf_obj[i].transform.localPosition = new Vector3(depth_level5, left_edge - center, -0.18f);
            }
            center += step;
        }
    }

    private void filling_level6()
    {
        fill_level6 = false;
        float width = left_edge - right_edge;
        float step = width / (level6_shelf.Length + 1);
        float center = step;

        level6_shelf_obj = new GameObject[level6_shelf.Length];
        for (int i = 0; i < level6_shelf.Length; i++)
        {
            if (level6_shelf[i] != null)
            {
                level6_shelf_obj[i] = Instantiate(level6_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level6_shelf_obj[i].transform.parent = this.gameObject.transform;
                level6_shelf_obj[i].transform.localEulerAngles = new Vector3(180, -90, -90);
                level6_shelf_obj[i].transform.localPosition = new Vector3(depth_level6, left_edge - center, -0.027f);
            }
            center += step;
        }
    }

    private void filling_level7()
    {
        fill_level7 = false;
        float width = left_edge - right_edge;
        float step = width / (level7_shelf.Length + 1);
        float center = step;

        level7_shelf_obj = new GameObject[level7_shelf.Length];
        for (int i = 0; i < level7_shelf.Length; i++)
        {
            if (level7_shelf[i] != null)
            {
                level7_shelf_obj[i] = Instantiate(level7_shelf[i], new Vector3(0, 0, 0), Quaternion.identity);
                level7_shelf_obj[i].transform.parent = this.gameObject.transform;
                level7_shelf_obj[i].transform.localEulerAngles = new Vector3(180, -90, -90);
                level7_shelf_obj[i].transform.localPosition = new Vector3(depth_level7, left_edge - center, 0.31f);
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
    private void create_level4()
    {
        create_level4_array = false;
        for (int i = 0; i < level4_shelf.Length; i++)
        {
            level4_shelf[i] = level4_main_item;
        }
    }
    private void create_level5()
    {
        create_level5_array = false;
        for (int i = 0; i < level5_shelf.Length; i++)
        {
            level5_shelf[i] = level5_main_item;
        }
    }
    private void create_level6()
    {
        create_level6_array = false;
        for (int i = 0; i < level6_shelf.Length; i++)
        {
            level6_shelf[i] = level6_main_item;
        }
    }
    private void create_level7()
    {
        create_level7_array = false;
        for (int i = 0; i < level7_shelf.Length; i++)
        {
            level7_shelf[i] = level7_main_item;
        }
    }
    private void clearing_items()
    {
        clear_items = false;
        for (int i = this.transform.childCount - 1; i > 11; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
