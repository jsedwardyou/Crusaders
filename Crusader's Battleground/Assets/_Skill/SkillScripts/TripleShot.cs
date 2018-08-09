using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour {

    public Skill skill;


    GameObject spell;
    GameObject spellEffect;
    GameObject player;

    Vector3[] BulletDirection = new Vector3[3];

    bool isFacingRight;


	// Use this for initialization
	void Start () {
        spell = skill.spell;
        spellEffect = skill.skillEffect;
        player = GetComponent<CastingCharacter>().player;
        isFacingRight = player.GetComponent<PlayerController>().GetPlayerDirection();

        if (isFacingRight) {
            BulletDirection[0]= new Vector3(1, 1,0).normalized;
            BulletDirection[1] = new Vector3(1, 0,0).normalized;
            BulletDirection[2] = new Vector3(1, -1,0).normalized;
        }
        else {
            BulletDirection[0] = new Vector3(-1, 1,0).normalized;
            BulletDirection[1] = new Vector3(-1, 0,0).normalized;
            BulletDirection[2] = new Vector3(-1, -1,0).normalized;
        }
        StartCoroutine(Coroutine_TripleShot());
    }

    IEnumerator Coroutine_TripleShot() {
        GameObject[] missiles = new GameObject[3];
        for (int i = 0; i < missiles.Length; i++) {
            if (isFacingRight)
            {
                missiles[i] = Instantiate(spellEffect, transform.position + BulletDirection[2 - i] * 5f, Quaternion.identity);
                missiles[i].GetComponent<MissileController>().player = player;
            }
            else {
                missiles[i] = Instantiate(spellEffect, transform.position + BulletDirection[i] * 5f, Quaternion.identity);
                missiles[i].GetComponent<MissileController>().player = player;
            }
        }
            missiles[0].transform.rotation = Quaternion.Euler(missiles[0].transform.rotation.eulerAngles.x, missiles[0].transform.rotation.eulerAngles.y, -30);
            missiles[2].transform.rotation = Quaternion.Euler(missiles[2].transform.rotation.eulerAngles.x, missiles[1].transform.rotation.eulerAngles.y, 30);
        yield return null;
    }





}
