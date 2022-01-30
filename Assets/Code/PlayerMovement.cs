using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundDistance;
    public bool onGround;
    public LayerMask groundLayers;
    public Animator leftHand;
    public Animator rightHand;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 vel = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * 15; ;
        vel.y = rb.velocity.y;
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            vel.y = 10;
        }
        rb.velocity = vel;
        transform.localEulerAngles = oldAngles+new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}
