using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class enemyScript : MonoBehaviour
{
    bool activated;
    float checkTime;
    int targetMode;
    NavMeshAgent n;
    GameObject player;
    float targetTime;
    public GameObject ragdoll;
    Animator anim;
    AnimationEvent evt;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        targetMode = 1;
        player = Camera.main.gameObject;
        n = GetComponent<NavMeshAgent>();
    }
    public void Shoot()
    {
        RaycastHit h2;
        transform.LookAt(player.transform);
        Physics.Raycast(transform.position, transform.forward, out h2);
        if (h2.transform.gameObject == player && h2.distance < 55)
        {
            player.GetComponent<PlayerMovement>().takeDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated && Time.realtimeSinceStartup > checkTime)
        {
            anim.SetBool("Shooting", false);
            checkTime += Random.Range(0.1f, 1f);
            RaycastHit h2;
            transform.LookAt(player.transform);
            Physics.Raycast(transform.position, transform.forward, out h2);
            n.SetDestination(transform.position);
            if (h2.transform.gameObject == player && h2.distance < 55)
            {
                activated = true;
                Vector3 randomDirection = Random.insideUnitSphere * 25;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, 25, 1);
                Vector3 finalPosition = hit.position;
                n.SetDestination(finalPosition);
            }
            return;

        }
        else if (Time.realtimeSinceStartup > targetTime)
        {
            if (targetMode == 1)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 25;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, 25, 1);
                Vector3 finalPosition = hit.position;
                n.SetDestination(finalPosition);
                
                anim.speed = Random.Range(0.7f, 1.3f);
            }
            targetTime = Time.realtimeSinceStartup + Random.Range(3, 6);
        }
        n.speed = anim.speed * 5;
        anim.SetBool("Shooting", false);
        if (n.remainingDistance < 5)
        {
            anim.SetBool("Shooting", true);
            transform.LookAt(player.transform);
            n.speed = 0;
        }
        
        
    }
    public void kill()
    {
        player.GetComponent<PlayerMovement>().playSlapSound();
        Destroy(gameObject);
        GameObject c = Instantiate(ragdoll);
        c.transform.position = transform.position;
        c.transform.eulerAngles = transform.eulerAngles;
        Time.timeScale = 0.1f;
        c.GetComponent<Rigidbody>().velocity = (Camera.main.transform.forward * 2 + Camera.main.transform.right * 5) * 25;
        Camera.main.transform.LookAt(transform);
    }
}
