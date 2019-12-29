using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPersonController : MonoBehaviour
{
    Camera game_camera;
    GameObject product;
    Rigidbody rb;
    Transform tr;

    bool take = false;

    public int speed_move_good;

    float step_yz, step_x;
    float good_yz_pos, good_x_pos, good_rot_x;

    void Start()
    {
        game_camera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
        speed_move_good = 100;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && take)
        {
            rb.velocity = new Vector3(0, 0, 0);

            if (Input.GetMouseButtonDown(2)) tr.localRotation = Quaternion.Euler(0, 0, 0);

            if (Input.GetAxis("Mouse ScrollWheel") > 0) tr.eulerAngles += new Vector3(90, 0, 0);
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) tr.eulerAngles -= new Vector3(90, 0, 0);

            good_yz_pos = Input.GetAxis("Mouse Y");      
            step_yz = good_yz_pos / speed_move_good;
            
            if (Input.GetMouseButton(1))
            {
                good_rot_x = Input.GetAxis("Mouse X");
                
                tr.localPosition += new Vector3(0, 0, step_yz);
                tr.eulerAngles += new Vector3(0, good_rot_x, 0);
            }
            else
            {
                good_x_pos = Input.GetAxis("Mouse X");
                step_x = good_x_pos / speed_move_good;

                tr.localPosition += new Vector3(step_x, 0, 0);
                tr.position += new Vector3(0, step_yz, 0);
            }
        }     

        if (Input.GetMouseButtonDown(0) && !take)
        {
            RaycastHit hit;
            Ray ray = game_camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider.gameObject.tag == "Product")
                {
                    Take_product(hit.collider.gameObject);
                }                               
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            rb.useGravity = true;
            rb.freezeRotation = false;
            take = false;
            product.transform.parent = null;
        }
    }

    void Take_product(GameObject some_product)  
    {
        product = some_product;
        product.transform.parent = this.gameObject.transform;
        rb = some_product.GetComponent<Rigidbody>();
        tr = some_product.GetComponent<Transform>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        take = true;
    }
}
