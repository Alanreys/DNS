using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_hierarchy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Showcase")
        {
            this.gameObject.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Showcase")
        {
            this.gameObject.transform.parent = null;
        }
    }
}
