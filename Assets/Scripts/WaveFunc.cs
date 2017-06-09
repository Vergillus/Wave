using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunc : MonoBehaviour {

    //[SerializeField, Range(1.0f, 20.0f)]
    private float[] Wavelenght;

    //[SerializeField, Range(1.0f, 10.0f)]
    private float[] Speed;

    [SerializeField, Range(0.0f, 1.0f)]
    private float steepnessParameter = 0.5f;
    [SerializeField, Range(1.0f, 5.0f)]
    private int numWaves = 1;

    Renderer Rend;
    Mesh Mesh;
    MeshCollider MyMeshCollider;
    Vector3[] Vertices;
    Color[] VertexColors;
    Vector3[] Direction;

    int Index = 0;
    float[] Frequency;
    float[] PhaseConstant;    
    float ElapsedTime;
    float[] Steepness;
    float[] Amplitute;
    // Use this for initialization
    void Start () {

        Mesh = GetComponent<MeshFilter>().mesh;
        Rend = GetComponent<Renderer>();
        MyMeshCollider = GetComponent<MeshCollider>();

        ElapsedTime = 0;

        //Frequency = Mathf.Sqrt(((2 * Mathf.PI) / Wavelenght) * Physics.gravity.y * -1);
        //Frequency = 2 / Wavelenght;        
        //PhaseConstant = Speed * Frequency;        

        Vertices = Mesh.vertices;

        Wavelenght = new float[Vertices.Length];
        Speed = new float[Vertices.Length];
        Frequency = new float[Vertices.Length];
        PhaseConstant = new float[Vertices.Length];
        Amplitute = new float[Vertices.Length];
        Steepness = new float[Vertices.Length];
        Direction = new Vector3[Vertices.Length];
        VertexColors = new Color[Vertices.Length];

        for (int i = 0; i < Vertices.Length; i++)
        {
            Wavelenght[i] = Random.Range(10.0f, 50.0f);
            Speed[i] = Random.Range(1.0f, 20.0f);

            Frequency[i] = Mathf.Sqrt(((2 * Mathf.PI) / Wavelenght[i]) * Physics.gravity.y * -1);
            PhaseConstant[i] = Speed[i] * Frequency[i];
            Amplitute[i] = Random.Range(1.0f, 50.0f);
            Steepness[i] = Mathf.Clamp(steepnessParameter / (Wavelenght[i] * Amplitute[i] * 1),0.0f,1.0f);
            
            Direction[i] = new Vector3(Random.Range(1.0f, 1.0f), Random.Range(0.0f, 0.0f), Random.Range(1.0f, 1.0f));
        }

	}
	
	// Update is called once per frame
	void Update () {

        Index = 0;
        //ElapsedTime = 0;
        ElapsedTime += Time.deltaTime;

        GerstnerWaves(ElapsedTime);      

    }

    void GerstnerWaves(float Time)
    {

        while (Index < Vertices.Length)
        {
            //float wave = Amplitute[Index] * Mathf.Sin(Vector3.Dot (Direction,Vertices[Index]) * Frequency + ElapsedTime * PhaseConstant);
            //Debug.Log(wave);
            //float wave = (Mathf.Sin(ElapsedTime * 5.0f) * 0.05f);
            //Vertices[Index].y = Mathf.Sin(Vertices[Index].x + ElapsedTime);

            //T += Mathf.Cos(Vector3.Dot(Direction[Index], Vertices[Index]) * Frequency + ElapsedTime * PhaseConstant);

            float waveY = Steepness[Index] * Amplitute[Index] * Direction[Index].y * Mathf.Cos(Vector3.Dot(Direction[Index], Vertices[Index]) * Frequency[Index] + ElapsedTime * PhaseConstant[Index]);
            float waveX = Steepness[Index] * Amplitute[Index] * Direction[Index].x * Mathf.Cos(Vector3.Dot(Direction[Index], Vertices[Index]) * Frequency[Index] + ElapsedTime * PhaseConstant[Index]);
                        
            waveY += waveY + waveX + waveX;            
            Vertices[Index].y = waveY;            
            //VertexColors[Index] = new Color(Vertices[Index].y, Vertices[Index].y, Vertices[Index].y) + Rend.material.color;            
            Index++;
        }

        Mesh.vertices = Vertices;
        //Mesh.colors = VertexColors;
        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();
        Mesh.MarkDynamic();

        MyMeshCollider.sharedMesh = Mesh; 
        

        //transform.position = new Vector3(0, Mathf.Sin(Time),0);
    }
}
