using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorScript : MonoBehaviour
{
    public GameObject child;
    float startTime;
    public GameObject player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        startTime = Time.realtimeSinceStartup + 2;
        
    }
    private void Update()
    {
        player.SetActive(Time.realtimeSinceStartup > startTime);
        child.SetActive(Time.realtimeSinceStartup < startTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
