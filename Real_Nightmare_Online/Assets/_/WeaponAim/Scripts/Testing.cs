using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour {

    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [Header("子彈")]
    public GameObject b;
    [Header("生成點")]
    public Transform point;
    [Header("子彈速度")]
    public float speed;
    [Header("射擊音效")]
    public AudioClip shootA;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
    private void Start() {
        playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot;
    }
    private void Update()
    {
        shoot();
    }

    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e) {
        UtilsClass.ShakeCamera(.6f, .05f);
        WeaponTracer.Create(e.gunEndPointPosition, e.shootPosition);
        Shoot_Flash.AddFlash(e.gunEndPointPosition);

        Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        shootDir = UtilsClass.ApplyRotationToVector(shootDir, 90f);
       


    }
    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject temp = Instantiate(b, point.position, point.rotation);   // 生成子彈
            aud.PlayOneShot(shootA, Random.Range(0.3f, 0.5f));
            temp.GetComponent<Rigidbody2D>().velocity = -transform.up * speed;    // 子彈賦予推力
            Destroy(temp, 2);
        }
        
    }
}
