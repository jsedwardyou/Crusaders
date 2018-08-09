using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    Vector3 MapPos1 = new Vector3(0, 0, -5);
    Vector3 MapPos2 = new Vector3(110, -1.82f, -5);

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
