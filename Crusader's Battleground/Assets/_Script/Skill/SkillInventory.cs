using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventory : MonoBehaviour {

    public GameObject inventoryUIFrame;

    [SerializeField]
    private int max_inventory_items;

    private List<Skill> inventory = new List<Skill>();
    private List<GameObject> inventory_UI = new List<GameObject>();
    private List<bool> AbleToUseSkill = new List<bool>();
    private int currentIndex;
    private int ui_index;

    private void Start()
    {
        SetUpSkillUI();
    }

    public void Add_And_CheckTriple(Skill skill) {
        inventory.Add(skill);
        AbleToUseSkill.Add(true);
        DisplaySkillUI(skill);
        if (inventory.Count >= 3) {
            if (skill.TripleSpell != null)
            {
                if (checkTripleFromEnd(inventory.Count - 1)) {
                    StartCoroutine(RemoveTriple(inventory.Count-1));
                    Add_And_CheckTriple(skill.TripleSpell);
                }
            }
        }
    }

    public GameObject Remove(int index) {
        if (currentIndex == inventory.Count - 1) {
            currentIndex--;
        }

        GameObject temp = inventory[index].skillBlock;
        inventory.RemoveAt(index);
        RemoveSkillUI(index);
        if (inventory.Count >= 3) {
            for (int i = 2; i < inventory.Count; i++) {
                if (inventory[i].TripleSpell != null) {
                    if (checkTripleFromEnd(i)) {
                        Skill tempSkill = inventory[i].TripleSpell;
                        StartCoroutine(RemoveTriple(i));
                        Add_And_CheckTriple(tempSkill);
                    }
                }
            }
        }

        return temp;
    }

    /*Check if three identical spells are adjacent to one another*/
    //inventory => canon, canon, missile -> false
    //inventory.Add(canon) => canon, canon, canon, missile -> true
    bool checkTripleFromEnd(int index) {
        if (inventory.Count > 2)
        {
            string skillName = inventory[index].skillName;
            if (skillName == inventory[index - 1].skillName && skillName == inventory[index - 2].skillName)
            {
                return true;
            }
            else {
                return false;
            }
        }
        return false;
    }

    IEnumerator RemoveTriple(int i) {
        Remove(i); Remove(i-1); Remove(i-2);
        yield return null;
    }

    void SetUpSkillUI()
    {
        for (int i = 0; i < max_inventory_items; i++)
        {
            
            GameObject SkillUI = Instantiate(new GameObject("Skill" + i), new Vector3(0, 0, 0), Quaternion.identity);
            SkillUI.transform.SetParent(inventoryUIFrame.transform);
            SkillUI.AddComponent<Image>();
            RectTransform SkillUI_RectTransform = SkillUI.GetComponent<RectTransform>();
            SetSkillUI_RectTransform(/* RectTransform */ SkillUI_RectTransform,
                                     /* anchorMin     */ new Vector2(1, 0.5f),
                                     /* anchorMax     */ new Vector2(1, 0.5f),
                                     /* pivot         */ new Vector2(1, 0.5f),
                                     /* sizeHorizontal*/ 40,
                                     /* sizeVertical  */ 40,
                                     /* anchoredPos   */ new Vector2(-49.5f - 45 * (i - 1), 0)
                                     );
            
            
            Image SkillUI_Image = SkillUI.GetComponent<Image>();
            SkillUI_Image.type = Image.Type.Filled;
            SkillUI_Image.fillMethod = Image.FillMethod.Vertical;
            SkillUI_Image.enabled = false;

            inventory_UI.Add(SkillUI);
        }
        ui_index = 0;
    }

    void SetSkillUI_RectTransform(RectTransform rect, Vector2 aMin, Vector2 aMax, Vector2 piv, float sizeHorizontal, float sizeVertical, Vector2 pos) {
        rect.anchorMin = aMin;
        rect.anchorMax = aMax;
        rect.pivot = piv;
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeHorizontal);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeVertical);
        rect.anchoredPosition = pos;
    }

    void DisplaySkillUI(Skill skill) {
        Image skill_Image = inventory_UI[ui_index].GetComponent<Image>();
        skill_Image.enabled = true;
        skill_Image.sprite = skill.skill_Image;
        ui_index++;
    }

    void RemoveSkillUI(int index) {
        Image skill_Image = inventory_UI[index].GetComponent<Image>();
        if (index == inventory_UI.Count - 1)
        {
            skill_Image.enabled = false;
        }
        else {
            for (int i = index; i < inventory_UI.Count - 1; i++) {
                inventory_UI[i].GetComponent<Image>().sprite = inventory_UI[i + 1].GetComponent<Image>().sprite;
            }
            inventory_UI[inventory.Count].GetComponent<Image>().enabled = false;
        }
        ui_index--;
    }

    public int GetCurrentIndex() {
        return currentIndex;
    }

    public int GetInventoryCount() {
        return inventory.Count;
    }

    public Skill GetInventorySkill(int index) {
        return inventory[index];
    }

    public GameObject GetInventory_UIObj(int index) {
        return inventory_UI[index];
    }

    public void SetAbleToUseSkillFalse(int index) {
        AbleToUseSkill[index] = false;
    }

    public void setCurrentIndex(int index) {
        currentIndex = index;
    }

    public int Get_Max_Items() {
        return max_inventory_items;
    }








}
