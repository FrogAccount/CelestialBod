using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.UIElements;

public class Bettergen : MonoBehaviour
{
    private int previous = 0;
    public int dimensions = 2;
    public bool[] points = new bool[8];
    private Mesh mesh;
    private MeshRenderer mesh_rend;
    private Vector3[][] tri_dict = new Vector3[256][];
    private int[][] int_dict = new int[256][];

    static Vector3 v_x = new(0.5f, 0.0f, 0.0f);
    static Vector3 v_y = new(0.0f, 0.5f, 0.0f);
    static Vector3 v_z = new(0.0f, 0.0f, 0.5f);
    static Vector3 v_xy = new(1.0f, 0.5f, 0.0f);
    static Vector3 v_xz = new(1.0f, 0.0f, 0.5f);
    static Vector3 v_yx = new(0.5f, 1.0f, 0.0f);
    static Vector3 v_yz = new(0.0f, 1.0f, 0.5f);
    static Vector3 v_zx = new(0.5f, 0.0f, 1.0f);
    static Vector3 v_zy = new(0.0f, 0.5f, 1.0f);
    static Vector3 v_xyz = new(1.0f, 1.0f, 0.5f);
    static Vector3 v_xzy = new(1.0f, 0.5f, 1.0f);
    static Vector3 v_zyx = new(0.5f, 1.0f, 1.0f);

    private Vector3[] verts = { v_x, v_y, v_z, v_xy, v_xz, v_yx, v_yz, v_zx, v_zy, v_xyz, v_xzy, v_zyx };

    [SerializeField]
    private List<int> current_verts;
    [InspectorButton("generate_verts")]
    public bool genVerts;
    [SerializeField]
    private int[] current_tris;


    [InspectorButton("rotateX_f")]
    public bool rotateX;
    [InspectorButton("rotateX_y")]
    public bool rotateY;
    [InspectorButton("rotateX_z")]
    public bool rotateZ;

    [InspectorButton("generate")]
    public bool generateMesh;
    [InspectorButton("clear")]
    public bool clearMesh;

    public bool gen;

    void generate_verts() {
        current_verts.Clear();
        int[] ints = new int[12];
        int[,] connected_verts = {
            { 0, 1, 2},
            { 2, 8, 7},
            { 1, 5, 6},
            { 8, 6, 11},
            { 4, 3, 0},
            { 7, 4, 10},
            { 3, 9, 5},
            { 10, 11, 9}
        };


        for (int i = 0; i < points.Length; i++) {
            if (points[i]) {
                for (int j = 0; j < 3; j++) {
                    ints[connected_verts[i, j]]++;
                }
            }
        }

        for (int i = 0; i < ints.Length; i++) {
            if (ints[i] == 1) {
                current_verts.Add(i);            
            }
        }
    }

    void clear() {
        current_tris = null;
        current_verts.Clear();
        gen = false;
        mesh.Clear();
    }
    void generate() {
        gen = true;
        Gen();
    }

    void rotateX_f() {

        bool[] temp = new bool[8];

        int[] key = { 2, 0, 3, 1, 6, 4, 7, 5 };

        for (int i = 0; i < 8; i++) {
            temp[key[i]] = points[i];
        }

        points = temp;

        int[] tri_key = { 5, 6, 1, 9, 3, 11, 8, 0, 2, 10, 4, 7};

        for (int i = 0; i < current_verts.Count; i++) {
            current_verts[i] = tri_key[current_verts[i]];
        }

        if (gen)
        {
            generate();
        }
        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
    }

    void rotateX_y()
    {
        bool[] temp = new bool[8];

        int[] key = { 1, 5, 3, 7, 0, 4, 2, 6};

        for (int i = 0; i < 8; i++)
        {
            temp[key[i]] = points[i];
        }

        points = temp;

        int[] tri_key = { 2, 8, 7, 1, 0, 6, 11, 4, 10, 5, 3, 9 };

        for (int i = 0; i < current_verts.Count; i++)
        {
            current_verts[i] = tri_key[current_verts[i]];
        }

        if (gen)
        {
            generate();
        }
        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
    }
    void rotateX_z()
    {
        bool[] temp = new bool[8];

        int[] key = { 4, 5, 0, 1, 6, 7, 2, 3 };

        for (int i = 0; i < 8; i++)
        {
            temp[key[i]] = points[i];
        }

        int[] tri_key = { 3, 0, 4, 5, 9, 1, 2, 10, 7, 6, 11, 8 };

        for (int i = 0; i < current_verts.Count; i++)
        {
            current_verts[i] = tri_key[current_verts[i]];
        }

        points = temp;

        if (gen)
        {
            generate();
        }
        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
    }

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Gen()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        if (current_tris == null || current_verts == null) {
            return;
        }

        Vector3[] vector3s = new Vector3[current_verts.Count];
        for (int i = 0; i < current_verts.Count; i++) {
            vector3s[i] = verts[current_verts[i]];
        }
        mesh.vertices = vector3s;
        mesh.triangles = current_tris;
    }

    // Update is called once per frame
    void Update()
    {
        //GenerateMesh();
    }
    /*
    void GenerateMesh()
    {
        for (int x = 1; x < dimensions; x++)
        {
            for (int y = 1; y < dimensions; y++)
            {
                for (int z = 1; z < dimensions; z++)
                {
                    int val = 0;
                    int i = 0;
                    for (int a = -1; a < 1; a++)
                    {
                        for (int b = -1; b < 1; b++)
                        {
                            for (int c = -1; c < 1; c++)
                            {
                                val += points[(x + a) * dimensions * dimensions + (y + b) * dimensions + (z + c)] ? (int)Mathf.Pow(2, i) : 0;
                                i++;
                            }
                        }
                    }
                    Gen();
                    Debug.Log(val);
                }
            }
        }
    }
    */


    private void OnDrawGizmos()
    {
        if (previous != dimensions)
            points = new bool[dimensions * dimensions * dimensions];

        previous = dimensions;
        for (int x = 0; x < dimensions; x++)
        {
            for (int y = 0; y < dimensions; y++)
            {
                for (int z = 0; z < dimensions; z++)
                {
                    Gizmos.color = points[x * dimensions * dimensions + y * dimensions + z] ? Color.white : Color.black;
                    Gizmos.DrawSphere(new Vector3(x, y, z), 0.05f);
                }
            }
        }

        if (current_verts != null) {
            for (int i = 0; i < current_verts.Count; i++) {
                Handles.Label(verts[current_verts[i]], i.ToString());
            }
        }

    }
}
