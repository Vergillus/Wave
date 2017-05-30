using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshRenderer))]
//[ExecuteInEditMode]
public class ProceduralMeshGenerator : MonoBehaviour {

    [SerializeField]
    private Material Mat;
    [SerializeField]
    private Vector3[] Vs = new Vector3[4];
    [SerializeField]
    private Color GizmoColor = Color.red;
    [SerializeField]
    private int Row = 1;
    [SerializeField]
    private int Column = 1;

    private List<Vector3> Vertices;
    private List<int> Triangles;

    private Mesh MyMesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

   

	// Use this for initialization
	void Start () {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = Mat;        

    }
	
	// Update is called once per frame
	void Update () {      

        Vertices = new List<Vector3>();
        Triangles = new List<int>();

        CreateMesh();

	}

    void CreateMesh()
    {
        MyMesh = new Mesh();

        SetVertices();
        SetTriangles();

        MyMesh.SetVertices(Vertices);
        MyMesh.SetTriangles(Triangles, 0);

        MyMesh.RecalculateNormals();

        meshFilter.sharedMesh = MyMesh;
    }

    void SetVertices()
    {
        for (int x = 0; x < Row; x++)
        {
            for (int z = 0; z < Column; z++)
            {
                Vs = new Vector3[] { new Vector3(x, 0, z), new Vector3(x, 0, z + 1), new Vector3(x + 1, 0, z + 1), new Vector3(x + 1, 0, z) };
                Vertices.AddRange(Vs);

                //int[] Tr = new int[] { x * (Column + Row), z + Column, z + Row, x * (Column + Row), z +Row, z +Column+Row  };
                //int[] Tr = new int[] {0,1,2,0,2,3};
                //Triangles.AddRange(Tr);
               
            }
            
        }
        //Vs = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0) };
        //Vertices.AddRange(Vs);
        //SetTriangles(0);

        //Vs = new Vector3[] { new Vector3(1, 0, 0), new Vector3(1, 0, 1), new Vector3(2, 0, 1), new Vector3(2, 0, 0) };
        //Vertices.AddRange(Vs);
        //SetTriangles(0);
        ////Triangles.Add(0);
        ////Triangles.Add(1);
        ////Triangles.Add(2);
        ////Triangles.Add(0);
        ////Triangles.Add(2);
        ////Triangles.Add(3);

    }

    void SetTriangles()
    {
        int res = (Row + Column) * 2;

        for (int i = 0; i < res ; i++)
        {

            if ((i+1) % res == 0)
            {
                continue;
            }
            else
            {
                int[] Tr = new int[] { i, i + res ,i + 1 + res, i + res + 1, i+1,i };
                Triangles.AddRange(Tr);
            }


        }

        //int[] Tr = new int[] { Index , Index +Column, Index +Row};
        //Triangles.AddRange(Tr);
        //Triangles.Add(0);
        //Triangles.Add(1);
        //Triangles.Add(2);
        //Triangles.Add(0);
        //Triangles.Add(2);
        //Triangles.Add(3);

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(Vertices[i] + transform.position, .1f);
        }
    }
}
