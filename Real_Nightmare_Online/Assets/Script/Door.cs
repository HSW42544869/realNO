using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            if (plsyermovement.key > 0)
            {
                plsyermovement.key -= 1;
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject);
            } 
        }
    }
}
