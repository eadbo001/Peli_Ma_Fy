using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interpolation : MonoBehaviour
{
    public GameObject ObjA;
    public GameObject ObjB;
    public GameObject ObjInter;

    public float InterpTime;
   

    
    [Range(0f, 1f)] //you can start the motion from any point between A and B and it'll return to the original spot
    public float t = 0.0f;

    private float direction = 1.0f; //starts out from A to B
    private float speed = 0.0f; //no speed

    Vector3 posInter;

    Vector3 PosA;
    Vector3 PosB;

    private void DrawVectorAt(Vector3 pos, Vector3 vec, Color c, float thickness=1.0f)
    {
        //added colour saving and changing colour inside the function and the changing from gizmos to Handles and adding the option to draw line from anywhere to anywhere

        Color orig = Handles.color; //save the original colour and the universal variable
        Handles.color = c;
        Handles.DrawLine(pos, pos + vec, thickness); //can draw vector from anywhere to anywhere

        Handles.ConeHandleCap(0, pos + vec - 0.2f * vec.normalized, Quaternion.LookRotation(vec), 0.286f, EventType.Repaint);

        Handles.color = orig; //returns the colour to the original colour
    }
    private void OnDrawGizmos()
    {
        
        

        //vector from origin to obj A
        DrawVectorAt(Vector3.zero, PosA, Color.grey, 2.0f);

        //vector from origin to obj B
        DrawVectorAt(Vector3.zero, PosB, Color.grey, 2.0f);

        //interpolated position
        
        DrawVectorAt(Vector3.zero, posInter, Color.black, 2.0f);


        Handles.DrawLine(PosA, PosB, 2.0f);
    }
    void Start()
    {
        PosA = ObjA.transform.position;
        PosB = ObjB.transform.position;
    }
    
    void Update()
    {
        //once the time limit ends ObjInter stops and freezes in position
        //don't use deltaTime because that is the time between frames and not the elapsed time from the start
        if (Time.time <= InterpTime)
        {
            //interpolation formula
            // f(t) = (1-t)*A + t*B
            //t is a value between 0 and 1 and so could be viewed as a percentage of
            //how far something has traveled from the start to the end

            speed = Time.deltaTime / (InterpTime / 2);
            //the speed from A to B is determined by Time.deltaTime / InterpTime           
            //dividing by 2 makes it the time of one round trip (from A to B and back to A.)
            //idk why Time.time fucks it up but deltaTime works but it works

            //Clamping of the t between 0 and 1 and make sure it is >= and <=
            if (t >= 1.0f)
            {
                t = 1.0f;
                direction = -1.0f; //once t reaches point B it changes direction from B to A
            }
            else if (t <= 0.0f)
            {
                t = 0.0f;
                direction = 1.0f; //once t reaches point A it changes direction form A to B
            }


            t = t + direction * speed;
            //the direction of the "trip" can be reversed by multiplying the speed by 1 or -1

            //AND THEN ADDING THAT VALUE TO t!!!!!
          
            //positive multplier makes the "trip percentage" be looked from A to B
            //A negative multiplier makes it be looked form B to A
        }
       
        posInter = (1 - t) * PosA + t * PosB;
        //interpolated position where 0 is A and 1 is B
        
        ObjInter.transform.position = posInter;
        //moving the position of the object
    }
}
