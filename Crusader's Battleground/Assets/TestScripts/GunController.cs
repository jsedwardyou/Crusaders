using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{

    //Player Stats
    public PlayerStat playerStat;
    LayerMask layer;
    int layerint;

    //Player Movement
    public float x; public float y;

    //Player BasicAttack
    public float attackRange = 10f;
    public float attackSpeed = 0.25f;
    public bool attackMotion = true;
    public float attackDamage = 1.0f;
    public float knockBack = 1000;

    //Boolean
    public bool canJump = true;
    bool attack = true;
    public bool isRight;
    bool Walk;
    bool pickUpTrigger = true;

    //KeyCode
    KeyCode jumpKey;
    float fireKey;
    KeyCode attackKey;
    KeyCode pickUp;


    //Player Sprite
    SpriteRenderer playerSprite;

    //Player Animation
    Animator anim;

    //Coroutine
    bool coroutineTrigger = true;




    // Use this for initialization
    void Start()
    {
        playerStat = transform.parent.GetComponent<PlayerStat>();


        Physics2D.IgnoreLayerCollision(9, 12);

        anim = transform.parent.GetComponent<Animator>();


        if (this.transform.parent.name == "Player1")
        {
            jumpKey = KeyCode.Joystick5Button0;
            attackKey = KeyCode.Joystick5Button1;
            pickUp = KeyCode.Joystick5Button3;
            layer |= (1 << LayerMask.NameToLayer("Player1"));
            layer |= (1 << LayerMask.NameToLayer("Gas"));
            layer = ~layer;
            layerint = layer.value;
        }
        else if (this.transform.parent.name == "Player2") {
            jumpKey = KeyCode.Joystick2Button0;
            attackKey = KeyCode.Joystick2Button1;
            pickUp = KeyCode.Joystick2Button3;
            layer |= (1 << LayerMask.NameToLayer("Player2"));
            layer |= (1 << LayerMask.NameToLayer("Gas"));
            layer = ~layer;
            layerint = layer.value;
        }
        else if (this.transform.parent.name == "Player3")
        {
            jumpKey = KeyCode.Joystick6Button0;
            attackKey = KeyCode.Joystick6Button1;
            pickUp = KeyCode.Joystick6Button3;
            layer |= (1 << LayerMask.NameToLayer("Player3"));
            layer |= (1 << LayerMask.NameToLayer("Gas"));
            layer = ~layer;
            layerint = layer.value;
        }

    }

    private void FixedUpdate()
    {
        if (attackMotion) {
            if (this.transform.GetComponentInParent<PlayerStat>().PlayerName == "Player1")
            {
                x = -Input.GetAxisRaw("Horizontal4");
            }
            else if (this.transform.GetComponentInParent<PlayerStat>().PlayerName == "Player2") {
                x = -Input.GetAxisRaw("Horizontal2");
            }
            else if (this.transform.GetComponentInParent<PlayerStat>().PlayerName == "Player3")
            {
                x = -Input.GetAxisRaw("Horizontal3");
            }


            Vector3 movement = new Vector3(x, 0, 0);

        //transform.Translate(movement * Time.deltaTime * playerStat.speed);
        Flip();
            
        


        Jump(Input.GetKey(jumpKey));
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("x", x);
        Dead();
        Attack();

        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            Debug.Log(transform.parent.name);
        }

    }

    void Flip()
    {
        if (x < 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            isRight = false;
            Walk = true;
        }
        else if (x > 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            isRight = true;
            Walk = true;
        }
        else {
            Walk = false;
        }
        anim.SetBool("Walk", Walk);
    }

    void Jump(bool space)
    {
        if (space)
        {
            if (canJump)
            {
                canJump = false;
                this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerStat.jumpForce);
            }
        }
    }


    void Attack()
    {
        if (Input.GetKeyDown(attackKey))
        {
            if (attackMotion)
            {
                StartCoroutine(Coroutine_Attack());
            }

        }

    }

    void Dead()
    {
        if (true)//playerStat.currentHp < 0)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
        }
    }


    IEnumerator Coroutine_Attack()
    {
        attackMotion = false;
        anim.Play("attack1");

        //anim.SetBool("Attack", false);

        Vector2 direction;
        if (isRight)
        {
            direction = transform.right;
        }
        else
        {
            direction = -transform.right;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 2, 0), direction, attackRange, layer);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Box")
            {
                hit.transform.GetComponent<Box>().boxHp -= attackDamage;
            }
            else if (hit.collider.tag == "Player")
            {
                //hit.transform.parent.GetComponent<PlayerStat>().currentHp -= attackDamage;
                hit.transform.GetComponent<Rigidbody2D>().AddForce(-hit.normal * knockBack);
            }
        }
        else
        {

        }

        yield return new WaitForSeconds(attackSpeed);
        attackMotion = true;
        yield return null;
    }

}
