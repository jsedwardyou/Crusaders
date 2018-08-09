using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour {

    ParticleSystem flame;
    private float pushForce = 50;

    public float duration = 2f;
    public float damage = 1.0f;

    public GameObject player;

    float timer;




    // Use this for initialization
    void Start () {
        timer = Time.fixedTime + duration;
        player = transform.parent.GetChild(0).gameObject;

        flame = this.GetComponent<ParticleSystem>();
        
        StartCoroutine(FlameThrow());

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player") {
            if (other != player) {
                Rigidbody2D rigid = other.GetComponent<Rigidbody2D>();
                if (transform.rotation.eulerAngles.y <= 90)
                {
                    rigid.AddForce(pushForce * new Vector2(1, 0));
                }
                else if (transform.rotation.eulerAngles.y > 90)
                {
                    rigid.AddForce(pushForce * new Vector2(-1, 0));
                }
                //other.transform.parent.GetComponent<PlayerStat>().currentHp -= damage;
            }

        }
    }

    IEnumerator FlameThrow() {
        this.transform.SetParent(player.transform);
        if (true)//player.GetComponent<PlayerController>().isRight)
        {
            transform.position += new Vector3(3, 0, 0);
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.position += new Vector3(-3, 0, 0);
            this.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        //bool tempDirection = player.GetComponent<PlayerController>().isRight;
        bool changeDirection = false;
        while (timer > Time.fixedTime) {
            if (true)//player.GetComponent<PlayerController>().isRight != tempDirection)
                changeDirection = true;


            if (changeDirection) {
                if (true)//player.GetComponent<PlayerController>().isRight)
                {
                    transform.position = player.transform.position + new Vector3(3, 0, 0);
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                else
                {
                    transform.position = player.transform.position + new Vector3(-3, 0, 0);
                    this.transform.rotation = Quaternion.Euler(0, 270, 0);
                }
                changeDirection = false;
            }

            //tempDirection = player.GetComponent<PlayerController>().isRight;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
        yield return null;
    }


}
