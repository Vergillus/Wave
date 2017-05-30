using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheUniqueVertTetrahedrons : AbstractMeshGenerator
{
    [SerializeField]
    private Vector3[] vs = new Vector3[4];
    

    protected override void SetMeshNum()
    {
        numVertices = 12; //one per vertex per side
        numTriangles = 12; //Tethrahedron has 4 side, all triangular. 4 physical triangles * 3 ints for each = 12
    }

    protected override void SetNormals(){ }

    protected override void SetTangents(){ }

    protected override void SetTriangles()
    {
        for (int i = 0; i < numTriangles; i++)
        {
            triangles.Add(i);
        }
    }

    protected override void SetUVs(){ }

    protected override void SetVertexColors(){}

    protected override void SetVertices()
    {
        vertices.Add(vs[0]);
        vertices.Add(vs[2]);
        vertices.Add(vs[1]);

        vertices.Add(vs[0]);
        vertices.Add(vs[3]);
        vertices.Add(vs[2]);

        vertices.Add(vs[2]);
        vertices.Add(vs[3]);
        vertices.Add(vs[1]);

        vertices.Add(vs[1]);
        vertices.Add(vs[3]);
        vertices.Add(vs[0]);

    }
}
