using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public long index = new long();

    public List<Beam> beams = new List<Beam>();

    public Vector3 F_axial = new Vector3();
    public Vector3 F_crease = new Vector3();
    public Vector3 F_face = new Vector3();
    public Vector3 F_dumping = new Vector3();

    public Vector3 vel = new Vector3();
    public Vector3 position = new Vector3();
    public float mass = 1f;
    public float deltaT = 0.1f;

    // for debug
    public GameObject sphere;
    public GameObject lineobj;
    public LineRenderer line;


    public void AddBeam(Beam beam)
    {
        beams.Add(beam);
    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
        vel = Vector3.zero;

        // for debug
        // node visualization
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        sphere.transform.position = pos;

        // for debug
        // beam visualization
        lineobj = new GameObject();
        line = lineobj.AddComponent<LineRenderer>();
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
    }

    public void SetIndex(int i)
    {
        index = i;
    }

    public void updateF_axial()
    {
        this.F_axial = new Vector3(0, 0, 0);
        for(int i=0; i<beams.Count; ++i)
        {
            Beam b = beams[i];
            this.F_axial += b.getF(this);
        }
    }

    public void updateVel()
    {
        Vector3 F = F_axial + F_crease + F_dumping + F_face;
        Vector3 a = F / mass;
        vel += a * deltaT;
    }

    public void updatePosition()
    {
        position += vel * deltaT;

        // update {node, beam} visualization
        sphere.transform.position = position;

        line.positionCount = beams.Count * 2;
        for(int i=0; i<beams.Count; ++i)
        {
            line.SetPosition(i * 2, position);
            line.SetPosition(i * 2 + 1, beams[i].getOther(this).position);
        }
    }
}
