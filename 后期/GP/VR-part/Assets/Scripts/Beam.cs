using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public enum Type { Border, Mountain, Valley, Facet, Unknown }
    public long index;
    public float k_axial = 0.7f;

    public Node n1 = new Node();
    public Node n2 = new Node();
    public Type type;
    public float angle;

    // length
    public float l;
    public float l_0;


    public void SetNode1(Node _n1)
    {
        n1 = _n1;
       //  _n1.addBeam(this);
    }

    public Node getOther(Node n)
    {
        if (n == n1)
            return n2;
        return n1;
    }

    public void SetNode2(Node _n2)
    {
        n2 = _n2;
        // _n2.addBeam(this);
    }

    public void SetNode(Node n){
        if(n1 == null)
            n1 = n;
        else
            n2 = n;
    }

    public void SetL(float _l)
    {
        l = _l;
    }

    public void SetL_0(float _l_0)
    {
        l_0 = _l_0;
    }

    public void SetIndex(long i)
    {
        index = i;
    }

        public void SetAngel(float a)
    {
        angle = a;
    }

    public void SetType(string s)
    {
        if (s == "\"B\"")
        {
            type = Type.Border;
        }
        else if (s == "\"M\"")
        {
            type = Type.Mountain;
        }
        else if (s == "\"V\"")
        {
            type = Type.Valley;
        }
        else if (s == "\"F\"")
        {
            type = Type.Facet;
        }
        else
        {
            type = Type.Unknown;
        }
    }

    public Vector3 getF(Node n)
    {
        Vector3 I12 = (n1.position - n2.position).normalized;
        if (n == n1)
        {
            I12 = (n2.position - n1.position).normalized;
        }
       
        return -k_axial * (l - l_0) * I12;
    }

    public void updateF_crease()
    {
        // TBD
        n1.F_crease = Vector3.zero;
        n2.F_crease = Vector3.zero;
    }

    public void updateF_dumping()
    {
        // TBD
        n1.F_dumping = Vector3.zero;
        n2.F_dumping = Vector3.zero;
    }

    public void updateL()
    {
        l = Vector3.Distance(n1.position, n2.position);
    }


}
