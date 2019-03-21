using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool human;
    public bool spotted;
    public bool dialogueFinished;
    public int shrineCount;
    public int shrinesFound;

    private GameObject[] shrines;

    private void Start() {
        //if (GM == null) GM = this;
        //else if (GM != this) Destroy(gameObject);

        //DontDestroyOnLoad(this);

        shrines = GameObject.FindGameObjectsWithTag("Shrine");
        shrineCount = shrines.Length;

        //Debug.Log(shrineCount);
    }
}
