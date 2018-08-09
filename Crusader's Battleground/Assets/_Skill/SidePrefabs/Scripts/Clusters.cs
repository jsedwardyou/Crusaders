using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clusters : MonoBehaviour {

    Vector2 Direction;
    public float force = 200;
    public float damage = 10;

    bool startCoroutine = true;

    ParticleSystem endEffect;
    public float clusterSize = 0.1f;

    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(13, 13);
        endEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        endEffect.Stop();
        Direction = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f));
        transform.GetComponent<Rigidbody2D>().AddForce(Direction * force);
        transform.localScale = new Vector3(clusterSize, clusterSize, clusterSize);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (startCoroutine) {
            StartCoroutine(clusters(collision));

        }

        
    }

    IEnumerator clusters(Collision2D collision) {
        startCoroutine = false;
        endEffect.Play();
        if (collision.transform.tag == "Player") {
            //collision.transform.parent.GetComponent<PlayerStat>().currentHp -= damage;
        }

        while (endEffect.isPlaying) {
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
        

        yield return null;
    }


}
