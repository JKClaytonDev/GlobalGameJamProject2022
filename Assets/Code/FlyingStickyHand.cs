using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingStickyHand : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<PlayerMovement>().rightHand.gameObject.SetActive(false);
    }
    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<PlayerMovement>().rightHand.gameObject.SetActive(true);
        door.SetActive(true);
        Destroy(gameObject);
    }
}
