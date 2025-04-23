using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{

    [Range(0.1f, 5f)]public float radius = 1.0f;

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius, 2f);

    }
    
}
