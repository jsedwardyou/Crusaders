using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    private float _currentHP;

	// Use this for initialization
	void Start () {
        PlayerStat playerStat = GetComponent<PlayerStat>();
        _currentHP = playerStat.MaxHp;
	}

    public float GetCurrentHP() {
        return _currentHP;
    }

    public float DamageToCurrentHP(float damage) {
        _currentHP -= damage;
        return _currentHP;
    }
}
