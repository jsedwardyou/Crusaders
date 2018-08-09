using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public GameObject skillStorage;
    SkillStorage skillPool;

    public float boxHp = 3;
    public GameObject destroyedBox;

    int SkillToSpawnNum;
    int SkillToSpawnIndex;

	// Use this for initialization
	void Start () {
        skillStorage = GameObject.Find("SkillManager");
        skillPool = skillStorage.GetComponent<SkillStorage>();
        
        SkillToSpawnNum = Random.Range(0, 5);

    }
	
	// Update is called once per frame
	void Update () {
        if (boxHp <= 0) {
            Destroyed();
        }
	}

    void Destroyed() {
        Instantiate(destroyedBox, transform.position, Quaternion.identity);
        for (int i = 0; i < SkillToSpawnNum; i++) {
            SkillToSpawnIndex = Random.Range(0, skillPool.skill.Length);
            Instantiate(skillPool.skill[SkillToSpawnIndex], transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }




}
