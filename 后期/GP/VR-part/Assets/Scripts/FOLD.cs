using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FOLD
{
    public List<Vector3> vertices_coords;
    public List<Vector2Int> edges_verts;
    public List<string> edges_types;
    public List<Vector3Int> faces_verts;
    public List<float> edges_foldAngles;

    private StreamReader file;

    private void ParseVertCoord()
    {
        List<Vector3> verts = new List<Vector3>();
        string l;
        while ((l = file.ReadLine().Trim()) != "],")
        {
            string x_str, y_str, z_str;

            x_str = file.ReadLine().Trim().Split(',')[0];    // x
            y_str = file.ReadLine().Trim().Split(',')[0];    // y
            z_str = file.ReadLine().Trim().Split(',')[0];    // z
            file.ReadLine();    // ]

            float x, y, z;
            x = float.Parse(x_str);
            y = float.Parse(y_str);
            z = float.Parse(z_str);

            verts.Add(new Vector3(x, y, z));
        }
        vertices_coords = verts;
        
    }
    private void ParseEdgeVert()
    {
        List<Vector2Int> eVerts = new List<Vector2Int>();
        string l;
        while ((l = file.ReadLine().Trim()) != "],")
        {
            string from_str, to_str;

            from_str = file.ReadLine().Trim().Split(',')[0];    // from_idx
            to_str = file.ReadLine().Trim().Split(',')[0];    // to_idx

            file.ReadLine();    // ]

            int from, to;
            from = int.Parse(from_str);
            to = int.Parse(to_str);

            eVerts.Add(new Vector2Int(from, to));
        }
        edges_verts = eVerts;

    }
    private void ParseEdgeType()
    {
        List<string> types = new List<string>();
        string l;
        while ((l = file.ReadLine().Trim()) != "],")
        {
            string type = l.Split(',')[0];

            types.Add(type);
        }
        edges_types = types;
        
    }
    private void ParseFaceVert()
    {
        List<Vector3Int> fVerts = new List<Vector3Int>();
        string l;
        while ((l = file.ReadLine().Trim()) != "],")
        {
            string v1_str, v2_str, v3_str;

            v1_str = file.ReadLine().Trim().Split(',')[0];    // v1_idx
            v2_str = file.ReadLine().Trim().Split(',')[0];    // v2_idx
            v3_str = file.ReadLine().Trim().Split(',')[0];    // v3_idx

            file.ReadLine();    // ]

            int v1, v2, v3;
            v1 = int.Parse(v1_str);
            v2 = int.Parse(v2_str);
            v3 = int.Parse(v3_str);

            fVerts.Add(new Vector3Int(v1, v2, v3));
        }
        faces_verts = fVerts;
        
    }
    private void ParseFoldAngle()
    {
        List<float> angles = new List<float>();
        string l;
        while ((l = file.ReadLine().Trim()) != "]")
        {
            string angle_str = l.Split(',')[0];
            float angle;
            if (angle_str == "null")
            {
                angle = 0.0f;
            }
            else
            {
                angle = float.Parse(angle_str);
            }

            angles.Add(angle);
        }
        edges_foldAngles = angles;
        
    }
    public void Parse(string filename)
    {
        file = new StreamReader(filename);
        string line;
        while ((line = file.ReadLine()) != null)
        {
            line = line.Trim();
            if (line.IndexOf("vertices_coords") >= 0)
            {
                Debug.Log("============ParseVertCoord=============");
                ParseVertCoord();
                continue;
            }
            else if (line.IndexOf("edges_vertices") >= 0)
            {
                Debug.Log("============ParseEdgeVert=============");
                ParseEdgeVert();
                continue;
            }
            else if (line.IndexOf("edges_assignment") >= 0)
            {
                Debug.Log("============ParseEdgeType=============");
                ParseEdgeType();
                continue;
            }
            else if (line.IndexOf("faces_vertices") >= 0)
            {
                Debug.Log("============ParseFaceVert=============");
                ParseFaceVert();
                continue;
            }
            else if (line.IndexOf("edges_foldAngles") >= 0)
            {
                Debug.Log("============ParseFoldAngle=============");
                ParseFoldAngle();
                continue;
            }
        }
    }


}
