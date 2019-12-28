using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsHierarchy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Product")
        {
            collision.transform.parent = this.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Product")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
