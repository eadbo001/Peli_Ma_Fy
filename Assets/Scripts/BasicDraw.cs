using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicDraw : MonoBehaviour
{
    public GameObject target;

    [Header("Axes Settings")]
    public float axis_length = 3.0f;

    public GameObject axesOrigin;

    [Header("Rectangle Settings")]
    [Range(0.1f, 10f)] public float rectWidth;
    [Range(0.1f, 10f)] public float rectHeight;
    public GameObject rectPos;

    //Draws a vector from pos and to vec
    private void DrawVectorAt(Vector3 pos, Vector3 vec, Color c)
    {
        //added colour saving and changing colour inside the function and the changing from gizmos to Handles and adding the option to draw line from anywhere to anywhere
       
        Color orig = Handles.color; //save the original colour and the universal variable
        Handles.color = c;
        Handles.DrawLine(pos, pos+vec); //can draw vector from anywhere to anywhere

        Handles.ConeHandleCap(0,pos+vec-0.2f*vec.normalized,Quaternion.LookRotation(vec),0.286f,EventType.Repaint);

        Handles.color = orig; //returns the colour to the original colour
    }
    
    //changing so that the axis use the draw vector function and then OnDrawGizmos we get the length of the axis
    private void DrawAxesAt(Vector3 location, float axis_magnitude)
    {
        //X-axis
        DrawVectorAt(location, new Vector3(axis_magnitude,0,0), Color.red);
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(location, location + new Vector3(axis_length, 0)); 

        //Y-axis
        DrawVectorAt(location, new Vector3(0, axis_magnitude, 0), Color.green);
        //Gizmos.color += Color.green;
        //Gizmos.DrawLine(location, location + new Vector3(0, axis_length));

    }

    //adding colour management and switching to handles
    //making it so that the Rectangle is drawn from the bottom left corner
    private void DrawXYRectAt(Vector3 pos, float width, float height, Color c) 
    {
        Color orig = Handles.color;
        Handles.color = c;
        //Creating Vectors/positions for each corner of the rectangle
        //by adding parameters onto vector3 pos
        Vector3 topLeft = pos + new Vector3 (0, height, 0);
        Vector3 topRight = pos + new Vector3 (width, height, 0);
        Vector3 bottomRight = pos + new Vector3(width, 0, 0);

        //Drawing the vectors/sides of the rectangle and in yellow
        
        Handles.DrawLine(pos, topLeft);  //Left Side
        Handles.DrawLine(topLeft, topRight); //Top Side
        Handles.DrawLine(bottomRight, topRight);  //Right Side
        Handles.DrawLine(pos, bottomRight);  //Bottom Side        

        Handles.color = orig;
    
    }
    
    public void DrawWireDiscAt(Vector3 pos, float radius, Color c)
    {
        Color orig = Handles.color;

        //Unit circle
        Handles.color = c;
        Handles.DrawWireDisc(pos, Vector3.forward, radius);

        Handles.color = orig;
    }
    private void OnDrawGizmos()
    {
        DrawAxesAt(Vector3.zero, axis_length);

        DrawVectorAt(Vector3.zero,axesOrigin.transform.position, Color.magenta);
        DrawAxesAt(axesOrigin.transform.position, axis_length / 2);

        DrawWireDiscAt(new Vector3(1, 1), 1.0f, Color.blue);
        
        DrawXYRectAt(rectPos.transform.position, rectWidth, rectHeight, Color.white);
        DrawAxesAt(rectPos.transform.position, 0.5f);
        
    }
}
