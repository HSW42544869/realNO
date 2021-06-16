using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;

    private void Start()
    {
        playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
    }
    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(0.2f, 0.2f);
        //WeaponTrace.Create(e.gunEndPointPosition, e.shootPosition);
        //Shoot_Flash.AddFlash(e.gunEndPointPosition);
    }
}
