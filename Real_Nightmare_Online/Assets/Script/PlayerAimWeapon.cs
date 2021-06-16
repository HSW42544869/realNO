using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private newfieldofview newfieldofview;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        private Transform aimGunEndPointTransform;
    }

    public event EventHandler<OnShootEventArgs> OnShoot;
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Animator ani;
    bool isAimDownSights = false;

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
        newfieldofview.SetAimDirection(aimDirection);
        newfieldofview.SetOrigin(transform.position);
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
        if (Input.GetMouseButtonDown(1))
        {
            isAimDownSights = !isAimDownSights;
            if (isAimDownSights)
            {
                newfieldofview.SetFoV(40f);
                newfieldofview.SetViewDistance(100f);
            }else
            {
                newfieldofview.SetFoV(90f);
                newfieldofview.SetViewDistance(50f);
            }
        }
                
    }
        
}
