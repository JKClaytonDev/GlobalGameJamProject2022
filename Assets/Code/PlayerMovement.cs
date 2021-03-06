using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    float mSens;
    float yVel;
    public AudioSource source;
    public AudioClip slapSound;
    public bool flying;
    public StickyHand s;
    public float groundDistance;
    public bool onGround;
    public LayerMask groundLayers;
    public Animator leftHand;
    public Animator rightHand;
    public float health = 100;
    public Image healthImage;
    Rigidbody rb;
    public void playSlapSound()
    {
        source.PlayOneShot(slapSound);
    }
    public void takeDamage(int Damage)
    {
        health -= Damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mSens = 1;
        if (PlayerPrefs.HasKey("Sens") && PlayerPrefs.GetFloat("Sens") != 0)
            mSens = PlayerPrefs.GetFloat("Sens");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mSens /= 1.2f;
            PlayerPrefs.SetFloat("Sens", mSens);
            PlayerPrefs.Save();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mSens *= 1.2f;
            PlayerPrefs.SetFloat("Sens", mSens);
            PlayerPrefs.Save();
        }
        Color c = Color.red;
        c.a = (100 - health)/100;
        healthImage.color = c;
        if (health < 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        health += Time.deltaTime * 20;
        if (health > 100)
            health = 100;
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, Time.deltaTime*3);
        onGround = (Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayers));
        if (Input.GetMouseButtonDown(0))
            leftHand.Play("LeftHandSlap");
        if (Input.GetMouseButtonDown(1))
            rightHand.Play("RightHandPull");
        Vector3 oldAngles = transform.localEulerAngles;
        Vector3 newAngles = transform.localEulerAngles;
        newAngles.x = 0;
        newAngles.z = 0;
        transform.localEulerAngles = newAngles;
        
        Vector3 vel = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * 15;
        if (flying)
            vel = new Vector3();
        vel.y = rb.velocity.y;
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            vel.y = 5;
        }
        if (vel.y > 5)
            vel.y = 5;
        rb.velocity = vel;
        transform.localEulerAngles = oldAngles+(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)*mSens);
    }
}
