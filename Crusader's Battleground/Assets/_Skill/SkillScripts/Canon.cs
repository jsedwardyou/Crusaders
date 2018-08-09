using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Canon : MonoBehaviour {
    public GameObject player;
    public Skill skillInfo;


    private float damage;
    private float range;
    private float duration;
    private float speed;
    private float height;
    private float force;

    private bool coroutineTrigger = true;

    GameObject skillEffect;

    private bool contact = false;
    private bool move = true;
    private Vector3 pos;

    float timer;


    // Use this for initialization
    void Start() {
        player = this.GetComponent<CastingCharacter>().player;
        for (int i = 0; i < player.GetComponentsInChildren<BoxCollider2D>().Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentsInChildren<BoxCollider2D>()[i]);
        }
        for (int i = 0; i < player.GetComponentsInChildren<CircleCollider2D>().Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentsInChildren<CircleCollider2D>()[i]);
        }
        Physics2D.IgnoreLayerCollision(13, 13);

        player.transform.parent.GetComponent<Animator>().Play("rpg_shot");

        damage = skillInfo.damage;
        range = skillInfo.range;
        duration = skillInfo.duration;
        speed = skillInfo.speed;
        skillEffect = skillInfo.skillEffect;
        force = skillInfo.force;
        pos = transform.position;

        timer = Time.fixedTime + duration;

    }
	
	// Update is called once per frame
	void Update () {
        if (timer > Time.fixedTime && coroutineTrigger)
        {
            StartCoroutine(startSkill());
            coroutineTrigger = false;
        }
        else if(timer < Time.fixedTime) {
            Destroy(this.gameObject);
        }

        if (!move)
        {
            this.transform.position = pos;
        }
        else {
            pos = transform.position;
        }
	}
    public void projectile()
    {
        if (!player.GetComponent<PlayerController>().GetPlayerDirection())
        {
            GetComponent<Rigidbody2D>().AddForce(-Vector2.right * force + Vector2.up * force);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * force + Vector2.up * force);
        }
    }

    IEnumerator startSkill()
    {
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<PlayerController>().SetPlayerAttackState(true);
        yield return new WaitForSeconds(0.2f);
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ParticleSystem EndEffect;
        ParticleSystem MainEffect;
        EndEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        MainEffect = transform.GetComponent<ParticleSystem>();
        MainEffect.Play();
        EndEffect.Stop();
        projectile();
        StartCoroutine(startMotion());
        while (!contact) {
            yield return new WaitForEndOfFrame();
        }
        MainEffect.Stop();
        EndEffect.Play();
        move = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.GetComponentInParent<PlayerHP>().DamageToCurrentHP(damage);
        }
        else if (collision.gameObject.tag == "Box")
        {
            collision.gameObject.GetComponent<Box>().boxHp -= damage;
        }

        contact = true;
    }
    IEnumerator startMotion() {
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerController>().SetPlayerAttackState(false);
        yield return null;
    }
}
