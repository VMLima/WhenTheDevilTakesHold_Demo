using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour {

    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        anim.SetInteger("battle", 1);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
