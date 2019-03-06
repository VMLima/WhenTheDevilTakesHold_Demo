using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour {

    public GameObject player;
    private bool interacting;
    private ParticleSystem[] particles;

    void Start() {
        particles = transform.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) interacting = true;
        else interacting = false;
    }

    public void ActivateShrine() {
        if (interacting) {
            for (int i = 0; i < particles.Length; i++) {
                particles[i].Play();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            ActivateShrine();
        }
    }
}
