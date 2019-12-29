using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsHierarchy : MonoBehaviour
{
    bool HaveChild = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>().useGravity)
        {
            if (collision.gameObject.tag == "Product")
            {
                collision.transform.parent = this.gameObject.transform;
            }
        }
    }
}