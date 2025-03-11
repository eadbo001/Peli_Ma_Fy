using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanvasCalculation : MonoBehaviour
{
    public float screenWidth = 800f;
    public float screenHeight = 600f;

    //pop-up size (in %)
    [Range(0.01f, 1f)]
    public float WidthPercentage = 0.75f;
    [Range(0.01f, 1f)]
    public float HeightPercentage = 0.75f;
    
    float WidthRemnant;
    float HeightRemnant;
    private void DrawXYRectAt(Vector3 pos, float width, float height, Color c, float thickness=1.0f)
    {
        Color orig = Handles.color;//save the current color
        Handles.color = c;
        //Creating Vectors/positions for each corner of the rectangle
        //by adding parameters onto vector3 pos
        Vector3 bottomLeft = pos;
        Vector3 topLeft = pos + new Vector3(0, height, 0);
        Vector3 topRight = pos + new Vector3(width, height, 0);
        Vector3 bottomRight = pos + new Vector3(width, 0, 0);

        //Drawing the vectors/sides of the rectangle and in yellow

        Handles.DrawLine(pos, topLeft, thickness);  //Left Side
        Handles.DrawLine(topLeft, topRight, thickness); //Top Side
        Handles.DrawLine(bottomRight, topRight, thickness);  //Right Side
        Handles.DrawLine(pos, bottomRight, thickness);  //Bottom Side        

        Handles.color = orig;//return the current color

    }
    private void DrawScreenAndCanvas()
    {
        DrawXYRectAt(Vector3.zero, screenWidth, screenHeight, Color.black, 3.0f);        
    }

    private void DrawPopUp()
    {
        WidthRemnant = (1.0f - WidthPercentage) / 2.0f;
        HeightRemnant = (1.0f - HeightPercentage) / 2.0f;

        float x = screenWidth * WidthRemnant;
        float y = screenHeight * HeightRemnant;


        DrawXYRectAt(new Vector3(x, y, 0), screenWidth * WidthPercentage, screenHeight * HeightPercentage, Color.red, 3.0f);
    }

    private void OnDrawGizmos()
    {
        DrawScreenAndCanvas();
        DrawPopUp();
    }
}
