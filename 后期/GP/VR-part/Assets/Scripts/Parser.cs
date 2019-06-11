using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public string filename = "../Resources/FOLD/flappingBird.fold";
    public List<Node> nodes = new List<Node>();
    public List<Beam> beams = new List<Beam>();
    public List<Face> faces = new List<Face>();

    private FOLD fold;
    private Debuger debuger = new Debuger();

    // Start is called before the first frame update
     public void Parse()
    {
        // Step 1: .fold -> FOLD
        fold = new FOLD();
        fold.Parse(filename);

        // Step 2: FOLD -> Node/Beam/Face

        // parse Nodes
        List<Vector3> verts = fold.vertices_coords;
        for (int i = 0; i < verts.Count; i++)
        {
            Vector3 coord = verts[i];
            Node node = new Node();
            node.SetIndex(i);
            node.SetPosition(coord);

            nodes.Add(node);
        }

        // parse Beams
        List<Vector2Int> edges_verts = fold.edges_verts;
        List<string> edges_types = fold.edges_types;
        List<float> edges_foldAngles = fold.edges_foldAngles;
        for(int i = 0; i < edges_verts.Count; i++)
        {
            Beam beam = new Beam();
            beam.SetIndex(i);
            beam.SetType(edges_types[i]);
            beam.SetAngel(edges_foldAngles[i]);
            
            int n1 = edges_verts[i].x;
            int n2 = edges_verts[i].y;
            beam.SetNode1(nodes[n1]);
            beam.SetNode2(nodes[n2]);
            beam.SetL(Vector3.Distance(nodes[n1].position, nodes[n2].position));
            beam.SetL_0(beam.l);
            nodes[n1].AddBeam(beam);
            nodes[n2].AddBeam(beam);

            beams.Add(beam);
        }

        // parse Faces
        List<Vector3Int> faces_verts = fold.faces_verts;
        for(int i = 0; i < faces_verts.Count; i++)
        {
            Face face = new Face();
            face.SetIndex(i);
            Vector3Int ids = faces_verts[i];
            face.AddNode(nodes[ids.x]);
            face.AddNode(nodes[ids.y]);
            face.AddNode(nodes[ids.z]);

            faces.Add(face);
        }

        Debug.Log("parse Success!");
    }

    public void Print()
    {
        debuger.PrintNodes(nodes);
        debuger.PrintBeams(beams);
        debuger.PrintFaces(faces);
    }

}
