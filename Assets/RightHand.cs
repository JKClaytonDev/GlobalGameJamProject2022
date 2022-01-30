using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    public Material focus;
    public GameObject parent;
    AnimationEvent evt;
    public StickyHand sticky;
    // Start is called before the first frame update
    void Start()
    {
        evt = new AnimationEvent();
        evt.functionName = "ThrowHand";
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hit))
        {
            if (hit.transform.gameObject.GetComponent<PushPull>())
            {
                hit.transform.gameObject.GetComponent<PushPull>().highlightTime = Time.realtimeSinceStartup + 0.1f;
                hit.transform.gameObject.GetComponent<PushPull>().highlightMat = focus;
            }
        }
    }
    public void ThrowHand()
    {
        sticky.attached = false;
        RaycastHit hit;
        if (Physics.Raycast(parent.transform.position, parent.transform.forward, out hit)){
            if (hit.transform.gameObject.GetComponent<PushPull>())
            {
                sticky.stuckTo = (hit.transform.gameObject);
                sticky.pull = hit.transform.gameObject.GetComponent<PushPull>().pull;
                Debug.Log("HIT");
                sticky.transform.parent = null;
                sticky.transform.position = hit.point;
            }
        }
    }
}
