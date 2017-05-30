using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTheTriangles : AbstractMeshGenerator {

    [SerializeField]
    private Vector3[] vs = new Vector3[3];

    protected override void SetMeshNum()
    {
        numVertices = 3;
        numTriangles = 3;
    }

    protected override void SetVertices()
    {
        vertices.AddRange(vs);
    }

    protected override void SetTriangles()
    {
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);
    }

    protected override void SetNormals() { }
    protected override void SetTangents() { }
    protected override void SetUVs() { }
    protected override void SetVertexColors() { }

}
