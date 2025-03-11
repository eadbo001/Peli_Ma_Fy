using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    
    //radius slider
    [Range(0.5f,5.0f)]public float radius = 5.0f;

    [Range(5f, 180f)] public float angleFOV = 45f;
    [Range(0.5f,3f)] public float heightFOV = 1f;
    //bools/checkboxes to draw vectors and stuff
    [SerializeField] bool drawRadius;
    [SerializeField] bool drawOriginVectors;
    [SerializeField] bool drawTargetVector;
    [SerializeField] bool drawLookAt;
    [SerializeField] bool drawFOV;

    
    [SerializeField] GameObject target;
    [SerializeField] GameObject lookAt;



    private void OnDrawGizmos()
    {

        Vector3 posTrigger = transform.position;
        Vector3 posTarget = target.transform.position;
        Vector3 posLookAt = lookAt.transform.position;
        //the positions for the top and bottom centre points of the cheese
        //adding the offset to the original positions both ways (+/-)
        Vector3 posTopFOV = posTrigger + (Vector3.up * heightFOV / 2 );
        Vector3 posBottomFOV = posTrigger - (Vector3.up * heightFOV / 2 );

        Vector3 vecTrigToTarget = posTarget - posTrigger;
        Vector3 vecTrigToLook = posLookAt - posTrigger;

        Vector3 normLookAt = vecTrigToLook.normalized;
        Vector3 normTarget = vecTrigToTarget.normalized;



        //FOV sector left and right vectors
        Vector3 rightFOV = Quaternion.AngleAxis(-angleFOV /2 , Vector3.up) * (normLookAt * radius);
        Vector3 leftFOV = Quaternion.AngleAxis(angleFOV / 2 , Vector3.up) * (normLookAt * radius);

        //dotproduct of the LookAt vector and the Target vector
        float dotLook_Target = Vector3.Dot(normLookAt, normTarget);
        //using Acos to make have the dot product be in radians and then converting them to degrees
        float angleLook_Target = Mathf.Acos(dotLook_Target) * Mathf.Rad2Deg;


        //if statements with bool to draw and/or not draw stuff
        if (drawOriginVectors)
        {
            //2. Draw Vectors:
            //  - from origin to trigger position
            MyDraw.DrawVectorAt(Vector3.zero, posTrigger, Color.red, 2.0f);
            //  - from origin to target 
            MyDraw.DrawVectorAt(Vector3.zero, posTarget, Color.green, 2.0f);
        }
        if (drawTargetVector)
        {
            //  - from trigger to target!!!!

            MyDraw.DrawVectorAt(posTrigger, vecTrigToTarget, Color.blue, 2.0f);
        }
        if (drawLookAt)
        {
            // - from triggerPos to LookingAt

            //if the player/target is inside the fov then the looking vector turns red

            if (angleLook_Target < angleFOV / 2.0f)
                //draws the vector to be as "long" as the radius
                MyDraw.DrawVectorAt(posTrigger, normLookAt * radius, Color.red, 2.0f);
            else
                MyDraw.DrawVectorAt(posTrigger, normLookAt * radius, Color.green, 2.0f);

        }


        // - check if target is closer than radius
        // - change drawing color when triggered
        if (drawRadius)
        {
            //1. draw trigger radius
            //using ternary conditional operator ?:
            Color origColor = Handles.color;
            Handles.color = vecTrigToTarget.sqrMagnitude < radius * radius ? new Color32(0x00, 0x6e, 0xff, 0xF1) : new Color32(0xfc, 0x60, 0xa1, 0xFF); //checks if the target is inside the detection raidus and changes colour to a different hexa colour
            Handles.DrawWireDisc(posTrigger, Vector3.up, radius, 2.0f);
            Handles.color = origColor;
        }

        if (drawFOV)
        { 
            //Drawing center lines
            MyDraw.DrawVectorAt(posTrigger, leftFOV, Color.yellow, 1f);
            MyDraw.DrawVectorAt(posTrigger, rightFOV, Color.yellow, 1f);

            
            //drawing lines between sectors
            Handles.DrawLine(posBottomFOV, posTopFOV, 1);
            Handles.DrawLine(posBottomFOV + leftFOV, posTopFOV + leftFOV, 1);
            Handles.DrawLine(posBottomFOV + rightFOV, posTopFOV + rightFOV, 1);
            //drawing sectors
            MyDraw.DrawSectorAt( posTopFOV,  leftFOV, rightFOV, angleFOV, radius, 1f, Color.white);
            MyDraw.DrawSectorAt( posBottomFOV,  leftFOV,  rightFOV, angleFOV, radius,1f, Color.white); 
        }





    }
   
}

