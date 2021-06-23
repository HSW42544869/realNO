﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldofview;
    public class OnShootEventArgs : EventArgs   //位置上進行動作
    {
        public Vector3 gunEndPointPosition; //射擊動畫位置
        public Vector3 shootPosition;       //射擊位置
        public Transform shellPosition;  //射擊子彈位置
    }

    public event EventHandler<OnShootEventArgs> OnShoot;
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator ani;


    bool isAimDownSights = false;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");   //追蹤目標
        ani =aimTransform.GetComponent<Animator>(); //動畫控制
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition"); //獲取物件
        aimShellPositionTransform = aimTransform.Find("ShellPosition");            
    }
    private void Update()
    {
        HandleAiming();
        Handleshooting();
    }
    /// <summary>
    /// 取得位置
    /// </summary>
    private void HandleAiming()
    {
        Vector3 mousePosition =  UtilsClass.GetMouseWorldPosition();    //取得鼠標位置

        Vector3 aimDirection = (UtilsClass.GetMouseWorldPosition() - transform.position).normalized;  //鼠標的方向，normalized:歸一化
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;  //將歸一化的數值計算且傳回angle
        aimTransform.eulerAngles = new Vector3(0,0, angle);
        fieldofview.SetAimDirection(aimDirection);
        fieldofview.SetOrigin(transform.position);
    }
    /// <summary>
    /// 射擊動作、開關
    /// </summary>
    private void Handleshooting()
    {
        if (Input.GetMouseButtonDown(0))    //射擊按鍵
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            ani.SetBool("shoot",true);
            OnShoot.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                
            }) ;
            
        }
        else
        {
            ani.SetBool("shoot", false);    //開槍初始直
        }
        if (Input.GetMouseButtonDown(1))    //手電筒轉換開關按鍵
        {
            isAimDownSights = !isAimDownSights;
            if (isAimDownSights)
            {
                fieldofview.SetFoV(40f);
                fieldofview.SetViewDistance(15f);
            }else
            {
                fieldofview.SetFoV(20f);
                fieldofview.SetViewDistance(40f);
            }
        }
                
    }
        
}
