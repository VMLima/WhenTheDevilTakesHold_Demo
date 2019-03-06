using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float speed;
    public float mouseSpeed;
    public float jumpSpeed;

    private bool isGrounded = true;
    private bool crouched;

    private Vector3 movement;
    private Rigidbody rig;
    public Camera cam;

    private float InputY;
    private float InputX;
    private float MouseY;
    private float MouseX;


    void Start() {
        rig = GetComponent<Rigidbody>();
        rig.maxAngularVelocity = mouseSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate() {

        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");

        movement = (InputX * transform.right + InputY * transform.forward).normalized * speed;

        transform.position += movement * Time.deltaTime;

        //Handles mouse rotation
        MouseX += Input.GetAxis("Mouse X") * mouseSpeed;
        MouseY += Input.GetAxis("Mouse Y") * mouseSpeed;

        transform.eulerAngles = new Vector3(0, MouseX, 0);
        cam.transform.eulerAngles = new Vector3(Mathf.Clamp(-MouseY, -90, 90), MouseX, 0);

        // Handles jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            rig.velocity = transform.up * jumpSpeed;
            isGrounded = false;
        }

        // Crouching 
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            cam.transform.localPosition = new Vector3(0, 0, 0);
            crouched = true;
        } else if (Input.GetKeyUp(KeyCode.LeftControl)){
            cam.transform.localPosition = new Vector3(0, 0.75f, 0);
            crouched = false;
        }

        // Reveals cursor
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }
}