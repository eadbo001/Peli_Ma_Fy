using System.Collections.Generic;
using UnityEngine;

public class SimpleMesh : MonoBehaviour
{
    [Range(3, 64),SerializeField] int Segments;
    [Range(1f, 50f),SerializeField] float Radius;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDrawGizmos()
    {
        float delta_angle = 360.0f / Segments;

        float angle = 0f;
        for (int i = 0; i < Segments; i++)
        {
            //deg to rad
            float x = Radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            x += transform.position.x;
            y += transform.position.z;

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(new Vector3(x, 0, y), 0.2f);

            angle += delta_angle;

        }

    }
    private Mesh GenerateDiscMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        // The center of the disc (at index 0)
        vertices.Add(Vector3.zero);
        uvs.Add(new Vector2(0.5f,0.5f));

        float delta_angle = 360.0f / Segments;

        float angle = 0f;
        for (int i = 0; i < Segments; i++)
        {
            //                           DEG --> RAD
            float x =  Mathf.Cos(angle * Mathf.Deg2Rad);
            float y =  Mathf.Sin(angle * Mathf.Deg2Rad);

            // Create one vertex
            Vector3 vertex = new Vector3(x * Radius, 0, y * Radius);
            vertices.Add(vertex);  // Add it to the list

            uvs.Add(new Vector2((x / 2f) + 0.5f, (y / 2f) + 0.5f));

            angle += delta_angle;
        }

        List<int> triangles = new List<int>();
        // Loop from 0 until Segments minus one (avoid indexing above the max vertex count)
        for (int i = 0; i < Segments - 1; i++)
        {
            // 1st is always index 0
            triangles.Add(0);
            // 3nd
            triangles.Add(i + 2);
            // 2nd 
            triangles.Add(i + 1);
        }
        // Last triangle
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(Segments);


        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();

        
        return mesh;
    }
    // Update is called once per frame
    void Update()
    {
        Mesh mesh = GenerateDiscMesh();
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
