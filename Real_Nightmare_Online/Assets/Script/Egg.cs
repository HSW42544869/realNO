using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [Header("血量")]
    static public int elive = 3;
    [Header("範圍")]
    public float range = 5;
    [Header("目標物件")]
    public Transform target;
    [Header("生成物件")]
    public GameObject born;
    [Header("生成間隔")]
    public float time = 3;
    private float timer = 0;

    private void Awake()
    {
        target = GameObject.Find("character").transform;
    }
    private void Update()
    {
        Born();
        Die();
    }

    private void Born()
    {
        float dis = Vector2.Distance(target.position, transform.position);

        if (timer >= time)
        {
            
            if (dis < range)
            {
                timer = 0;
                Instantiate(born, transform.position, transform.rotation);
            }

        }
        else
        {
            timer += Time.deltaTime;            // 累加時間
        }
        
    }

    private void Die()
    {
        if (elive <= 0)
        {
            Instantiate(born, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "子彈(Clone)")
        {
            elive -= 1;
            Destroy(collision);
        }
    }
}
