using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class planet_generator : MonoBehaviour
{
    private Mesh mesh;
    private MeshRenderer mesh_rend;
    public int maximum_circumference;
    public float unit_size;

    private int dimensions;
    public float scale;

    private Texture2D noise_texture;
    private bool[,,] points;
    public float radius;
    private Vector3 center;
    public float seed = 0.0f;
    public int slice;
    public float threshold = 0.5f; 
    // Start is called before the first frame update
    void Start()
    {
        dimensions =  (int) (maximum_circumference/ unit_size);
        noise_texture = new Texture2D(dimensions, dimensions);
        points = new bool[dimensions, dimensions, dimensions];
        mesh = GetComponent<MeshFilter>().mesh;
        mesh_rend = GetComponent<MeshRenderer>();
        center = new Vector3(dimensions / 2, dimensions / 2, dimensions / 2);
        slice = dimensions / 2;
        //GenerateMesh();
        mesh_rend.material.mainTexture = noise_texture;
        
    }

    void GenerateNoise() {
        Color[] colors = new Color[dimensions * dimensions];
        int x = 0;
        while(x < dimensions)
        {
            int y = 0;
            while (y < dimensions) {
                int z = 0;
                while (z < dimensions) {
                    points[x, y, z] = GeneratePoint(x,y,z);

                    if (x == slice) {
                        if (points[x, y, z] == true)
                        {
                            colors[(int)y * dimensions + (int)z] = Color.white;
                        }
                        else
                        {
                            colors[(int)y * dimensions + (int)z] = Color.black;
                        }
                    }

                    z++;
                }
                //float value = Mathf.Max(Mathf.PerlinNoise(xCoord, yCoord) - gradient, 0.0f);
                y++;
            }
            x++;
        }
      
        noise_texture.SetPixels(colors);
        noise_texture.Apply();
    }


    bool GeneratePoint(int x, int y, int z) {
        float xCoord = (float)x / dimensions * scale;
        float yCoord = (float)y / dimensions * scale;
        float zCoord = (float)z / dimensions * scale;
        float distance = Vector3.Distance(new Vector3(x, y, z), center);
        float gradient = Mathf.Lerp(1.0f, 0.0f, distance / radius);
        float value = gradient + (
                                  Mathf.PerlinNoise(xCoord + seed, yCoord + seed) +
                                  Mathf.PerlinNoise(xCoord + seed + scale, zCoord + seed + scale) +
                                  Mathf.PerlinNoise(yCoord + seed + scale + scale, zCoord + seed + scale + scale)
                                  / 3.0f
                                  ) * gradient;
        if (value > threshold) {
            return true;
        }
        return false;
    }

    // LET'S SEE IF I REMEMBER MARCHING CUBES
    /*
    void GenerateMesh()
    {
        mesh.vertices = new Vector3[] {
                Vector3.zero, Vector3.right, Vector3.up
        };
        mesh.triangles = new int[] { 0, 2, 1 };
    }
    */


    // Update is called once per frame
    void Update()
    {
        GenerateNoise();
    }
}
