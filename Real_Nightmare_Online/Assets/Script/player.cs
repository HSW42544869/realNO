using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    /// <summary>
    /// 角色狀態
    /// </summary>
    [Header("血量")]
    public int health = 5;
    [Header("體力")]
    public int tired = 5;
    [Header("移動速度")]
    public float movespeed = 0.1f;
    [Header("旋轉速度")]
    public float spinspeed = 3;

    /// <summary>
    /// 槍枝
    /// </summary>
    [Header("紅外線取得")]
    public bool getred = false;
    [Header("子彈")]
    public int bullet = 50;
    [Header("彈夾")]
    public int clip = 10;
    [Header("裝彈時間")]
    public float relord = 2.5f;



    [Header("移動精靈")]
    public Transform target;
    private float reho = 0 ;
    private Joystick Movejoy;
    private Joystick Spinjoy;

    protected void Awake()
    {
        target = GameObject.Find("Mover").transform;
        Movejoy = GameObject.Find("MoveJoy").GetComponent<Joystick>();
        Spinjoy = GameObject.Find("SpinJoy").GetComponent<Joystick>();
    }
    private void Update()
    {
        MOVE();
        ROTATE();
        print(transform.position);
      
    }
    #region 角色操作
    /// <summary>
    /// 角色移動
    /// </summary>
    private void MOVE()
    {
        Vector3 dir = Movejoy.Direction * movespeed * 0.05f; // 取得搖桿向量
        transform.position = transform.position + dir;  // 移動角色
    }
    /// <summary>
    /// 角色旋轉
    /// </summary>
    private void ROTATE()
    {

        float ho = Spinjoy.Horizontal;  // 取得水平位移
        
        if (reho != ho)                 // 旋轉角色
        {
            if(reho > 0 && ho < reho)          
            {
                transform.Rotate(0, 0, ho * spinspeed);     // 反向    
            }
            else if (reho < 0 && ho > reho)          
            {
                transform.Rotate(0, 0, ho * spinspeed);     // 反向
            }
            else
            {
                transform.Rotate(0, 0, ho * -spinspeed);    // 正向
            }
            reho = ho;
        }
        

    }
    #endregion


    public void Fire()
    {
        //射擊
    }
    private void OnDrawGizmos()
    {
        
        //Gizmos.DrawRay(transform.position,);
    }

}
