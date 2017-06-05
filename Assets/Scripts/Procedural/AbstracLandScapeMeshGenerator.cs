using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstracLandScapeMeshGenerator : AbstractMeshGenerator {

    [SerializeField]
    protected int xResolution = 20;
    [SerializeField]
    protected int zResolution = 20;
    [SerializeField]
    protected float meshScale = 1;
    [SerializeField]
    protected float yScale = 1;

    [SerializeField, Range(1, 8)]
    protected int octaves = 1;
    [SerializeField]
    protected float lacunarity = 2;
    [SerializeField, Range(0, 1)]
    protected float gain = 0.5f;
    [SerializeField]
    protected float perlinScale = 1;

    [SerializeField]
    protected FallOffType type;
    [SerializeField]
    protected float fallOffSize;
    [SerializeField]
    protected float seaLevel;

    protected float FallOff(float x, float height, float z)
    {
        // Shift the coordinates to the centre
        x = x - xResolution / 2;
        z = z - xResolution / 2;

        float fallOff = 0;

        switch (type)
        {
            case FallOffType.None:
                return height;
            case FallOffType.Circular:
                fallOff = Mathf.Sqrt(x * x + z * z) / fallOffSize;
                return GetHeight(fallOff, height);
            case FallOffType.RoundedSquare:
                fallOff = Mathf.Sqrt(x * x * x * x + z * z * z * z) / fallOffSize;
                return GetHeight(fallOff, height);
            default:
                print("Unrecognised Falloff type: " + type);
                return height;
        }
    }

    private float GetHeight(float fallOff, float height)
    {
        // Faloff is 0,0 in the cente and then increases outwards
        if (fallOff < 1)
        {
            // Make the gradient softer. This is the smoothstep function
            fallOff = fallOff * fallOff * (3 - 2 * fallOff);

            // Gradiiant the height
            height = height - fallOff * (height - seaLevel);
        }
        else
        {
            height = seaLevel;
        }

        return height;
    }


    protected override void SetVertices() { }
    protected override void SetTriangles() { }
    protected override void SetMeshNum() { }
    protected override void SetUVs() { }
    protected override void SetNormals() { }
    protected override void SetTangents() { }
    protected override void SetVertexColors() { }

}
