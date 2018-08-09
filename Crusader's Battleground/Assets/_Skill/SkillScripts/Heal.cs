using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {

    public float healAmount = 50;
    ParticleSystem h;

    public GameObject player;

    // Use this for initialization
    void Start() {
        player = this.transform.parent.gameObject;
        h = this.GetComponent<ParticleSystem>();
        StartCoroutine(heal_Coroutine());
    }

    IEnumerator heal_Coroutine() {
        if (true)//player.GetComponent<PlayerStat>().currentHp + healAmount < player.GetComponent<PlayerStat>().MaxHp)
            //player.GetComponent<PlayerStat>().currentHp += healAmount;
        //else if (player.GetComponent<PlayerStat>().currentHp + healAmount > player.GetComponent<PlayerStat>().MaxHp)
            //player.GetComponent<PlayerStat>().currentHp = player.GetComponent<PlayerStat>().MaxHp;
        while (h.isPlaying) {
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
        yield return null;
    }
	
	
}
