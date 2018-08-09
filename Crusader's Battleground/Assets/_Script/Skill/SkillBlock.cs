using UnityEngine;

public class SkillBlock : MonoBehaviour {

    public Skill skill;

    public float force = 500;

    Rigidbody2D rb;

    Vector2 randomDirection;

    KeyCode PickUpKey;

    PlayerController playerController;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = skill.skill_Image;
        rb = this.GetComponent<Rigidbody2D>();
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0, 1f));
        if (rb != null) {
            rb.AddForce(randomDirection * force);
        }
        
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerController = collision.transform.GetComponentInParent<PlayerController>();
            PickUpKey = playerController.GetKeyCode("pickUpKey");
            if (Input.GetKeyDown(PickUpKey))
            {
                if (collision.transform.GetComponentInParent<SkillInventory>().Get_Max_Items() > collision.transform.GetComponentInParent<SkillInventory>().GetInventoryCount())
                {
                    collision.transform.GetComponentInParent<SkillInventory>().Add_And_CheckTriple(skill);
                    Destroy(this.gameObject);
                }
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
