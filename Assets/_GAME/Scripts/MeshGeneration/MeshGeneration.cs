using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGeneration : MonoBehaviour
{
    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;

    private void Start()
    {
        MeshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
        MeshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        DrawCylinder(1, 3, 10, 4);
    }

    private void CreateCube()
    {
        var vertices = new Vector3[]
        {
            new Vector3(0, 0, 0), // 0
            new Vector3(0, 0, 1), // 1
            new Vector3(1, 0, 0), // 2
            new Vector3(1, 0, 1), // 3

            new Vector3(0, 1, 0), // 4
            new Vector3(0, 1, 1), // 5
            new Vector3(1, 1, 0), // 6
            new Vector3(1, 1, 1), // 7

        };

        var triangles = new int[]
        {
            0, 2, 1,
            2, 3, 1,
            0, 1, 4,
            4, 1 ,5,
            2, 6, 3,
            6, 7, 3,
            4, 5, 6,
            6, 5, 7,
            0, 4, 2,
            2, 4, 6,
            1, 3, 5,
            5, 3, 7,
        };

        GenerateMesh(vertices, triangles);
    }

    private void CreateCircle(float radius, int segments)
    {
        var vertices = new Vector3[segments + 1];
        var triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;

        for (int i = 1; i < vertices.Length; i++)
        {
            var angle = (Mathf.PI * 2) / segments * i;
            vertices[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        triangles[triangles.Length - 1] = 1;

        GenerateMesh(vertices, triangles);
    }

    private void DrawCylinder(float radius, float height, int segments, int heightSegments = 1)
    {

        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        for (int i = 0; i < heightSegments + 1; i++)
        {
            for (int j = 0; j < segments; j++)
            {
                var angle = (Mathf.PI * 2) / segments * j;
                var x = Mathf.Cos(angle) * radius;
                var y = Mathf.Sin(angle) * radius;
                var z = i * height / heightSegments;

                vertices.Add(new Vector3(x, y, z));
            }
        }

        for (int i = 0; i < heightSegments; i++)
        {
            for (int j = 0; j < segments; j++)
            {
                var a = i * segments + j;
                var b = i * segments + (j + 1) % segments;
                var c = (i + 1) * segments + j;
                var d = (i + 1) * segments + (j + 1) % segments;

                triangles.Add(a);
                triangles.Add(c);
                triangles.Add(b);

                triangles.Add(b);
                triangles.Add(c);
                triangles.Add(d);
            }
        }

        GenerateMesh(vertices.ToArray(), triangles.ToArray());
    }


    public void GenerateMesh(Vector3[] vertices, int[] triangles)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.uv = new Vector2[vertices.Length];

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        mesh.Optimize();


        MeshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        var mesh = MeshFilter.sharedMesh;
        if (mesh == null)
            return;

        var vertices = mesh.vertices;
        var triangles = mesh.triangles;
        Gizmos.color = Color.red;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            var a = vertices[triangles[i]];
            var b = vertices[triangles[i + 1]];
            var c = vertices[triangles[i + 2]];

            DrawTriangle(a, b, c);
        }
    }

    private void DrawTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, a);
    }
}
