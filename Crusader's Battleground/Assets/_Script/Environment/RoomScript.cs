using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {

    float random;
    public float spawnBoxChance = 30;

    public GameObject box;
    public GameObject boxPos;

    // Use this for initialization
    void Start() {
        boxPos = this.transform.GetChild(0).gameObject;
        random = Random.Range(0, 100);
        if (random < spawnBoxChance) {
            Instantiate(box, boxPos.transform.position, Quaternion.identity);
        }

	}
	

}
