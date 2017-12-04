using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private float jumpHigh;
    [SerializeField] private float catchUpVelocity;
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float groundCheckRad = 0.1f;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D collider;
    private bool grounded;

    [SerializeField] private AudioSource audioSource;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        grounded = true;
    }

    void FixedUpdate()
    {
        //Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRad, groundLayer);
        //bool onGround = collider != null;

        bool onGround = Physics2D.IsTouchingLayers(collider);
        if ( onGround != grounded)
        {
            grounded = onGround;
            if(grounded) animator.SetTrigger("grounded");    
        }
        Debug.Log(rb.velocity.y);
    }

    void Update () {

        //Recover after impact with something
        if (Math.Abs(transform.position.x) > 0.01f)
        {
            rb.velocity = new Vector2(Math.Sign(-transform.position.x) * catchUpVelocity, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(0f, jumpHigh));
                animator.SetTrigger("jump");
                grounded = false;
            }
        }
    }

    void PlayJumpSound()
    {
        audioSource.Play();
    }
}
