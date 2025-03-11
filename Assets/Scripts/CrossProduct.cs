using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrossProduct : MonoBehaviour
{
    [Range(0.5f,2.0f)][SerializeField] float visionHeight =1.5f;
    [Range(0.5f, 2.0f)][SerializeField] float vectorLength = 3f;
    [SerializeField] GameObject Car;


    private void OnDrawGizmos()
    {
        //Origin is the gameobject a+ 50 cm up (in local coordinate scale)
        Vector3 orig = transform.position + transform.up * visionHeight;
        Vector3 dir = transform.forward;
        RaycastHit hit;

        //Darw the vision vector
        MyDraw.DrawVectorAt(orig, dir, Color.blue, 2);

        if (Physics.Raycast(orig, dir, out hit))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.1f);

            //draw normal vector
            MyDraw.DrawVectorAt(hit.point, vectorLength * hit.normal, Color.green);

            //compute the right vector using cross-product
            Vector3 vecRigth = Vector3.Cross(hit.normal, dir);
            //draw the right vector
            MyDraw.DrawVectorAt(hit.point,vectorLength * vecRigth, Color.red);
            //compute and draw the forward vector
            Vector3 vecForward = Vector3.Cross(vecRigth, hit.normal);
            MyDraw.DrawVectorAt(hit.point, vectorLength * vecForward, Color.blue);

            if (Car)
            {
                //set position
                Car.transform.position = hit.point;

                //set rotation
                Car.transform.rotation = Quaternion.LookRotation(vecForward, hit.normal);
        }
        }
       
    }
    
}
