using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AcceleratingMotion : MonoBehaviour
{
    [SerializeField]private Vector2 pos = Vector2.zero;      // initliat position: x=y=0 m
    [SerializeField] private Vector2 vel = new Vector2(10f, 10f); // initial velocity v_x = V_y = 10 m/s
    [SerializeField] private Vector2 accel = new Vector2(0f, -9.81f); //gravity: 9.81 m/(s^2)
    [Range(0, 3000)][SerializeField] private int n = 50;
    [Range(0f, 10f)][SerializeField] private float time = 5f;

    

    private void OnDrawGizmos()
    {
        Debug.Log(vel.magnitude);
        //XY-axis
        MyDraw.DrawVectorAt(Vector3.zero, 10 * Vector3.right, Color.red);
        Handles.Label(10 * Vector3.right, "X");
        MyDraw.DrawVectorAt(Vector3.zero, 10 * Vector3.up, Color.green);
        Handles.Label(10 * Vector3.up, "Y");

        //draw the velocity vector at initial position
        //MyDraw.DrawVectorAt(pos,vel, Color.black);

        Vector2 v = vel;
        Vector2 s = pos;

        //simulate accelarating motion: s = s0 + vt + 0.5at^2

        //delta time between points
        float delta_t = time / n;
        for (int i = 0; i <= n; i++)
        {
            //elapsed time:
            float t = delta_t * i;

            //1st update velocity
            v = v + accel*delta_t;

            //2nd update the position 
            s = s + v*delta_t;

            //3rd draw the "physics engine" position

            Vector2 position = s + v * t + 0.5f * accel * t * t;

            if (i % 60 == 0)
                MyDraw.DrawVectorAt(s, v, Color.yellow);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(s, 0.05f);
        }
    }
}
