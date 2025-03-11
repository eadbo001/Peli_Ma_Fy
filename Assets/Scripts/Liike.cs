using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liike : MonoBehaviour
{
    [Range(0, 100)][SerializeField] private int amount = 50;
    [Range(-5f, 5f)][SerializeField] private float velocity = 1.0f;
    [Range(0f, 100f)][SerializeField] private float time = 5f;
    [Range(0f, 10f)][SerializeField] private float startPoint = 0.0f;
    [Range(-3f, 3f)][SerializeField] private float acceleration = 0.0f;


    private void OnDrawGizmos()
    {
        MyDraw.DrawVectorAt(Vector3.zero, 10*Vector3.right, Color.red);
        MyDraw.DrawVectorAt(Vector3.zero, 10 * Vector3.up, Color.green);

        float delta_t = time / amount;

        for (int i = 0;i<=amount;i++)
        {

            //elapsed time
            float t = delta_t * i;
            //s = vt
            float s = velocity*t + (0.5f * acceleration * (t*t));

            //draw the circles
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(new Vector2(t, s + startPoint), 0.1f);
        }
    }
}
