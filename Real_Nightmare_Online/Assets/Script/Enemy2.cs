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
    public AudioClip die;
    [Header("死亡動畫")]
    public Animator deada;

    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("character").transform;
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

            Destroy(gameObject);
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
