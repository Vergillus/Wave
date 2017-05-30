using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllThePolygons : AbstractMeshGenerator
{
    [SerializeField, Range(3,50)]
    private int numSides = 3;
    [SerializeField]
    private float radius;

    protected override void SetMeshNum()
    {
        numVertices = numSides;
        numTriangles = 3 * (numSides - 2); // there are (numSides -2 ) physical triangles in a regular polygon. 3 ints describe each physical triangle, so multiple by 3
    }

    protected override void SetNormals()
    {
        
    }

    protected override void SetTangents()
    {
      
    }

    protected override void SetTriangles()
    {
        for (int i = 1; i < numSides - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
    }

    protected override void SetUVs()
    {
        
    }

    protected override void SetVertexColors()
    {
        
    }

    protected override void SetVertices()
    {
        // Coordinates of a regular polygon 
        for (int i = 0; i < numSides; i++)
        {
            float angle = 2 * Mathf.PI * i / numSides;
            vertices.Add(new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0));
        }
    }
}
