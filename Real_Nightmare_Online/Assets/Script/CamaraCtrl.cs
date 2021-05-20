using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraCtrl : MonoBehaviour
{
    [Header("追蹤物件")]
    public Transform target;
    [Header("移動速度")]
    public float speed = 2;
    [Header("限制")]
    public Vector2 height = new Vector2(0, 20.65f);
    public Vector2 width = new Vector2(0, 27);

    private void Update()
    {
        Track();
    }

    /// <summary>
    /// 物件追蹤
    /// </summary>
    private void Track()
    {   
        Vector3 posA = target.position;     // 目標座標
        Vector3 posB = transform.position;  // 攝影機座標

        posA.y = Mathf.Clamp(posA.y, height.x, height.y); // 判斷上下距離
        posA.x = Mathf.Clamp(posA.x, width.x, width.y);   // 判斷左右距離

        posB = Vector2.Lerp(posB, posA,Time.deltaTime * speed); 
        transform.position = posB;  // 設定新攝影機座標      
    }
}
