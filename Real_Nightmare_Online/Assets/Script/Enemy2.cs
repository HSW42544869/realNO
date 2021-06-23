using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("移動範圍")]
    public float moverange = 10;
    [Header("目標物件")]
    public Transform target;
    [Header("移動速度")]
    public float speed = 0.5f;

    private void Awake()
    {
        target = GameObject.Find("character").transform;
    }
    private void Update()
    {
        Move();
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
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, moverange);
    }
}
