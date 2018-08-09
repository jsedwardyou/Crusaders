using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {

    public GameObject Indi;

    Transform player;

	// Use this for initialization
	void Start () {
        player = this.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        UIPos(Indi, player.position + new Vector3(0, 2.5f, -0.1f));
    }

    void UIPos(GameObject obj, Vector3 pos)
    {
        obj.transform.position = pos;
    }
}
