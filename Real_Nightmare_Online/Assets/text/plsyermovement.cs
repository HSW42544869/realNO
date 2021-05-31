using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plsyermovement : MonoBehaviour
{
    public float movespeed = 5f;
    

    [SerializeField] private LayerMask dashLayerMask;
    public Rigidbody2D rb;
    public Animator ani;
    public CircleCollider2D cir;
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

        moveDir = new Vector3(movement.x, movement.y).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDir * movespeed;

        if (isDashButtonDown)
        {
            float dashAmount = 5f;
            Vector3 dashPosition = transform.position + moveDir * dashAmount;

            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
            }
            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        } 
    }
}
