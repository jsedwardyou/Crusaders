using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIController : MonoBehaviour {

    SkillInventory inventory;
    PlayerController playerController;

    private int currentIndex = 0;
    private int minSkillIndex;
    int maxSkillIndex;
    int availableSkillNum;

    public GameObject Skill_Indicator;

    //KeyCode
    KeyCode ToRightSkill;
    KeyCode ToLeftSkill;

	// Use this for initialization
	void Start () {
        playerController = GetComponentInParent<PlayerController>();
        inventory = GetComponentInParent<SkillInventory>();
        minSkillIndex = 0;
        currentIndex = 0;
	}

    // Update is called once per frame
    void Update () {
        maxSkillIndex = inventory.GetInventoryCount() - 1;
        ToRightSkill = playerController.GetKeyCode("right");
        ToLeftSkill = playerController.GetKeyCode("left");
        if (Input.GetKeyDown(ToRightSkill))
        {
             ShiftRight();
        }
        if (Input.GetKeyDown(ToLeftSkill))
        {
            ShiftLeft();
        }
        
        Indicate_Skill();
        inventory.setCurrentIndex(currentIndex);

        if (currentIndex == minSkillIndex) {

        }
        else if (currentIndex > maxSkillIndex) {
            currentIndex--;
        }
    }

    void ShiftLeft() {
        if (currentIndex >= 0 && currentIndex < maxSkillIndex)
        {
            currentIndex++;
        }
        else if (currentIndex == maxSkillIndex) {
            currentIndex = minSkillIndex;
        }
    }

    void ShiftRight() {
        if (currentIndex > 0 && currentIndex <= maxSkillIndex)
        {
            currentIndex--;
        }
        else if (currentIndex == 0) {
            currentIndex = maxSkillIndex;
        }
    }

    void Indicate_Skill() {
        if (inventory.GetInventoryCount() == 0)
        {
            Skill_Indicator.GetComponent<Image>().enabled = false;
        }
        else {
            Skill_Indicator.GetComponent<Image>().enabled = true;
        }
        Skill_Indicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.5f - 45*currentIndex, 0);
    }
}
