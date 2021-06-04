using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plsyermovement : MonoBehaviour
{
    public float movespeed = 5f;    //速度
    

    [SerializeField] private LayerMask dashLayerMask;   //設定快速移動且不會被穿越
    public Rigidbody2D rb;  
    public Animator ani;
    public CircleCollider2D cir;
    private bool isDashButtonDown;  //快速移動觸發

    Vector3 moveDir;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    /// <summary>
    /// 正常移動    且用Blead tree判斷方向
    /// </summary>
    private void Move()
    { 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        ani.SetFloat("Horizontal", movement.x);
        ani.SetFloat("Vertical", movement.y);
        ani.SetFloat("Speed", movement.sqrMagnitude);

        moveDir = new Vector3(movement.x, movement.y).normalized;

        if (Input.GetKeyDown(KeyCode.Space))    //如果按下空白鍵觸發快速移動
        {
            isDashButtonDown = true;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDir * movespeed;  //按下空白鍵快速移動

        if (isDashButtonDown)
        {
            float dashAmount = 5f;
            Vector3 dashPosition = transform.position + moveDir * dashAmount;
            
            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);  //判斷是否移動方位是否有障礙物
            if (raycastHit2d.collider != null)
            {
                dashPosition = raycastHit2d.point;
            }
            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        } 
    }
}
