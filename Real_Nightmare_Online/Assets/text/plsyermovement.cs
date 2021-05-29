using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plsyermovement : MonoBehaviour
{
    public float movespeed = 5f;
    public float dashAmount = 50f;

    public Rigidbody2D rb;
    public Animator ani;
    public BoxCollider2D Box;
    private bool isDashButtonDown;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    { 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        ani.SetFloat("Horizontal", movement.x);
        ani.SetFloat("Vertical", movement.y);
        ani.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);

        if (isDashButtonDown)
        {
            
            rb.MovePosition(transform.position + movement * movespeed * dashAmount);
            isDashButtonDown = false;
        }
    }
}
