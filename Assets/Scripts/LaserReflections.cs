using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflections : MonoBehaviour
{
    [Range(0, 10)] int reflectionNumber;
    private void OnDrawGizmos()
    {
        //making the parameters for the raycast
        Vector3 orig = transform.position;
        Vector3 dir = transform.right;
        RaycastHit hit;

        bool hitCheck = Physics.Raycast(orig, dir, out hit);
        Debug.Log("HIT?: " + hitCheck); 

       
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
