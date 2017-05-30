﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheUniqueVertQuads : AbstractMeshGenerator
{

    [SerializeField]
    private Vector3[] vs = new Vector3[4];

    protected override void SetMeshNum()
    {
        numVertices = 6;
        numTriangles = 6;
    }

    protected override void SetNormals()
    {

    }

    protected override void SetTangents()
    {

    }

    protected override void SetTriangles()
    {
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);

        triangles.Add(3);
        triangles.Add(4);
        triangles.Add(5);

    }

    protected override void SetUVs()
    {

    }

    protected override void SetVertexColors()
    {

    }

    protected override void SetVertices()
    {
        vertices.Add(vs[0]);
        vertices.Add(vs[1]);
        vertices.Add(vs[3]);

        vertices.Add(vs[0]);
        vertices.Add(vs[3]);
        vertices.Add(vs[2]);
    }
}
