using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform player;
    public GameObject lowerLevel;
    public GameObject upperLevel;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (player.position.y < -5) {
            lowerLevel.SetActive(true);
            upperLevel.SetActive(false);
        } else {
            lowerLevel.SetActive(false);
            upperLevel.SetActive(true);
        }
    }
}
