using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    public Renderer m;
    public Material basicMat;
    public Material highlightMat;
    public bool pull;
    public float highlightTime;
    
    private void Start()
    {
        if (!m)
        m = GetComponent<Renderer>();
        basicMat = m.material;
    }
    private void Update()
    {
        m.material = basicMat;
        if (Time.realtimeSinceStartup < highlightTime)
            m.material = highlightMat;
    }

}
