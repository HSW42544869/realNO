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

    Vector3 moveDir;
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
            float dashAmount = 50f;
            Vector3 dashPosition = rb.position + movement * movespeed * dashAmount;

            RaycastHit2D raycastHit2d = Physics2D.Raycast(rb.position, movement*movespeed, dashAmount);
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
            }
            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        } 
    }
}
