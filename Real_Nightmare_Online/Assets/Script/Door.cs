using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //獲取GameManager腳本
    private GameManager gm;
    // Start is called before the first frame update
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            if (gm.GetKeyNumbers() > 0)
            {
                gm.UseKey();
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject);
            } 
        }
    }
}
