using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public long index;
    public List<Node> nodes = new List<Node>();

    public void AddNode(Node n)
    {
        nodes.Add(n);
    }

    public void SetIndex(long _index)
    {
        index = _index;
    }

    public void updateF_face()
    {
        // TBD
        for(int i=0; i<nodes.Count; ++i)
        {
            nodes[i].F_face = Vector3.zero;
        }
    }

}
