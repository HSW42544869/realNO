using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");   //追蹤目標
    }
    private void Update()
    {
        HandleAiming();
        Handleshoot();
    }
    private void HandleAiming()
    {
        Vector3 mousePosition =  UtilsClass.GetMouseWorldPosition(); //取得屬標位置

        Vector3 aimDirection = (mousePosition - transform.position).normalized; //鼠標的方向
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void Handleshoot()
    {
        if (Input.GetMouseButtonDown(0)) { 
            
        }
    }
        
}
