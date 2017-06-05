using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator{

    private int octaves;    // Can also be called layers. Increasing this increses the level of detail in the terrain
    private float lacunarity;
    private float gain;
    private float perlinScale;

    public NoiseGenerator(int octaves, float lacunarity, float gain, float perlinScale)
    {
        this.octaves = octaves;
        this.lacunarity = lacunarity;
        this.gain = gain;
        this.perlinScale = perlinScale;
          
    }

    public NoiseGenerator()
    {

    }

    public float GetValueNoise()
    {
        return Random.value;
    }

    public float GetPerlinNoise(float x, float z)
    {
        //Mathf.PerlinNoise gives a float between 0 and 1. For better fractal terrain, change this to values between -1 and +1
        return (2 * Mathf.PerlinNoise(x, z) - 1 );
    }

    public float GetFractalNoise(float x, float z)
    {
        float fractalNoise = 0;

        float frequency = 1;
        float amplitute = 1;

        for (int i = 0; i < octaves; i++)
        {
            float xVal = x * frequency * perlinScale;
            float zVal = z * frequency * perlinScale;

            fractalNoise += amplitute * GetPerlinNoise(xVal, zVal);

            frequency *= lacunarity; //Determines how quickly the frequency changes
            amplitute *= gain; // Determines how quickly the amplitute changes over each octave
        }

        return fractalNoise;
    }

}
