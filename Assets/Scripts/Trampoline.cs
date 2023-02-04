using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator animator;
    public float bounce = 60f;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
            
            animator.SetTrigger("action");
            
            playerRigidBody.velocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
