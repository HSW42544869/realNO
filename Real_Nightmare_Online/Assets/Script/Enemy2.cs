using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("血量")]
    public int live = 10;
    [Header("移動範圍")]
    public float moverange = 10;
    [Header("目標物件")]
    public Transform target;
    [Header("移動速度")]
    public float speed = 0.5f;
    [Header("死亡音效")]
    public AudioClip deathsound;

    private AudioSource aud;
    private Animator ani;
    private Rigidbody2D rig;

    private void Awake()
    {

        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody2D>();
        target = GameObject.Find("character").transform;
        transform.position = new Vector3(transform.position.x,transform.position.y, target.position.z);
    }
    private void Update()
    {
        Move();
        Die();
    }
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float dis = Vector3.Distance(target.position, transform.position);

        if (dis < moverange)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Vector3 offset = transform.position - target.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward,-offset);
        }
       
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private void Die()
    {
        if(live <= 0)
        {
            
            ani.SetBool("dead" , true);
            //aud.PlayOneShot(deathsound, 2.5f);
            GetComponent<CapsuleCollider2D>().enabled = false;
            rig.Sleep();
            Destroy(gameObject,1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, moverange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "子彈(Clone)")
        {
            live -= 1;
            Destroy(collision);
        }
    }
}
