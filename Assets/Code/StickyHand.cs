using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StickyHand : MonoBehaviour
{
    
    public GameObject stuckTo;
    Vector3 lastStick;
    public Rigidbody playerRB;
    public GameObject player;
    public GameObject parent;
    public GameObject underParent;
    public LineRenderer l;
    public bool attached;
    public bool pull;
    Vector3 localPos;
    // Start is called before the first frame update
    void Start()
    {
        pull = true;
        attached = true;
    }

    // Update is called once per frame
    void Update()
    {
        l.SetPosition(0, transform.position);
        l.SetPosition(1, underParent.transform.position);
        player.GetComponent<PlayerMovement>().flying = false;
        if (attached)
        {
            transform.parent = parent.transform;
            pull = true;
            transform.localPosition = new Vector3();
            transform.position = parent.transform.position;
            transform.localEulerAngles = new Vector3(0, 180, 0);
            localPos = Vector3.MoveTowards(localPos, playerRB.velocity / 255, Time.deltaTime);
            transform.position += localPos;
            lastStick = new Vector3();
            if (stuckTo.GetComponent<NavMeshAgent>())
            {
                stuckTo.GetComponent<NavMeshAgent>().enabled = true;
                stuckTo.GetComponent<enemyScript>().recalculate();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 5)
                player.GetComponent<PlayerMovement>().flying = true;
            if (Vector3.Distance(transform.position, player.transform.position) > 2)
            player.transform.LookAt(transform.position);
            if (pull)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3();
                player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.deltaTime * 75);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, parent.transform.position, Time.deltaTime * 25);
                if (lastStick != new Vector3())
                stuckTo.GetComponent<Rigidbody>().velocity = (transform.position - lastStick)/Time.deltaTime;
                if (stuckTo.GetComponent<NavMeshAgent>())
                    stuckTo.GetComponent<NavMeshAgent>().enabled = false;
                lastStick = transform.position;
            }
            
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {
                
                pull = false;
            }
            if (Vector3.Distance(transform.position, parent.transform.position) < 0.1f)
            {

                attached = true;
            }
        }
        
    }
}
