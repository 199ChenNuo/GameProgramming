using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Parser parser;
    public List<Node> nodes = new List<Node>();
    public List<Beam> beams = new List<Beam>();
    public List<Face> faces = new List<Face>();

    // for debug

    // draw faces
    public Material material;
    public List<Vector3> vertices = new List<Vector3>();
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

     // Start is called before the first frame update
    void Start()
    {
        parser = new Parser();
        parser.Parse();
        parser.Print();
        nodes = parser.nodes;
        beams = parser.beams;
        faces = parser.faces;

    }

    // Update is called once per frame
    void Update()
    {
        // 计算 F_axial
        for(int i=0; i<nodes.Count; ++i)
        {
            nodes[i].updateF_axial();
        }

        // 计算 F_crease
        // 计算 F_dumping
        for(int i=0; i<beams.Count; ++i)
        {
            // TBD
            beams[i].updateF_crease();
            beams[i].updateF_dumping();
        }

        // 计算 F_face
        for(int i=0; i<faces.Count; ++i)
        {
            // TBD
            faces[i].updateF_face();
        }

        // 更新Node位置及速度
        vertices.Clear();
        for (int i=0; i<nodes.Count; ++i)
        {
            nodes[i].updateVel();
            nodes[i].updatePosition();
            vertices.Add(nodes[i].position);
        }

        // 更新Beam长度
        for(int i=0; i<beams.Count; ++i)
        {
            beams[i].updateL();
        }

        // 更新Face
        // 有点问题 TBD
        // Draw();
    }

    [ContextMenu("Draw")]
    public void Draw()
    {
        // updateVertives();
        Vector2[] vertices2D = new Vector2[vertices.Count];
        Vector3[] vertices3D = new Vector3[vertices.Count];
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vertice = vertices[i] + transform.position;
            vertices2D[i] = new Vector2(vertice.x, vertice.y);
            vertices3D[i] = vertice;
        }

        Triangulator tr = new Triangulator(vertices2D);
        int[] triangles = tr.Triangulate();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices3D;
        mesh.triangles = triangles;

        if (meshRenderer == null)
        {
            meshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
        }
        meshRenderer.material = material;
        if (meshFilter == null)
        {
            meshFilter = gameObject.GetOrAddComponent<MeshFilter>();
        }
        meshFilter.mesh = mesh;
    }
}
