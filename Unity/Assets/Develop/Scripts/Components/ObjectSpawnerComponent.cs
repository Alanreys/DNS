using UnityEngine;
using UnityEditor;


namespace Assets.Develop.Scripts.Components
{
    public class ObjectSpawnerComponent : MonoBehaviour
    {
        public GameObject LinkedObject;

        public void SpawnObjectClick()
        {
            var epsilon = 0.01f;

            GameObject linkedObjectInstanse = Instantiate(LinkedObject);

            var linkedObjectRenderer = linkedObjectInstanse.GetComponent<Renderer>().bounds;
            var parentBounds = GetComponent<Renderer>().bounds;

            linkedObjectInstanse.transform.SetParent(transform);
            linkedObjectInstanse.transform.localPosition = Vector3.zero;

            var parentXAngle = +(transform.eulerAngles.x);
            if (parentXAngle > 94)
                parentXAngle = 360 - parentXAngle;

            var xObjAngle = +(linkedObjectInstanse.transform.localEulerAngles.x);
            if (xObjAngle > 94)
                xObjAngle = 360 - xObjAngle;

            var offset = (parentXAngle > 42 && parentXAngle < 94) ? (parentBounds.size.y) : (parentBounds.size.y / 2);
            var offset1 = (xObjAngle > 42 && xObjAngle < 94) ? (linkedObjectRenderer.size.y / 2) : 0;

            var y = offset1 + offset;

            ///  X
            var minMargin = 0.03f;

            var x = (-parentBounds.size.x / 2) + (linkedObjectRenderer.size.x / 2);
            var ratio = parentBounds.size.x / linkedObjectRenderer.size.x;

            var objectCount = Mathf.FloorToInt(ratio);
            var remind = ratio - objectCount;

            var margin = 0.0f;
            objectCount++;
            while (margin < minMargin && objectCount > 0)
            {
                objectCount--;
                remind = ratio - objectCount;
                margin = (remind * linkedObjectRenderer.size.x) / (objectCount + 1);
            }

            x += margin;
            linkedObjectInstanse.transform.localPosition = new Vector3(x, 0, y);
            x += linkedObjectRenderer.size.x;

           for (int i = 0; i < objectCount - 1; i++)
           {
                GameObject obj = Instantiate(LinkedObject);
                obj.transform.SetParent(transform);

                x += margin;
                obj.transform.localPosition = new Vector3(x, 0, y);
                x += linkedObjectRenderer.size.x;
           }
        }
    }
}
