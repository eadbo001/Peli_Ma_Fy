using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MyDraw
{
    public static void DrawVectorAt(Vector3 pos, Vector3 vec, Color c, float thickness = 1.0f)
    {
        //added colour saving and changing colour inside the function and the changing from gizmos to Handles and adding the option to draw line from anywhere to anywhere

        Color orig = Handles.color; //save the original colour and the universal variable
        Handles.color = c;
        Handles.DrawLine(pos, pos + vec, thickness); //can draw vector from anywhere to anywhere

        Handles.ConeHandleCap(0, pos + vec - 0.2f * vec.normalized, Quaternion.LookRotation(vec), 0.286f, EventType.Repaint);

        Handles.color = orig; //returns the colour to the original colour
    }
    public static void DrawSectorAt( Vector3 center, Vector3 vecL, Vector3 vecR, float angle, float r, float t, Color c)
    {

        DrawVectorAt(center, vecL, c);
        DrawVectorAt(center, vecR, c);

        Color origColor = Handles.color;
        Handles.color = c;
              
        Handles.DrawWireArc(center, Vector3.up, vecR, angle, r, t);
        Handles.color = origColor;
    }
}
