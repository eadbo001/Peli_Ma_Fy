using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float leftWallX = 0.0f;
    public float rightWallX = 20.0f;
    public float topWallY = 10.0f;
    public float bottomWallY = 0.0f;

    //array of colliders
    public GameObject[] Colliders;


    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        //left wall: (LeftWallX, bottom wall y) -- (leftwallX, TopWallY)
        Handles.DrawLine(new Vector2(leftWallX, bottomWallY), new Vector2(leftWallX, topWallY));
        
        //Right wall: (RightWallX, bottom wall y) -- (RightwallX, TopWallY)
        Handles.DrawLine(new Vector2(rightWallX, bottomWallY), new Vector2(rightWallX, topWallY));
        //Top wall: (Leftwallx, Top wall y) -- (rightwallX, TopWallY)
        Handles.DrawLine(new Vector2(leftWallX, topWallY), new Vector2(rightWallX, topWallY));
        //Bottom wall: (leftwallx, bottom wall y) -- (leftwallX, BottomWallY)
        Handles.DrawLine(new Vector2(leftWallX, bottomWallY), new Vector2(rightWallX, bottomWallY));
    }
    private void FixedUpdate()
    {
        foreach (var collider in Colliders)
        {

            //GameObject 
            float r = collider.GetComponent<CircleCollider>().radius;
            //velocity
            Vector3 vel = collider.GetComponent<Particle>().Velocity;

            if (collider.transform.position.x + r >= rightWallX)
            {
                //remove overlap
                collider.GetComponent<Particle>().Position.x = rightWallX - r;
                //reverse velocity
                collider.GetComponent<Particle>().Velocity.x = -vel.x;
            }
            if (collider.transform.position.x - r <= leftWallX)
            {
                //remove overlap
                collider.GetComponent<Particle>().Position.x = leftWallX + r;
                //reverse velocity
                collider.GetComponent<Particle>().Velocity.x = -vel.x;
            }
            if (collider.transform.position.y - r <= bottomWallY)
            {
                //remove overlap
                collider.GetComponent<Particle>().Position.y = bottomWallY + r;
                //reverse velocity
                collider.GetComponent<Particle>().Velocity.y = -vel.y;
            }
            if (collider.transform.position.y + r >= topWallY)
            {
                //remove overlap
                collider.GetComponent<Particle>().Position.y = topWallY - r;
                //reverse velocity
                collider.GetComponent<Particle>().Velocity.y = -vel.y;
            }
        }
    }
}
