using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshCollider))]
[ExecuteInEditMode]
public abstract class AbstractMeshGenerator : MonoBehaviour
{

    protected List<Vector3> vertices;
    protected List<int> triangles;
    protected List<Vector3> normals;
    protected List<Vector4> tangents;
    protected List<Vector2> uvs;
    protected List<Color32> vertexColors;

    [SerializeField]
    protected Material material;

    protected int numVertices;
    protected int numTriangles;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    // Update is called once per frame
    void Update()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        meshRenderer.material = material;

        InitMesh();
        SetMeshNum();

        CreateMesh();
    }

    private bool ValidateMesh()
    {
        string errorStr = "";

        errorStr += vertices.Count == numVertices ? "" : "Should be " + numVertices + "vertices, but there are " + vertices.Count + ". ";
        errorStr += triangles.Count == numTriangles ? "" : "Should be " + numTriangles + "triangles, but there are " + triangles.Count + ". ";

        errorStr += (normals.Count == numVertices || normals.Count == 0) ? "" : "Should be " + numVertices + "normals, but there are " + normals.Count + ". ";
        errorStr += (tangents.Count == numVertices || tangents.Count == 0) ? "" : "Should be " + numVertices + "tangents, but there are " + tangents.Count + ". ";
        errorStr += (uvs.Count == numVertices || uvs.Count == 0) ? "" : "Should be " + numVertices + "uvs, but there are " + uvs.Count + ". ";
        errorStr += (vertexColors.Count == numVertices || vertexColors.Count == 0) ? "" : "Should be " + numVertices + "vertexColors, but there are " + vertexColors.Count + ". ";

        bool isValid = string.IsNullOrEmpty(errorStr);
        if (!isValid)
        {
            Debug.LogError("Not drawing mesh." + errorStr);
        }

        return isValid;
    }

    private void InitMesh()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        normals = new List<Vector3>();
        tangents = new List<Vector4>();
        uvs = new List<Vector2>();
        vertexColors = new List<Color32>();
    }

    private void CreateMesh()
    {
        mesh = new Mesh();

        SetVertices();
        SetTriangles();

        SetNormals();
        SetUVs();
        SetTangents();       
        SetVertexColors();

        if (ValidateMesh())
        {
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);

            if (normals.Count == 0)
            {
                mesh.RecalculateNormals();
                normals.AddRange(mesh.normals);
            }

            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            mesh.SetTangents(tangents);            
            mesh.SetColors(vertexColors);

            meshFilter.mesh = mesh;
            meshCollider.sharedMesh = mesh;
        }


    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.TransformPoint(vertices[i]), .1f);
        }
    }


    protected abstract void SetVertices();
    protected abstract void SetTriangles();
    protected abstract void SetMeshNum();
    protected abstract void SetNormals();
    protected abstract void SetTangents();
    protected abstract void SetUVs();
    protected abstract void SetVertexColors();

    protected void SetGeneralNormals()
    {
        int numGeometricTriangles = numTriangles / 3;
        Vector3[] norms = new Vector3[numVertices];
        int index = 0;

        for (int i = 0; i < numGeometricTriangles; i++)
        {
            // The triangle ints that make up a geometric triangle
            int triA = triangles[index];
            int triB = triangles[index + 1];
            int triC = triangles[index + 2];


            // Directions from index-th vertex that make up the triangle
            Vector3 dirA = vertices[triB] - vertices[triA];
            Vector3 dirB = vertices[triC] - vertices[triA];

            // Normal need to come out of the plane - use the left hand rule to work out the order of the cross product
            Vector3 normal = Vector3.Cross(dirA, dirB);

            // Add the normals for each vertex cumulatively so that shared verices are added together.
            norms[triA] += normal;
            norms[triB] += normal;
            norms[triC] += normal;

            index += 3;           

        }
        //go through the verices and normalize the norms (as they are sums)
        for (int j = 0; j < numVertices; j++)
        {
            normals.Add(norms[j].normalized);
        }
    }

    protected void SetGeneralTangents()
    {
        if (uvs.Count == 0 || normals.Count == 0)
        {
            print("Set UVs amd Normals before adding tangents");
            return;
        }


        int numGeometricTriangles = numTriangles / 3;
        Vector3[] tans = new Vector3[numVertices];
        Vector3[] bitans = new Vector3[numVertices];
        int index = 0;

        for (int i = 0; i < numGeometricTriangles; i++)
        {
            // The triangle ints that make up a geometric triangle
            int triA = triangles[index];
            int triB = triangles[index + 1];
            int triC = triangles[index + 2];

            // The corresponding UVs
            Vector2 uvA = uvs[triA];
            Vector2 uvB = uvs[triB];
            Vector2 uvC = uvs[triC];

            // Directions from index-th vertex that make up the triangle
            Vector3 dirA = vertices[triB] - vertices[triA];
            Vector3 dirB = vertices[triC] - vertices[triA];

            Vector2 uvDiffA = new Vector2(uvB.x - uvA.x, uvC.x - uvA.x);
            Vector2 uvDiffB = new Vector2(uvB.y - uvA.y, uvC.y - uvA.y);

            float determinant = 1f / (uvDiffA.x * uvDiffB.y - uvDiffA.y * uvDiffB.x);
            Vector3 sDir = determinant * (new Vector3(uvDiffB.y * dirA.x - uvDiffB.x * dirB.x, uvDiffB.y * dirA.y - uvDiffB.x * dirB.y, uvDiffB.y * dirA.z - uvDiffB.x * dirB.z));
            Vector3 tDir = determinant * (new Vector3(uvDiffA.x * dirB.x - uvDiffA.y * dirA.x, uvDiffA.x * dirB.y - uvDiffA.y * dirA.y, uvDiffA.x * dirB.z - uvDiffA.y * dirA.z));

            // Add the tangents for each vertex cumulatively so that all contributons are added
            tans[triA] += sDir;
            tans[triB] += sDir;
            tans[triC] += sDir;

            // And for bitans
            bitans[triA] += tDir;
            bitans[triB] += tDir;
            bitans[triC] += tDir;

            index += 3;
        }
        //go through the verices and normalize the norms (as they are sums)
        for (int j = 0; j < numVertices; j++)
        {
            Vector3 normal = normals[j];
            Vector3 tan = tans[j];

            // Use the Gram-Schmit algorithm to make normal and tan orthononal, then normalise
            Vector3 tangent3 = (tan - Vector3.Dot(normal, tan) * normal).normalized;
            Vector4 tangent = tangent3;

            // Calculate handedness
            tangent.w = Vector3.Dot(Vector3.Cross(normal, tan), bitans[j]) < 0f ? -1f : 1f;
            tangents.Add(tangent);
            

        }
    }

}
