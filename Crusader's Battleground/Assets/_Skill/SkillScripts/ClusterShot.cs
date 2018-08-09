using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterShot : MonoBehaviour {

    public Skill skill;
    public GameObject cluster;

    float damage;
    float force;
    Vector3 startPosition;

    bool startCluster = true;
    

    GameObject primaryCanon;

    GameObject player;

    ParticleSystem endEffect;
    // Use this for initialization
    void Start () {
        setVariables();
        setStartAnimation();
        player.transform.parent.GetComponent<Animator>().Play("rpg_shot");
        player.transform.GetComponent<PlayerController>().SetPlayerAttackState(true);
        StartCoroutine(AttackMotion());
    }
	
	// Update is called once per frame
	void Update () {
	}

    void setVariables() {
        Physics2D.IgnoreLayerCollision(13, 13);
        player = GetComponent<CastingCharacter>().player;
        damage = skill.damage;
        force = skill.force;
        primaryCanon = skill.skillEffect;


        for (int i = 0; i < player.GetComponentsInChildren<BoxCollider2D>().Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentsInChildren<BoxCollider2D>()[i]);
        }
        for (int i = 0; i < player.GetComponentsInChildren<CircleCollider2D>().Length; i++)
        {
            Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), player.GetComponentsInChildren<CircleCollider2D>()[i]);
        }
        endEffect = transform.GetChild(0).GetComponent<ParticleSystem>(); endEffect.Stop();
    }

    void setStartAnimation() {
        transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
    }

    void ShootPrimaryCanon() {
        if (player.GetComponent<PlayerController>().GetPlayerDirection())
        {
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force + Vector2.up * force);
        }
        else {
            transform.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * force + Vector2.up * force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (startCluster) {
            StartCoroutine(Coroutine_Cluster(collision));
        }
    }

    IEnumerator Coroutine_Cluster(Collision2D collision) {
        startCluster = false;
        endEffect.Play();
        if (collision.transform.tag == "Player") {
            collision.transform.GetComponentInParent<PlayerHP>().DamageToCurrentHP(damage);
        }
        for (int i = 0; i < 4; i++) {
            Instantiate(cluster, transform.position, Quaternion.identity);
        }
        while (endEffect.isPlaying) {
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);

        yield return null;
    }

    IEnumerator AttackMotion() {
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.3f);
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ShootPrimaryCanon();
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerController>().SetPlayerAttackState(false);
        yield return null;
    }
}
