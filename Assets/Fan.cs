using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private PlayerMovement playerScript;

    [SerializeField] private float bounce = 60f;
    private float currentBounce = 0f;
    private bool isFlying;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            if((int)playerScript.state == 2) //jumping
                player.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);

            else if((int)playerScript.state == 3)
                player.AddForce(Vector2.up * 1f, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (int)playerScript.state == 3)
        {
            player.velocity = Vector2.zero;
            player.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        }
    }
}
