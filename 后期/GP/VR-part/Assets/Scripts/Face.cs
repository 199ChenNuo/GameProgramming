using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    // ==============================================
    // attributes
    // ==============================================

    // index of this face
    public long index;

    // stiffness of face constraint
    public float k_face = 0.2f;

    // nodes of this face
    //      normally, a face has 3 nodes
    //      if not, that face is boundary face, or not completed currently
    public List<Node> nodes = new List<Node>();

    // nominal angle of three alpha (in the flat state)
    //      the order should related to nodes[]
    public List<float> alpha_0s = new List<float>();

    // alphas of this face (in dynamic state)
    //      the order should related to nodes[]
    public List<float> alphas = new List<float>();

    // normal vector of this face
    public Vector3 n;

    // ==============================================
    // functions: called by EventController
    //              update F_face and F_dumping for nodes
    // ==============================================
    public void updateF_face()
    {
        // boundary face, return
        if (nodes.Count != 3)
            return;

        // update 3 current alpha of this face
        updateAlpha();
        // update normal vector of this face
        updateN();
        // remove previous f_face of nodes
        clearF_face();

        for(int i=0; i<3; ++i)
        {
            // notice that alpha has +/-

            // alpha: the angle from (p2-p1) to (p2-p3)
            // e.g. 
            //     p1: nodes[(i+2)%3].pos
            //     p2: nodes[i].pos
            //     p3: nodes[(i+1)%3].pos 

            // f_face for p1
            float k = -k_face * (alphas[i] - alpha_0s[i]);
            nodes[(i + 2) % 3].F_face += k * getAlphaODE1(n, nodes[(i + 2) % 3].position, nodes[i].position);
            // f_face for p2
            nodes[i].F_face += k * getAlphaODE2(n, nodes[(i + 2) % 3].position,
                                                                        nodes[i].position, nodes[(i + 1) % 3].position);
            // f_face for p3
            nodes[(i + 1) % 3].F_face += k * getAlphaODE3(n, nodes[i].position, nodes[(i + 1) % 3].position);
        }
    }

    // ==============================================
    // functions: set up face
    // ==============================================
    public void AddNode(Node n)
    {
        nodes.Add(n);
    }

    public void SetIndex(long _index)
    {
        index = _index;
    }

    // calculate initial alpha_0s of this face
    public void SetAlpha_0()
    {
        if (nodes.Count != 3)
            return;
        for (int i = 0; i < 3; ++i)
        {
            // n1: p1 -> p2
            Vector3 n1 = nodes[i].position - nodes[(i + 2) % 3].position;
            // n2: p3 -> p2
            Vector3 n2 = nodes[i].position - nodes[(i + 1) % 3].position;

            /*
            Debug.Log("n1: " + n1.ToString());
            Debug.Log("n2: " + n2.ToString());
            Debug.Log("angle: " + Vector3.Angle(n1, n2));
            */

            alpha_0s.Add(Vector3.Angle(n1, n2));
            alphas.Add(alpha_0s[i]);

            /*
            Debug.Log("alpha0_s: " + alpha_0s.ToString());
            */
        }
    }

    // ==============================================
    // functions: utils that helps doing calculate
    // ==============================================

    // update alphas every frame
    //      according to nodes position
    public void updateAlpha()
    {
        if (nodes.Count != 3)
            return;
        for(int i=0; i<3; ++i)
        {
            // n1: p1 -> p2
            Vector3 n1 = nodes[i].position - nodes[(i + 2) % 3].position;
            // n2: p3 -> p2
            Vector3 n2 = nodes[i].position - nodes[(i + 1) % 3].position;
            // alpha[i]: angle from n1 point to n2
            alphas[i] = Vector3.Angle(n1, n2);
        }
    }

    // update nromal vector of this face
    public void updateN()
    {
        if (nodes.Count != 3)
            return;
        Vector3 n1 = nodes[0].position - nodes[1].position;
        Vector3 n2 = nodes[0].position - nodes[2].position;
        n = Vector3.Cross(n1, n2);
    }

    // set nodes's f_face to 0
    public void clearF_face()
    {
        if (nodes.Count != 3)
            return;
        for(int i=0; i<3; ++i)
        {
            nodes[i].F_face = Vector3.zero;
        }
    }

    // p1 and alpha2_31
    public Vector3 getAlphaODE1(Vector3 n, Vector3 p1, Vector3 p2)
    {
        Vector3 numerator = Vector3.Cross(n, p1 - p2);
        float denominator = Vector3.Distance(p1, p2);
        return numerator / (denominator * denominator);
    }

    // p2 and alpha2_31
    public Vector3 getAlphaODE2(Vector3 n, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 numerator1 = Vector3.Cross(n, p1 - p2);
        float denominator1 = Vector3.Distance(p1, p2);
        Vector3 numerator2 = Vector3.Cross(n, p3 - p2);
        float denominator2 = Vector3.Distance(p3, p2);
        return -numerator1 / (denominator1 * denominator1) + numerator2 / (denominator2 * denominator2);
    }

    // p3 and alpha2_31
    public Vector3 getAlphaODE3(Vector3 n, Vector3 p2, Vector3 p3)
    {
        Vector3 numerator = Vector3.Cross(n, p3 - p2);
        float denominator = Vector3.Distance(p3, p2);
        return -numerator / (denominator * denominator);
    }

}
