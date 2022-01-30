using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    AnimationEvent evt;
    // Start is called before the first frame update
    void Start()
    {
        evt = new AnimationEvent();
        evt.functionName = "Slap";
    }

    public void slap()
    {
        foreach (Collider c in Physics.OverlapSphere(transform.position, 2))
        {
            if (c.GetComponent<enemyScript>())
            {
                c.GetComponent<enemyScript>().kill();
            }
        }
    }
}
