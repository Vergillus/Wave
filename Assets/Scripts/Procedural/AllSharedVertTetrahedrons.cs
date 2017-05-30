using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSharedVertTetrahedrons : AbstractMeshGenerator
{
    [SerializeField]
    private Vector3[] vs = new Vector3[4];

    protected override void SetMeshNum()
    {
        numVertices = 4;
        numTriangles = 12; //Tethrahedron has 4 side, all triangular. 4 physical triangles * 3 ints for each = 12
    }

    protected override void SetNormals() { }

    protected override void SetTangents() { }

    protected override void SetTriangles()
    {
        //base - swap so that face points outwards
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);

        //sides
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);

        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(1);

        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(0);
    }

    protected override void SetUVs(){ }

    protected override void SetVertexColors(){ }

    protected override void SetVertices()
    {
        vertices.AddRange(vs);
    }
}
