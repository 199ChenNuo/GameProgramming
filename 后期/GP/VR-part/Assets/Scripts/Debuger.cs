using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Debuger 
{
    // Debug FOLD
    public void PrintCords(List<Vector3> verts)
    {
        for (int i = 0; i < verts.Count; i++)
        {
            Vector3 v = verts[i];
            Debug.Log(v.x.ToString() + ", " + v.y.ToString() + ", " + v.z.ToString());
        }
    }
    public void PrintFaceVerts(List<Vector3Int> verts)
    {
        for (int i = 0; i < verts.Count; i++)
        {
            Vector3 v = verts[i];
            Debug.Log(v.x.ToString() + ", " + v.y.ToString() + ", " + v.z.ToString());
        }
    }
    public void PrintEdgeVerts(List<Vector2Int> eVerts)
    {
        for (int i = 0; i < eVerts.Count; i++)
        {
            Vector2Int v = eVerts[i];
            Debug.Log(v.x.ToString() + ", " + v.y.ToString());
        }
    }

    // Debug Node
    public void PrintNodes(List<Node> nodes)
    {
        foreach(Node node in nodes)
        {
            string log = "Node " + node.index + "\n";
            log += "\tpositon " + node.position.ToString() + "\n";
            log += "\tbeams:";
            foreach(Beam b in node.beams)
            {
                log += b.index.ToString() + ", ";
            }
            log += "\n";
            Debug.Log(log);
        }
    }

    // Debug Beam
    public void PrintBeams(List<Beam> beams)
    {
        string[] types = new string[5] { "Border", "Mountain", "Valley", "Facet", "Unknown" };
        foreach (Beam beam in beams)
        {
            string log = "Beam " + beam.index + "\n";
            log += "\tfrom [Node " + beam.n1.index + "] to [Node " + beam.n2.index + "]\n";
            log += "\tangle: " + beam.angle.ToString();
            log += "\ttype: " + types[(int)beam.type];            
            log += "\n";
            Debug.Log(log);
        }
    }

    // Debug Face
    public void PrintFaces(List<Face> faces)
    {
        string[] types = new string[5] { "Border", "Mountain", "Valley", "Facet", "Unknown" };
        foreach (Face face in faces)
        {
            string log = "Face " + face.index + "\n";
            log += "\t nodes:";
            foreach(Node n in face.nodes)
            {
                log += n.index + ", ";
            }
            log += "\n";
            Debug.Log(log);
        }
    }

}
