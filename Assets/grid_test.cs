using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class grid_test : MonoBehaviour
{
    private int previous = 0;
    public int dimensions = 2;
    public bool[] points;
    private Mesh mesh;
    private MeshRenderer mesh_rend;
    private Vector3[][] tri_dict = new Vector3[128][];
    private int[][] int_dict = new int[128][];

    private Vector3 v_x   = new(0.5f, 0.0f, 0.0f);
    private Vector3 v_y   = new(0.0f, 0.5f, 0.0f);
    private Vector3 v_z   = new(0.0f, 0.0f, 0.5f);
    private Vector3 v_xy  = new(1.0f, 0.5f, 0.0f);
    private Vector3 v_xz  = new(1.0f, 0.0f, 0.5f);
    private Vector3 v_yx  = new(0.5f, 1.0f, 0.0f);
    private Vector3 v_yz  = new(0.0f, 1.0f, 0.5f);
    private Vector3 v_zx  = new(0.5f, 0.0f, 1.0f);
    private Vector3 v_zy  = new(0.0f, 0.5f, 1.0f);
    private Vector3 v_xyz = new(1.0f, 1.0f, 0.5f);
    private Vector3 v_xzy = new(1.0f, 0.5f, 1.0f);
    private Vector3 v_zyx = new(0.5f, 1.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        points = new bool[dimensions * dimensions * dimensions];
        mesh = GetComponent<MeshFilter>().mesh;

        tri_dict[0] = null;
        tri_dict[1] = new Vector3[] { v_x, v_y, v_z };
        int_dict[1] = new int[] { 0, 1, 2 };

        tri_dict[2] = new Vector3[] { v_z, v_zy, v_zx};
        int_dict[2] = new int[] { 0, 1, 2 };

        tri_dict[3] = new Vector3[] { v_x, v_y, v_zy, v_zx};
        int_dict[3] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[4] = new Vector3[] { v_y, v_yx, v_yz };
        int_dict[4] = new int[] { 0, 1, 2 };
    }

    void Gen(int index) {
        if (tri_dict[index] == null)
        {
            mesh.vertices = null;
            mesh.triangles = null;
            return;
        }
        Vector3[] vector3s = new Vector3[tri_dict[index].Length];
        vector3s = tri_dict[index];
        mesh.vertices = vector3s;
        mesh.triangles = int_dict[index];
        Debug.Log(mesh.vertices[0] + " " + mesh.vertices[1] + " " + mesh.vertices[2]);
        Debug.Log("FUCK");
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMesh();
    }

    void GenerateMesh() {
        for (int x = 1; x < dimensions; x++)
        {
            for (int y = 1; y < dimensions; y++)
            {
                for (int z = 1; z < dimensions; z++)
                {
                    int val = 0;
                    int i = 0;
                    for (int a = -1; a < 1; a++) {
                        for (int b = -1; b < 1; b++) {
                            for (int c = -1; c < 1; c++) {
                                val += points[(x + a) * dimensions * dimensions + (y + b) * dimensions + (z + c)] ? (int)Mathf.Pow(2, i) : 0;
                                i++;
                            }
                        }
                    }
                    Gen(val);
                    Debug.Log(val);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (previous != dimensions)
            points = new bool[dimensions * dimensions * dimensions];

        previous = dimensions;
        for (int x = 0; x < dimensions; x++) {
            for (int y = 0; y < dimensions; y++) {
                for (int z = 0; z < dimensions; z++) {
                    Gizmos.color = points[x * dimensions * dimensions + y * dimensions + z] ? Color.white: Color.black;
                    Gizmos.DrawSphere(new Vector3(x,y,z), 0.05f);
                }
            }
        }
    }
}
