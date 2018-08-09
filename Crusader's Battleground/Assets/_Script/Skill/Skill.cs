using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject {

    public GameObject spell;
    public GameObject skillEffect;
    public GameObject skillBlock;
    public Skill TripleSpell;

    public Sprite skill_Image;

    public string skillName;
    public string description;

    public float damage;
    public float range;
    public float duration;
    public float interval;
    public float speed;
    public float force;

    public bool stun;
    public float stunDuration;

    bool contact;




}
