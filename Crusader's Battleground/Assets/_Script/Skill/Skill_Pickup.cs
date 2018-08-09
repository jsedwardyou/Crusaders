using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Pickup : MonoBehaviour {

    public GameObject skill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            SkillStorage skillStorage = collision.transform.parent.GetChild(1).GetComponent<SkillStorage>();
            int numOfSkill = skillStorage.skill.Length;
            StartCoroutine(AttainSkill(skillStorage, numOfSkill));
            
        }
    }

    IEnumerator AttainSkill(SkillStorage skillStorage, int SkillNum) {
        for (int i = 0; i < SkillNum; i++) {
            if (skillStorage.skill[i] == null) {
                skillStorage.skill[i] = skill;
                Destroy(this.gameObject);
                yield break;
            }
        }
        yield return null;
    }

}
