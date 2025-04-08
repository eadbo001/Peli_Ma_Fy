using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [Header("Particle")]
    //Model for our Physics particle
    [Range(0.01f, 1000f)] public float Mass = 1.0f; //default mass 1kg
    public Vector3 Position = Vector3.zero;
    public Vector3 Velocity = Vector3.zero;

    //Forces:
    // ---------------
    //Earth-like gravity
    [Header("Gravity")]
    public Vector3 Gravity = new Vector3(0, -9.81f, 0);

    [Header("Wind")]
    public float SurfaceArea = 1.0f;
    public Vector3 WindForce = Vector3.zero;
    public bool UseWind = false;

    [Header("Spring Force")]
    [Range(0.1f, 100f)] public float SpringConstant = 5.0f;
    public GameObject SpringLocationObject;
    public float NormalLength = 3f;
    private Vector3 SpringLocation;
    public bool UseSpring = false; 
    bool ApplySpring = false;

    [Header("Buoyancy")]
    public float WaterHeight = 0.0f;
    public float rho = 1000.0f; // 1000 kilograms per cubic meter

    [Header("Misc")]
    public bool AirResistance = false;

    private void OnDrawGizmos()
    {
        if (ApplySpring)
            Handles.color = Color.black;
        else Handles.color = Color.white;
        if (UseSpring)
            Handles.DrawLine(Position, SpringLocation, 3f);

        //Water level
        Handles.color = Color.blue;
        Handles.DrawLine(new Vector3(-100.0f, WaterHeight, 0.0f),
                         new Vector3(100.0f, WaterHeight, 0.0f),3f);


    }
    private void Start()
    {
        Position = transform.position;
        SpringLocation = SpringLocationObject.transform.position;
    }
    private void FixedUpdate()
    {
        //Zero out the resultant force
        Vector3 ResultantForce = Vector3.zero;

        //Add Gravity;
        ResultantForce += Gravity * Mass; //G = mg
        // ... add other forces to the resultant!

        //add Windforce:
        if (UseWind)
            ResultantForce += WindForce * SurfaceArea;

        //Spring force
        //Compute vector from particle to spring location
        Vector3 springVector = SpringLocation - Position;
        float delta_l = springVector.magnitude - NormalLength;

        ApplySpring = false;
        //Bungee?
        if (delta_l < 0.0f)
            ApplySpring = true;
        //F = -k * delta_l (*springVector.normalized)
        Vector3 SpringForce = SpringConstant * delta_l * springVector.normalized;
        if (ApplySpring&& UseSpring)
        {
            ResultantForce += SpringForce;
        }

        //Lift/Buoyancy
        Vector3 LiftForce = Vector3.zero;

        //Volume of the Cube
        float V = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        float height = transform.localScale.y;
        //case 1: totally airborne, not under water
        if (Position.y - height /2.0f >= WaterHeight)
        {
            //no lift/buoyancy
            LiftForce = Vector3.zero;
        }
        //case 2: totally under water
        else if (Position.y + height /2.0f < WaterHeight)
        {
            //F = rho * V * g
            //Force = density * Volume * gravity (opposite the  gravity)
            LiftForce = rho * V * (-Gravity);
        }
        //case 3: todo
        else
        {
           
            //how many percantage the object underwater??
            //waterlevel - bottom of object (position.y + height / 2.0f)
            float underwater = WaterHeight - (Position.y - height/2.0f);
            float UWPercantage = underwater / height;

            LiftForce = rho * (V * UWPercantage) * (-Gravity);
        }

        //Add the lift Force
        ResultantForce += LiftForce;
        //endlift / buoyancy

        //1. solve acceleration
        Vector3 Acceleration = ResultantForce / Mass;
        // 2. update velocity 
        Velocity += Acceleration * Time.fixedDeltaTime;

        //(fake air resistance)
        if(AirResistance)
            Velocity *= (1f - 0.1f*Time.fixedDeltaTime);
        //3. update position 
        Position += Velocity * Time.fixedDeltaTime;

        /*if (Position.y < 0)
        {
            Position.y = 0f;
            //Should we stop the particle
            Time.timeScale = 0f;

        }
       */
        //finally, set the gameobjevt to our newly computed position
        transform.position = Position;
       
    }
}
