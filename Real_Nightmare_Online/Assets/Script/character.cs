using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public float movespeed;
    private Rigidbody2D rig;
    private Vector3 moveDir;
    private Animator ani;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
       
    }
    private void Move()
    {
        float V = Input.GetAxis("Vertical");
        float H = Input.GetAxis("Horizontal");
        ani.SetBool("run", true);
    }
    private void FixedUpdate()
    {
       
    }
}
