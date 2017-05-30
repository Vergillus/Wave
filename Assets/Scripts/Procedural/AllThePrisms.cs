﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllThePrisms : AbstractMeshGenerator {

    [SerializeField, Range(3, 50)]
    private int numSides = 3;
    [SerializeField]
    private float frontRadius;
    [SerializeField]
    private float backRadius;
    [SerializeField]
    private float lenght;

    [SerializeField]
    private Gradient gradient;

    protected override void SetMeshNum()
    {
        numVertices = 6 * numSides; //numSides vertices on each end, 4 on each lenght-side
        numTriangles = 12 * (numSides - 1); // there are 3 * (numSides -2 ) on each end and 6 on eacn lenght-side: 6 * numsides
    }

    protected override void SetNormals(){}

    protected override void SetTangents(){}

    protected override void SetTriangles()
    {
        //first end
        for (int i = 1; i < numSides - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }

        for (int i = 1; i <= numSides; i++)
        {
            //There are numSİdes triangles in the first end, so start at numSides. On each loop, need to increase. 4 * (i -1 ) does this correctly
            int val = numSides + 4 * (i - 1);

            triangles.Add(val);
            triangles.Add(val + 1);
            triangles.Add(val + 2);

            triangles.Add(val);
            triangles.Add(val + 3);
            triangles.Add(val + 1);
        }
        //other end - opposite way around so face points outwards
        for (int i = 1; i < numSides - 1; i++)
        {
            // There are numSides triangles in the first end, 4 * numsides triangles in the middle, so this starts 5*numSides
            triangles.Add(5 * numSides);
            triangles.Add(5 * numSides + i);
            triangles.Add(5 * numSides + i + 1);
        }
    }

    protected override void SetUVs(){}

    protected override void SetVertexColors()
    {
        for (int i = 0; i < numVertices; i++)
        {
            //use the values in the gradient to color
            vertexColors.Add(gradient.Evaluate((float)i / numVertices));
        }
    }

    protected override void SetVertices()
    {
        //Building block verices
        Vector3[] vs = new Vector3[2 * numSides];

        // Set the vs
        for (int i = 0; i < numSides; i++)
        {
            float angle = 2 * Mathf.PI * i / numSides;
            //one end
            vs[i] = new Vector3(frontRadius * Mathf.Cos(angle), frontRadius * Mathf.Sin(angle), 0);
            //other end
            vs[i + numSides] = new Vector3(backRadius * Mathf.Cos(angle), backRadius * Mathf.Sin(angle), lenght);
        }

        //set vertices - first end
        for (int i = 0; i < numSides; i++)
        {
            vertices.Add(vs[i]);
        }

        //middle verts
        for (int i = 0; i < numSides; i++)
        {
            vertices.Add(vs[i]);
            int secondIndex = i == 0 ? 2 * numSides - 1 : numSides + i - 1;
            vertices.Add(vs[secondIndex]);
            int thirdIndex = i == 0 ? numSides - 1 : i - 1;
            vertices.Add(vs[thirdIndex]);
            vertices.Add(vs[i + numSides]);
        }
        //other end
        for (int i = 0; i < numSides; i++)
        {
            vertices.Add(vs[i + numSides]);
        }

    }
}
