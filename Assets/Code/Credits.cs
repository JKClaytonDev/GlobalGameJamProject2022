using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > 10)
            SceneManager.LoadScene("Menu");
    }
}