using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class grid_test : MonoBehaviour
{
    private int previous = 0;
    public int dimensions = 2;
    public bool[] points;
    private Mesh mesh;
    private MeshRenderer mesh_rend;
    private Vector3[][] tri_dict = new Vector3[256][];
    private int[][] int_dict = new int[256][];

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

        tri_dict[2] = new Vector3[] { v_z, v_zy, v_zx };
        int_dict[2] = new int[] { 0, 1, 2 };

        tri_dict[3] = new Vector3[] { v_x, v_y, v_zy, v_zx };
        int_dict[3] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[4] = new Vector3[] { v_y, v_yx, v_yz };
        int_dict[4] = new int[] { 0, 1, 2 };

        tri_dict[5] = new Vector3[] { v_x, v_yx, v_yz, v_z };
        int_dict[5] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[6] = new Vector3[] { v_y, v_yx, v_zx, v_z, v_yz, v_zy };
        int_dict[6] = new int[] { 0, 1, 2, 0, 2, 3, 1, 4, 5, 1, 5, 2 };

        tri_dict[7] = new Vector3[] { v_x, v_yx, v_zx, v_yz, v_zy };
        int_dict[7] = new int[] { 0, 1, 2, 1, 3, 4, 1, 4, 2 };

        tri_dict[8] = new Vector3[] { v_zy, v_yz, v_zyx };
        int_dict[8] = new int[] { 0, 1, 2 };

        tri_dict[9] = new Vector3[] { v_x, v_y, v_yz, v_z, v_zy, v_zyx };
        int_dict[9] = new int[] { 0, 1, 2, 0, 2, 5, 3, 0, 5, 3, 5, 4 };

        tri_dict[10] = new Vector3[] { v_z, v_yz, v_zyx, v_zx };
        int_dict[10] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[11] = new Vector3[] { v_x, v_y, v_yz, v_zx, v_zyx };
        int_dict[11] = new int[] { 0, 1, 2, 0, 2, 4, 0, 4, 3 };

        tri_dict[12] = new Vector3[] { v_y, v_yx, v_zyx, v_zy };
        int_dict[12] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[13] = new Vector3[] { v_x, v_yx, v_zyx, v_z, v_zy };
        int_dict[13] = new int[] { 0, 1, 2, 0, 2, 4, 0, 4, 3 };

        tri_dict[14] = new Vector3[] { v_z, v_y, v_yx, v_zx, v_zyx };
        int_dict[14] = new int[] { 0, 1, 2, 0, 2, 3, 3, 2, 4 };

        tri_dict[15] = new Vector3[] { v_x, v_yx, v_zyx, v_zx };
        int_dict[15] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[16] = new Vector3[] {v_x, v_xz, v_xy};
        int_dict[16] = new int[] { 0, 1, 2 };

        tri_dict[17] = new Vector3[] { v_xz, v_xy, v_y, v_z };
        int_dict[17] = new int[] { 0, 1, 2, 0, 2, 3 };

        tri_dict[18] = new Vector3[] { v_z, v_zy, v_xy, v_x, v_zx, v_xz };
        int_dict[18] = new int[] { 0, 1, 2, 0, 2, 3, 5, 2, 1, 5, 1, 4 };

        tri_dict[19] = new Vector3[] { v_y, v_xy, v_zy, v_xz, v_zx };
        int_dict[19] = new int[] { 0, 2, 1, 3, 1, 2, 3, 2, 4 };

        tri_dict[20] = new Vector3[] {v_x, v_xz, v_yz, v_y, v_xy, v_yx};
        int_dict[20] = new int[] { 0, 1, 2, 0, 2, 3, 5, 2, 1, 5, 1, 4 };

        tri_dict[21] = new Vector3[] { v_z, v_xz, v_yz, v_yx, v_xy };
        int_dict[21] = new int[] { 0, 1, 2, 3, 2, 4, 2, 1, 4 };

        tri_dict[22] = new Vector3[] { v_xz, v_yx, v_yz, v_zx, v_z, v_y, v_x,  v_xy, v_zy};
        int_dict[22] = new int[] { 0, 1, 2, 0, 2, 3, 4, 5, 6,  7, 1, 0, 8, 3, 2};

        tri_dict[23] = new Vector3[] { v_xz, v_xy, v_yx, v_yz, v_zy, v_zx };
        int_dict[23] = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 5, 3, 4, 5};

        tri_dict[24] = new Vector3[] { v_x, v_zy, v_yz, v_xy, v_xz, v_zyx };
        int_dict[24] = new int[] { 0, 1, 2, 0, 2, 3, 3, 2, 5, 3, 5, 4, 4, 5, 1, 4, 1, 0};

        tri_dict[25] = new Vector3[] { v_xy, v_xz, v_z, v_zy, v_yz, v_zyx, v_y };
        int_dict[25] = new int[] { 0, 5, 1, 0, 4, 5, 0, 6, 4, 1, 5, 3, 1, 3, 2};

        tri_dict[26] = new Vector3[] {v_x, v_xz, v_zyx, v_zx, v_z, v_yz, v_xy};
        int_dict[26] = new int[] { 0, 4, 5, 0, 5, 6, 6, 5, 2, 1, 6, 2, 1, 2, 3 };

        tri_dict[27] = new Vector3[] { v_y, v_xy, v_xz, v_yz, v_zyx, v_zx };
        int_dict[27] = new int[] { 0, 3, 1, 1, 3, 4, 1, 4, 2, 2, 4, 5 };

        tri_dict[28] = new Vector3[] { v_xy, v_yx, v_zyx, v_xz, v_zy, v_x, v_y};
        int_dict[28] = new int[] { 0, 1, 2, 0, 2, 3, 3, 2, 4, 3, 4, 5, 5, 4, 6};

        tri_dict[29] = new Vector3[] { v_xy, v_yx, v_zyx, v_xz, v_zy, v_z };
        int_dict[29] = new int[] { 0, 1, 2, 0, 2, 3, 3, 2, 4, 3, 4, 5,};

        tri_dict[30] = new Vector3[] { v_xy, v_yx, v_zyx, v_xz, v_zx, v_x, v_y, v_z};
        int_dict[30] = new int[] { 0, 1, 2, 0, 2, 3, 3, 2, 4, 5, 7, 6};

        tri_dict[31] = new Vector3[] { v_xy, v_yx, v_zyx, v_xz, v_zx };
        int_dict[31] = new int[] { 0, 1, 2, 0, 2, 3, 3, 2, 4};

        // THIS IS TOO MANY

        tri_dict[32] = new Vector3[] { v_xz, v_zx, v_xzy };
        int_dict[32] = new int[] { 0, 1, 2 };

        tri_dict[64] = new Vector3[] { v_xy, v_xyz, v_yx };
        int_dict[64] = new int[] { 0, 1, 2 };

        tri_dict[128] = new Vector3[] { v_xzy, v_zyx, v_xyz };
        int_dict[128] = new int[] { 0, 1, 2 };


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

        Handles.Label(v_x, "x");
        Handles.Label(v_xy, "xy");
        Handles.Label(v_xz, "xz");
        Handles.Label(v_z, "z");
        Handles.Label(v_zx, "zx");
        Handles.Label(v_zy, "zy");
        Handles.Label(v_y, "y");
        Handles.Label(v_yx, "yx");
        Handles.Label(v_yz, "yz");
        Handles.Label(v_xzy, "xzy");
        Handles.Label(v_xyz, "xyz");
        Handles.Label(v_zyx, "zyx");

    }
}
