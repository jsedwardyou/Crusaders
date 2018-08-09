using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSkill : MonoBehaviour {

    PlayerController playerController;
    SkillInventory playerInventory;
    private int _currentIndex;

    float triggerTemp = 0;

    // Use this for initialization
    void Start () {
        playerInventory = GetComponentInParent<SkillInventory>();
        playerController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        _currentIndex = playerInventory.GetCurrentIndex();
        ActivateSkill();
        DropLoot(playerController.Check_Dead());

    }

    void ActivateSkill() {
        if (playerInventory.GetInventoryCount() == 0) {
            return;
        }

        float coolDownGauge = playerInventory.GetInventory_UIObj(_currentIndex).GetComponent<Image>().fillAmount;
        float triggerKeyValue = playerController.GetSpellTriggerKey();
        if ( triggerKeyValue > 0 && triggerTemp != triggerKeyValue && coolDownGauge == 1)
        {
            playerInventory.SetAbleToUseSkillFalse(_currentIndex);
            StartCoroutine(SkillCoolDown(playerInventory.GetInventorySkill(_currentIndex).duration, _currentIndex));
            GameObject skill = Instantiate(playerInventory.GetInventorySkill(_currentIndex).spell, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            skill.GetComponent<CastingCharacter>().player = this.gameObject;
        }
        else if (Input.GetKeyDown(playerController.GetKeyCode("dropKey")))
        {
            DropSkill(_currentIndex);
        }
        triggerTemp = triggerKeyValue;
    }

    IEnumerator SkillCoolDown(float coolDown, int index) {
        float timer = Time.fixedTime;
        GameObject skill_UI = playerInventory.GetInventory_UIObj(index);
        skill_UI.GetComponent<Image>().fillAmount = 0;
        while (skill_UI.GetComponent<Image>().fillAmount != 1) {
            skill_UI.GetComponent<Image>().fillAmount = (1 / coolDown)*(Time.fixedTime - timer);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    void DropSkill(int index) {
        Instantiate(playerInventory.Remove(index), transform.position, Quaternion.identity);      
    }

    void DropLoot(bool isDead) {
        if (isDead) {
            for (int i = playerInventory.GetInventoryCount() - 1; i >= 0; i--) {
                DropSkill(i);
            }
            GetComponent<UseSkill>().enabled = false;
        }
    }



}
