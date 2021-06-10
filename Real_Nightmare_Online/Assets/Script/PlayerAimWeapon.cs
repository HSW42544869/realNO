using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        private Transform aimGunEndPointTransform;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Animator ani;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");   //追蹤目標
        ani =aimTransform.GetComponent<Animator>(); //動畫控制
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
    }
    private void Update()
    {
        HandleAiming();
        Handleshooting();
    }
    private void HandleAiming()
    {
        Vector3 mousePosition =  UtilsClass.GetMouseWorldPosition(); //取得屬標位置

        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized; //鼠標的方向
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void Handleshooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            ani.SetBool("shoot",true);
            OnShoot.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
            
        }
        else
        {
            ani.SetBool("shoot", false);
        }
                
    }
        
}
