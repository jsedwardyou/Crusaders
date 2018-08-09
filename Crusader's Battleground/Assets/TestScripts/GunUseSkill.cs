using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUseSkill : MonoBehaviour
{

    SkillInventory invenPlayer;

    float RightTrigger;
    KeyCode DropKey;
    bool canUse = true;
    float skillDuration;
    float tmp;
    int currentIndex;

    // Use this for initialization
    void Start()
    {
        SetPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.name == "Player1")
        {
            RightTrigger = Input.GetAxisRaw("Fire1");
            DropKey = KeyCode.Joystick5Button9;
        }
        else if (this.transform.parent.name == "Player2") {
            RightTrigger = Input.GetAxisRaw("Fire2");
            DropKey = KeyCode.Joystick2Button9;
        }
        else if (this.transform.parent.name == "Player3")
        {
            RightTrigger = Input.GetAxisRaw("Fire3");
            DropKey = KeyCode.Joystick6Button9;
        }
       // currentIndex = invenPlayer.currentIndex;

        ActivateSkill();

    }

    void ActivateSkill()
    {
        if (transform.name == "root")
        {
            if (true)//invenPlayer.inventory.Count != 0)
            {
                int index = currentIndex;
                if (true)//RightTrigger > 0 && tmp != RightTrigger && invenPlayer.SkillUI_inventory[currentIndex].GetComponent<Image>().fillAmount == 1)
                {
                    //invenPlayer.AbleToUseSkill[index] = false;
                    //StartCoroutine(SkillTimer(invenPlayer.inventory[index].duration, index));
                    //GameObject skill = Instantiate(invenPlayer.inventory[index].spell, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    //skill.GetComponent<CastingCharacter>().player = this.gameObject;
                    
                }
                else if (Input.GetKeyDown(DropKey))
                {
                    DropSkill();
                }
                tmp = RightTrigger;
            }

        }
    }

    void SetPlayers()
    {
        invenPlayer = transform.parent.GetComponent<SkillInventory>();
        
    }

    IEnumerator SkillTimer(float coolDown, int index)
    {
        float timer = Time.fixedTime;
        //invenPlayer.SkillUI_inventory[index].GetComponent<Image>().fillAmount = 0;
        while (true)//invenPlayer.SkillUI_inventory[index].GetComponent<Image>().fillAmount != 1)
        {
            //invenPlayer.SkillUI_inventory[index].GetComponent<Image>().fillAmount = (1 / coolDown) * (Time.fixedTime - timer);
            yield return new WaitForEndOfFrame();
        }


        yield return null;
    }

    void DropSkill()
    {
        Instantiate(invenPlayer.Remove(currentIndex), transform.position, Quaternion.identity);

    }



}
