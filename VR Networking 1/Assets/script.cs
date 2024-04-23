using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[1];
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "taggy")
        {
            rend.sharedMaterial = material[2];
        }
    }
}
