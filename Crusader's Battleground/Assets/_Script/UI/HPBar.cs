using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    Image img;

    PlayerHP playerHP;
    PlayerStat playerStat;
    PlayerController playerController;

	// Use this for initialization
	void Start () {
        img = this.GetComponent<Image>();
        playerController = GetComponentInParent<PlayerController>();
        playerHP = GetComponentInParent<PlayerHP>();
        playerStat = GetComponentInParent<PlayerStat>();
    }
	
	// Update is called once per frame
	void Update () {
        img.fillAmount = playerHP.GetCurrentHP() / playerStat.MaxHp;

        if (playerController.GetPlayerDirection())
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
