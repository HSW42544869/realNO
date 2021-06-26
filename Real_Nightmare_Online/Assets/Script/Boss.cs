using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("攻擊間隔")]
    public float[] time ;
    private float timer = 0; //紀錄時間
    [Header("蜘蛛絲")]
    public GameObject silk;
    [Header("生成點")]
    public Transform point;
    [Header("子彈速度")]
    public float speed;
    [Header("發射音效")]
    public AudioClip shot;

    private AudioSource aud;
    private Rigidbody2D rig;
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Born();
        Shoot();
        Die();
    }
    private void Shoot()
    {
        if (timer>= time[0])
        {
            timer = 0;
            aud.PlayOneShot(shot, Random.Range(0.3f, 0.5f));                                              // 播放音效
            GameObject temp = Instantiate(silk, point.position, point.rotation);                          // 生成子彈
            temp.GetComponent<Rigidbody2D>().AddForce(transform.right * speed + transform.up * 100);      // 子彈賦予推力

        }
        else
        {
            timer += Time.deltaTime;            // 累加時間
        }
    }
    [Header("小蜘蛛")]
    public GameObject spider;
    [Header("蜘蛛叫聲")]
    public AudioClip scream;

    private float timer1 = 0; //紀錄時間
    private void Born()
    {
        if (timer1 >= time[1])
        {
            timer1 = 0;
            aud.PlayOneShot(scream, Random.Range(0.3f, 0.5f));                                        // 播放音效
            Instantiate(spider, transform.position, transform.rotation);                              // 生成怪物
        }
        else
        {
            timer1 += Time.deltaTime;            // 累加時間
        }
    }
    private void Die()
    {
        if (plsyermovement.live <= 0)
        {
            enabled = false;
        }
    }
}
