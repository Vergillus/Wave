using System.Collections;
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

    private Vector3[] vs;

    protected override void SetMeshNum()
    {
        numVertices = 6 * numSides; //numSides vertices on each end, 4 on each lenght-side
        numTriangles = 12 * (numSides - 1); // there are 3 * (numSides -2 ) on each end and 6 on eacn lenght-side: 6 * numsides
    }

    protected override void SetNormals()
    {
        SetGeneralNormals();
        ////Polygon end
        //Vector3 frontNormal = new Vector3(0, 0, -1);
        //for (int i = 0; i < numSides; i++)
        //{
        //    normals.Add(frontNormal);
        //}

        ////Middle
        //for (int i = 0; i < numSides; i++)
        //{
        //    // All sides are rectabgles. All normals are perpendicular to the face.
            
        //    int index = numSides + 4 * i;
        //    Vector3 dirOne = vertices[index + 2] - vertices[index]; // from top left corner of the face to the buttom left      
        //    Vector3 dirTwo = vertices[index + 3] - vertices[index]; // from top left corner of the face to the top right


        //    //Normal need to come out of the plane - use the left hand rule to work out the order of the cross product
        //    Vector3 normal = Vector3.Cross(dirTwo,dirOne).normalized;

        //    //add for each of the 4 corners
        //    for (int j = 0; j < 4; j++)
        //    {
        //        normals.Add(normal);
        //    }
        //}


        ////Other polygon end
        //Vector3 backNormal = new Vector3(0, 0, 1);
        //for (int i = 0; i < numSides; i++)
        //{
        //    normals.Add(backNormal);
        //}

    }

    protected override void SetTangents()
    {
        SetGeneralTangents();
        //// In the direection of the uvs
        //Vector4 frontTangent = new Vector4(1, 0, 0, -1); // -1 because of left hand rule
        //for (int i = 0; i < numSides; i++)
        //{
        //    tangents.Add(frontTangent);
        //}

        //for (int i = 0; i < numSides; i++)
        //{
        //    // All sides are rectabgles. All normals are perpendicular to the face.
        //    int index = numSides + 4 * i;
        //    Vector3 uDir = vertices[index] - vertices[index + 2]; //front bottom left corner of the face to the top left - in the direction of the u coordinate of the UVs
        //    Vector4 sideTangent = uDir.normalized;
        //    sideTangent.w = 1; //left hand rule
        //    //Add for each of the 4 corners
        //    for (int n = 0; n < 4; n++)
        //    {
        //        tangents.Add(sideTangent);
        //    }
        //}

        //Vector4 backTangent = new Vector4(1, 0, 0, 1); // +1 because of left hand rule
        //for (int i = 0; i < numSides; i++)
        //{
        //    tangents.Add(backTangent);
        //}
    }

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

    protected override void SetUVs()
    {
        //polygon end
        for (int i = 0; i < numSides; i++)
        {
            uvs.Add(vs[i]);
        }
        //middle polygon
        //polygon end
        for (int i = 0; i < numSides; i++)
        {
            uvs.Add(new Vector2(frontRadius, 0));
            uvs.Add(new Vector2(0, lenght));
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(backRadius, lenght));
        }

        //other polygon end
        for (int i = 0; i < numSides; i++)
        {
            uvs.Add(vs[i + numSides]);
        }

    }

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
        vs = new Vector3[2 * numSides];

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
