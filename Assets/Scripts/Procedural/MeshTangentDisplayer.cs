using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshTangentDisplayer : MonoBehaviour {

    [SerializeField]
    private bool drawTangents;
    [SerializeField]
    private float tangentsLength = 0.5f;

    private void OnDrawGizmos()
    {
        if (drawTangents)
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

            if (mesh != null)
            {
                for (int i = 0; i < mesh.vertexCount; i++)
                {
                    // Change these to world space so they display tangents when move transform
                    Vector3 vertex = transform.TransformPoint(mesh.vertices[i]);
                    Vector3 tangent = transform.TransformDirection(mesh.tangents[i]);

                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(vertex, vertex + tangentsLength * tangent);
                }
            }
        }
    }
}
