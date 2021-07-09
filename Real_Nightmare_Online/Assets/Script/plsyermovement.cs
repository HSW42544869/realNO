using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class plsyermovement : MonoBehaviour
{
    [Header("無敵時間")]
    public float invin = 2;
    public float movespeed = 3f;    //速度
    [Header("生命值")]
    static public int live = 100;
    [SerializeField] private LayerMask dashLayerMask;   //設定快速移動且不會被穿越
    public Rigidbody2D rb;
    public Animator ani;
    public CircleCollider2D cir;
    [Header("移動特效"), Tooltip("存放要生成的特效預製物")]
    public GameObject Specialeffects;
    [Header("特效生成點"), Tooltip("特效生成起始點")]
    public Transform point;
    [Header("死亡音效")]
    public AudioClip die;

    private AudioSource aud;
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
        aud = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
        
    }

    // Update is called once per frame
    void Update() {

        Move();
        Invin();
        Die();
    }

    #region 角色操作
    /// <summary>
    /// 正常移動    且用Blead tree判斷方向
    /// </summary>
    private void Move()     //且用Blend tree判斷方位
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 offset = transform.position -mousePosition;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, offset);

        switch (state) {
            case State.Normal:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");


                moveDir = new Vector3(movement.x, movement.y).normalized;
                if (movement.x != 0 || movement.y != 0) {
                    //Not idle
                    lastMoveDir = moveDir;
                }

                if (Input.GetKeyDown(KeyCode.F))    //如果按下空白鍵觸發快速移動
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
    #endregion


    #region 受到傷害
    private bool hit;
    private float time = 0;
    private void Invin()
    {
        if (time >= invin)
        {
            time = 0;
            hit = true;
        }
        else
        {
            time += Time.deltaTime;            // 累加時間
        }
    }
    
    private int hit_sp = 15;//蜘蛛手下傷害
    private int hit_boss = 25; //boss傷害
    private int hit_skil = 5; //蜘蛛絲傷害

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit)
        {
            
            hit = false;
            if (collision.name == "Sprite(Clone)")
            {
                live -= hit_sp;
            }
            if(collision.name == "S(Clone)")
            {
                Destroy(collision.gameObject);
                live -= hit_skil;
            }
            if (collision.name == "Boss")
            {
                live -= hit_boss;
            }
        }
        
        if(collision.name == "傳送門")
        {
            int lvIndex = SceneManager.GetActiveScene().buildIndex;     // 取得當前場景的編號

            lvIndex++;                                                  // 編號加一

            SceneManager.LoadScene(lvIndex);                            // 載入下一關
        }
    }
    #endregion

    #region 玩家死亡
        private void Die()
    {
        if(live <= 0)
        {
            aud.PlayOneShot(die, Random.Range(0.3f, 0.5f));
            //ani.SetBool("", true);
            enabled = false;  
        }
    }
#endregion
}
