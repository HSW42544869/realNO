using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plsyermovement : MonoBehaviour
{
    public float movespeed = 3f;    //速度
    
    [SerializeField] private LayerMask dashLayerMask;   //設定快速移動且不會被穿越
    public Rigidbody2D rb;  
    public Animator ani;
    public CircleCollider2D cir;
    [Header("移動特效"),Tooltip("存放要生成的特效預製物")]
    public GameObject Specialeffects;
    [Header("特效生成點"), Tooltip("特效生成起始點")]
    public Transform point;

    private bool isDashButtonDown;  //快速移動觸發
    private Vector2 movement; //移動2維向量
    private Vector3 moveDir;  //瞬移3維向量
    private Vector3 rolldir;  //翻滾3為向量
    private Vector3 lastMoveDir;
    private float rollspeed;    //滾動速度
    private State state;
    private enum State { 
        Normal,
        Rolling,
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }

    // Update is called once per frame
    void Update(){
       
        Move();
    }
    /// <summary>
    /// 正常移動    且用Blead tree判斷方向
    /// </summary>
    private void Move()     //且用Blend tree判斷方位
    {
        switch (state) {
            case State.Normal:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                ani.SetFloat("Horizontal", movement.x);
                ani.SetFloat("Vertical", movement.y);
                ani.SetFloat("Speed", movement.sqrMagnitude);

                moveDir = new Vector3(movement.x, movement.y).normalized;
                if (movement.x != 0 || movement.y != 0) {
                    //Not idle
                    lastMoveDir = moveDir;
                }

                if (Input.GetKeyDown(KeyCode.N))    //如果按下空白鍵觸發快速移動
                {
                    isDashButtonDown = true;    //開關打開
                    
                    
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rolldir = lastMoveDir;
                    rollspeed = 25f;
                    state = State.Rolling;
                }
                break;
            case State.Rolling:
                float rollSpeedDropMutiplier = 3F;
                rollspeed -= rollspeed * rollSpeedDropMutiplier * Time.deltaTime;

                float rollSpeedMinimum = 10f;
                if (rollspeed < rollSpeedMinimum) {
                    state = State.Normal;
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rb.velocity = moveDir * movespeed;  //瞬移動

                if (isDashButtonDown)   //如果(瞬移開關打開)
                {
                    float dashAmount = 5f;
                    Vector3 dashPosition = transform.position + lastMoveDir * dashAmount;

                    RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, lastMoveDir, dashAmount, dashLayerMask);  //判斷是否移動方位是否有障礙物
                    if (raycastHit2d.collider != null)
                    {
                        dashPosition = raycastHit2d.point;
                    }
                    rb.MovePosition(dashPosition);
                    isDashButtonDown = false;
                }
                break;
            case State.Rolling:
                rb.velocity = rolldir * rollspeed;
                break;
        }
    }
}
