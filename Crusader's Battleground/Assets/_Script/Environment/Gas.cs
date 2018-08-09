using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour {

    float damage = 0.1f;
    bool ActivateGas = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(ActivateGas)
        transform.localScale += new Vector3(0, 0.2f, 0.2f) * Time.deltaTime;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "root")
        {
            //collision.transform.parent.GetComponent<PlayerStat>().currentHp -= damage;
        }
        else if (collision.tag == "ground") {
            ActivateGas = false;
        }
    }
}
