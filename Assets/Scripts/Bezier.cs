using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [Range(0f, 1f)] public float t;

    [SerializeField] Transform A;
    [SerializeField] Transform B;
    [SerializeField] Transform C;
    [SerializeField] Transform D;


    private void OnDrawGizmos()
    {
        //we need the vector3 to get the point data and to use them directly
        Vector3 posA = A.position;
        Vector3 posB = B.position;
        Vector3 posC = C.position;
        Vector3 posD = D.position;

        //Draw lines (AB, BC, CD) between the points
        Handles.color = Color.white;
        Handles.DrawLine(posA, posB, 3f);
        Handles.DrawLine(posB, posC, 3f);
        Handles.DrawLine(posC, posD, 3f);

        //first round of interpolations
        Vector3 posX = (1 - t) * posA + t * posB;
        Vector3 posY = (1 - t) * posB + t * posC;
        Vector3 posZ = (1 - t) * posC + t * posD;
        //draw spheres at the 1st interpolated points
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(posX, 0.1f);
        Gizmos.DrawWireSphere(posY, 0.1f);
        Gizmos.DrawWireSphere(posZ, 0.1f);

        //drawlines between
        Handles.DrawLine(posX, posY, 3f);
        Handles.DrawLine(posY, posZ, 3f);

        //second round of interpolation
        Vector3 posU = (1 - t) * posX + t * posY;
        Vector3 posV = (1 - t) * posY + t * posZ;
        //speheres for 2nd round of interpolatio
        Gizmos.DrawWireSphere(posU, 0.1f);
        Gizmos.DrawWireSphere(posV, 0.1f);

        //third round
        Vector3 posW = (1 - t) * posU + t * posV;
        Gizmos.DrawWireSphere(posW, 0.1f);

        Handles.DrawBezier(posA, posD, posB, posC, Color.magenta, null, 3f);



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
