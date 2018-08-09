using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {

    public GameObject ToPortal;

    PlayerController playerController;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            playerController = collision.GetComponentInParent<PlayerController>();
            if (Input.GetKeyDown(playerController.GetKeyCode("portalKey"))) {
                Teleport(collision.transform);
            }
        } 
    }

    public void Teleport(Transform collision) {
        collision.parent.position = ToPortal.transform.position;
        collision.position = ToPortal.transform.position;
    }
}
