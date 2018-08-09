using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileController : MonoBehaviour {

    public Skill skill;
    //Missile stat
    public float damage = 10;

    ParticleSystem Nova;
    ParticleSystem EndEffect;

    private bool _facingRight;
    public float speed;
    public float lifeDuration;
    bool startUpdate = false;

    float timer;

    private bool move = true;

    public GameObject player;

	// Use this for initialization
	void Start () {
        if (player == null)
        {
            player = GetComponent<CastingCharacter>().player;
        }
        player.transform.parent.GetComponent<Animator>().Play("attack2");
        Physics2D.IgnoreLayerCollision(13, 13);
        _facingRight = player.GetComponent<PlayerController>().GetPlayerDirection();
        Nova = this.GetComponent<ParticleSystem>();
        EndEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        Nova.Play();
        EndEffect.Stop();

        timer = Time.fixedTime + lifeDuration;
        for (int i = 0; i < player.GetComponentsInChildren<BoxCollider2D>().Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentsInChildren<BoxCollider2D>()[i]);
        }
        for (int i = 0; i < player.GetComponentsInChildren<CircleCollider2D>().Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentsInChildren<CircleCollider2D>()[i]);
        }
        StartCoroutine(startRoutine());


    }
	
	// Update is called once per frame
	void Update () {

        if (startUpdate) {
            if (timer > Time.fixedTime && move)
            {
                if (_facingRight)
                {
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                }
                else
                {
                    transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
                }
            }
            else if (timer < Time.fixedTime && move)
            {
                Destroy(this.gameObject);
            }
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(contact());

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<PlayerHP>().DamageToCurrentHP(damage);
        }
        else if (collision.gameObject.tag == "Box") {
            collision.gameObject.GetComponent<Box>().boxHp -= damage;
        }
    }

    IEnumerator contact() {
        GetComponent<CircleCollider2D>().enabled = false;
        move = false;
        Nova.Stop();
        EndEffect.Play();
        while (EndEffect.isPlaying) {
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
        yield return null;
    }

    IEnumerator startRoutine() {
        player.GetComponent<PlayerController>().SetPlayerAttackState(true);
        yield return new WaitForSeconds(0.5f);
        startUpdate = true;
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<PlayerController>().SetPlayerAttackState(false);
        yield return null;
    }
    
}
