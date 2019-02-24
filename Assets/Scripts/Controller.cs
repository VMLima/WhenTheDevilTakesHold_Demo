using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float speed;
    public float mouseSpeed;
    public float jumpSpeed;
    public float maxSpeed;

    //public Transform leave;

    private bool isGrounded = true;

    private Vector3 movement;
    private Vector3 rotation;
    private Rigidbody rig;
    public Camera cam;

    private float y;
    private float x;

    void Start() {
        rig = GetComponent<Rigidbody>();
        rig.maxAngularVelocity = mouseSpeed;
    }

    void FixedUpdate() {

        // Handles movement
        if (Input.GetKey(KeyCode.W)) {
            movement = transform.forward * speed;
        } else if (Input.GetKey(KeyCode.A)) {
            movement = -transform.right * speed;
        } else if (Input.GetKey(KeyCode.S) ) {
            movement = -transform.forward * speed;
        } else if (Input.GetKey(KeyCode.D)) {
            movement = transform.right * speed;
        } else {
            movement = Vector3.zero;
        }

        if (rig.velocity.magnitude > maxSpeed) {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }

        movement.y = rig.velocity.y;
        rig.position += movement * Time.deltaTime;

        //Handles mouse rotation
        //if (!camSwapped) {
        //y -= Input.GetAxis("Mouse Y") * mouseSpeed;
        x += Input.GetAxis("Mouse X") * mouseSpeed;
        //cam.transform.position = Quaternion.AngleAxis(x, Vector3.up);
        transform.eulerAngles = new Vector3(0, x, 0);
        //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * mouseSpeed, Vector3.up) * offset;
        //Vector3 normOffset = Vector3.Normalize(offset);
        //normOffset.y = 4;
        //normOffset.x *= 4;
        //normOffset.z *= 4;
        //cam.transform.position = transform.position + normOffset;
        //cam.transform.LookAt(transform.position);
        //}

        // Handles jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            rig.velocity = transform.up * jumpSpeed;
            isGrounded = false;
        }

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        //if (other.CompareTag("Exit")) {
        //    leave.gameObject.SetActive(true);
        //}
    }
}