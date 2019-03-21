using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Transform player;
    public GameObject lowerLevel;
    public GameObject upperLevel;

    private GameObject[] particles;

    private void Start() {
        particles = GameObject.FindGameObjectsWithTag("Light");
    }

    // Update is called once per frame
    void Update() {
        if (player.position.y < -11) {
            lowerLevel.SetActive(true);
            upperLevel.SetActive(false);
        } else {
            lowerLevel.SetActive(false);
            upperLevel.SetActive(true);
        }
        //print(particles.Length);
        for (int i = 0; i < particles.Length; i++) {
            if (Vector3.Distance(player.position, particles[i].GetComponentInParent<Transform>().position) > 30) {
                particles[i].GetComponent<ParticleSystem>().Stop();
            } else {
                particles[i].GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
