using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Story : MonoBehaviour
{
    public int frame;
    public Image i;
    public Text t;
    public Sprite[] sprites;
    public string[] text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i.sprite = sprites[frame];
        t.text = text[frame];
        if (Input.GetKeyDown(KeyCode.Space))
            frame++;
        if (frame >= text.Length)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
